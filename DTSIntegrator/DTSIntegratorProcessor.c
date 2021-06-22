//  Copyright © Titian Software Ltd
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Titian.BMS.Shipping.DTSIntegration.TasProd.Constant;
using Titian.BMS.Shipping.DTSIntegration.TasProd.DataAccess;
using Titian.BMS.Shipping.DTSIntegration.TasProd.DataModel;
using Titian.BMS.Shipping.DTSIntegration.TasProd.DataTransferObjects;
using Titian.Infrastructure.DataAccess;
using Titian.Shipping.Constants;
using Titian.Shipping.DataAccess.DataTransferObject;
using Titian.Shipping.DataAccess.Interfaces;
using Titian.TAS.TASServer.Interop;

namespace Titian.BMS.Shipping.DTSIntegration.TasProd
{
  /// <summary>
  /// class DTS Integrator Processor
  /// </summary>
  public class DTSIntegratorProcessor : ITASServer
  {
    const string c_shipmentStatus = "SHIP";
    const string c_sourceApplication = "Mosaic";
    const string c_shipmentType = "DOMESTIC";
    const string c_destinationCategory = "SCIENTIST";
    const string c_cmShipment = "CM Shipping -";
    const string c_entityType = "Chemical";
    const string c_cancelStatus = "CANCELLED";
    const string c_errorWritingToDTS= "Error Writing to DTS";

    private readonly IDTSIntegratorApiHelper m_dtsIntegratorApiHelper;
    private readonly IDADTSIntegrator m_daDTSIntegrator;
    private readonly IDAShipment m_daShipment;

    protected int ProcInstanceId =
      DAProcInstance.GetAppProcInstanceId(Environment.MachineName, "Titian.BMS.Shipping.DTSIntegration.TasProd");

    public DTSIntegratorProcessor()
    {
      m_dtsIntegratorApiHelper = new DTSIntegratorApiHelper();
      m_daDTSIntegrator = new DADTSIntegrator();
      m_daShipment = new Titian.Shipping.DataAccess.Implementations.DAShipment();
    }

    public DTSIntegratorProcessor(IDTSIntegratorApiHelper dtsIntegratorApiHelper, IDADTSIntegrator daDTSIntegrator, IDAShipment daShipment)
    {
      m_dtsIntegratorApiHelper = dtsIntegratorApiHelper;
      m_daDTSIntegrator = daDTSIntegrator;
      m_daShipment = daShipment;
    }


    /// <summary>
    /// The Prod method of ITASServer interface, to trigger the job process. 
    /// </summary>
    public void Prod()
    {
      try
      {
        Trace.WriteLine("TAS prod start.");
        var shipments = GetListOfInternalShipment();
        Trace.WriteLine("No of shipment processed for DTS service " + shipments.Count());
        foreach (var item in shipments)
        {
          try
          {
            ParseDespatchItemToDistributionGroup(GetDespatchLabwareByShipmentId(item.Key), item.Value.ID);
            var bmsTrackingNumber = GetBMSTrackingNumber(item.Value.ID);
            m_daShipment.UpdateShipment(ProcInstanceId, item.Key, bmsTrackingNumber);
          }
          catch (Exception err)
          {
            Trace.TraceError("Error for Mosaic shipmnetId "+ item.Key + " in ParseDespatchItemToDistributionGroup " + err.StackTrace);
            m_daShipment.UpdateShipment(ProcInstanceId, item.Key, c_errorWritingToDTS);
          }
          
        }

        Trace.WriteLine("TAS prod end.");
      }
      catch (Exception ex)
      {
        Trace.WriteLine("Error on Prod " + ex.StackTrace);
      }
      
    }

    /// <summary>
    /// The prodex method of ITASServer interface, Not supported here 
    /// </summary>
    /// <param name="lngReference">lng reference</param>
    public void ProdEx(int lngReference)
    {
      throw new NotSupportedException(GetType().AssemblyQualifiedName + ".ProdEx not supported.");
    }

    /// <summary>
    /// Create shipment method used to call the post api of DTS service to submit shipment
    /// </summary>
    /// <param name="dtsShipment">DTSShipment object</param>
    /// <returns>Task<HttpResponseMessage> object</returns>
    public Task<HttpResponseMessage> CreateShipment(DTSShipment dtsShipment)
    {
      return m_dtsIntegratorApiHelper.CreateShipment(dtsShipment);
    }

    /// <summary>
    /// Get shipment method used to call get api of DTS service to get shipment object by shipment id
    /// </summary>
    /// <param name="shipmentId">shipment id</param>
    /// <returns>Task<HttpResponseMessage> object</returns>
    public Task<HttpResponseMessage> GetShipment(int shipmentId)
    {
      return m_dtsIntegratorApiHelper.GetShipment(shipmentId);
    }

    /// <summary>
    /// Create Distribution Group method used to call post api of DTS service to submint distribution group object
    /// </summary>
    /// <param name="dTSDistributionGroup">DTSDistributionGroup object</param>
    /// <returns>Task<HttpResponseMessage> object</returns>
    public Task<HttpResponseMessage> CreateDistributionGroup(DTSDistributionGroup dTSDistributionGroup)
    {
      return m_dtsIntegratorApiHelper.CreateDistributionGroup(dTSDistributionGroup);
    }

    /// <summary>
    /// Create distritubtion method used to call post api of DTS service to submit distribution object
    /// </summary>
    /// <param name="dtsDistribution"> DTSDistribution object</param>
    /// <returns>Task<HttpResponseMessage> object</returns>
    public Task<HttpResponseMessage> CreateDistribution(DTSDistribution dtsDistribution)
    {
      return m_dtsIntegratorApiHelper.CreateDistribution(dtsDistribution);
    }

    /// <summary>
    /// Update Distribution Group Status method used to call the post api of DTS service to Update Distribution Group Status
    /// </summary>
    /// <param name="dtsStatus">DTSStatus object</param>
    /// <returns>Task<HttpResponseMessage> object</returns>
    public Task<HttpResponseMessage> UpdateDistributionGroupStatus(DTSStatus dtsStatus)
    {
      return m_dtsIntegratorApiHelper.UpdateDistributionGroupStatus(dtsStatus);
    }

    /// <summary>
    /// Get list of Internal shipment and remodel it into DTS shipment model
    /// </summary>
    /// <returns>Dictionary<int, DTSShipment> Object</returns>
    public Dictionary<int, DTSShipment> GetListOfInternalShipment()
    {
      List<Task> tasks = new List<Task>();
      Dictionary<int, DTSShipment> retVal = new Dictionary<int, DTSShipment>();
      DTSAddress dtsSourceAddress=null;
      var shipmentStates =new int []{ (int)ShipmentState.Complete, (int)ShipmentState.InTransit};
      var shipmentDtos = m_daShipment.GetShipmentByStates(ProcInstanceId, shipmentStates);
      if (shipmentDtos != null && shipmentDtos.Count > 0)
      {
        foreach (ShipmentDto item in shipmentDtos.Where(f =>string.IsNullOrEmpty( f.ExternalReference)))
        {
          var sourceZoneAddress = m_daShipment.GetZoneAddressByZoneId(ProcInstanceId,item.SourceZoneId);
          if (sourceZoneAddress!=null)
          {
            dtsSourceAddress = new DTSAddress() {
              City=sourceZoneAddress.City,
              Country=sourceZoneAddress.CountryName,
              EMail = sourceZoneAddress.EmailAddress ?? string.Empty,
              Name = sourceZoneAddress.Recipient,
              Phone = sourceZoneAddress.Phone ?? string.Empty,
              State = sourceZoneAddress.StateName ?? string.Empty,
              Street1 = sourceZoneAddress.AddressLine1 ?? string.Empty,
              Street2 = sourceZoneAddress.AddressLine2 ?? string.Empty,
              Zip = sourceZoneAddress.PostalCode ?? string.Empty,
            };
          }
          var recipientAddress = new DTSAddress()
          {
            City = item.City,
            Country = item.CountryName,
            EMail = item.EmailAddress ?? string.Empty,
            Name = item.Recipient,
            Phone = item.Phone ?? string.Empty,
            State =item.StateName ?? string.Empty,
            Street1 = item.AddressLine1?? string.Empty,
            Street2 = item.AddressLine2 ?? string.Empty ,
            Zip = item.PostalCode?? string.Empty,
          };

          var dtsShipment = new DTSShipment()
          {
            Status = c_shipmentStatus,
            SourceApplication = c_sourceApplication,
            SourceSite = item.SourceZoneName,
            DestinationSite = item.DestinationZoneName,
            DestinationName = item.Recipient ?? item.DestinationZoneName,
            DestinationCategory = c_destinationCategory,
            Recipient = item.Recipient,
            ShipmentType = c_shipmentType,
            Title = $"{c_cmShipment}{item.SourceZoneName}",
            ShipDate = DateTime.Now,
            RecipientAddress = recipientAddress,
            SenderAddress= dtsSourceAddress,
            CreatedBy =  item.CreateUserName
          };

          Task task = m_dtsIntegratorApiHelper.CreateShipment(dtsShipment)
            .ContinueWith((t1) =>
            {
              if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
              {
                var result = t1.Result.Content.ReadAsStringAsync();
                if (t1.Result.StatusCode == HttpStatusCode.OK)
                {
                  Trace.WriteLine("Result received from Create Shipment " + result.Result);
                  dtsShipment.ID = Convert.ToInt32(result.Result);
                  retVal.Add(item.ShipmentId, dtsShipment); 
                }
                else
                {
                  Trace.WriteLine("Bad Request Error: Call to Create Shipment returned: " + result.Result);
                }
              }
              else
              {
                Trace.WriteLine("Error: Create shipment Api "+t1.Exception.InnerException.Message);
              }

            });
          tasks.Add(task);
        }
        Task.WaitAll(tasks.ToArray());
      }
      return retVal;
    }

    /// <summary>
    /// Get Despatch labware by shipment Id
    /// </summary>
    /// <param name="shipmentId">shipment id</param>
    /// <returns>List of <see cref="DespatchLabwareDto"/></returns>
    public List<DespatchLabwareDto> GetDespatchLabwareByShipmentId(int shipmentId)
    {
      return m_daDTSIntegrator.GetDespatchLabwareItemsForShipment(ProcInstanceId, shipmentId);
    }

    /// <summary>
    /// Parse despatch item to distribution groups and distribution for DTS service
    /// </summary>
    /// <param name="despatchLabwareDtos">List of DespatchLabware Data Object</param>
    /// <param name="dtsShipmentId">dts shipment id</param>
    public void ParseDespatchItemToDistributionGroup(List<DespatchLabwareDto> despatchLabwareDtos, int dtsShipmentId)
    {
      var tasks = new List<Task>();
      List<DTSDistributionGroup> retVal = new List<DTSDistributionGroup>();
      List<LabwareSampleDto> labwareSampleList = null;
      Trace.WriteLine("Processing of Despatch Labware Data with item count:" + despatchLabwareDtos.Count);
      var despatches = despatchLabwareDtos.Select(s =>new { s.DespatchId, s.ContainerLabwareClassId } ).Distinct();

      if (despatches != null && dtsShipmentId >= 0)
      {

        foreach (var despatch in despatches)
        {
          var despatchItems = despatchLabwareDtos.Where(f => f.DespatchId == despatch.DespatchId && f.ContainerLabwareClassId == despatch.ContainerLabwareClassId).ToList();
          var recipientAddress = new DTSAddress()
          {
            City = despatchItems[0].City,
            Country = despatchItems[0].Country,
            EMail = despatchItems[0].Email,
            Name = despatchItems[0].Recipient,
            Phone = despatchItems[0].Phone?? string.Empty ,
            State = despatchItems[0].State?? string.Empty,
            Street1 = despatchItems[0].AddressLine1 ?? string.Empty,
            Street2 = despatchItems[0].AddressLine2?? string.Empty ,
            Zip = despatchItems[0].PostalCode ?? string.Empty 
          };

          //Created Parent Distribution Group
          var parentDistributionGroup = new DTSDistributionGroup()
          {
            ContainerType = Enum.GetName(typeof(DTSContainerType),despatchItems[0].ContainerLabwareClassId).ToUpper(),
            RecipientAddress = recipientAddress,
            ShipmentId = dtsShipmentId,
            CreatedBy = despatchItems[0].CreatedBy
          };

          var task = m_dtsIntegratorApiHelper.CreateDistributionGroup(parentDistributionGroup).ContinueWith((t1) =>
          {
            if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
            {
              #region "Remodaling for Chemical, Distribution,child Distribution Group"

              var t1Result = t1.Result.Content.ReadAsStringAsync();

              if (t1.Result.StatusCode == HttpStatusCode.OK)
              {
                var labwareContainers = despatchItems.Select(s =>new { s.LabwareClassId, s.ContainerBarcode }).Distinct();
                foreach (var labwareContainer in labwareContainers)
                {
                  var labwareItem= despatchItems.Where(s =>s.LabwareClassId == labwareContainer.LabwareClassId && s.ContainerBarcode==labwareContainer.ContainerBarcode).ToList();

                  //Created child Distribution Group
                  var childDistributionGroup = new DTSDistributionGroup()
                  {
                    ContainerType = Enum.GetName(typeof(DTSContainerType), labwareItem[0].ContainerLabwareClassId == 3? labwareItem[0].LabwareClassId : labwareItem[0].ContainerLabwareClassId).ToUpper(),
                    RecipientAddress = recipientAddress,
                    Barcode = (labwareItem[0].ContainerLabwareClassId== (short)DTSContainerType.TUBE)?string.Empty:(labwareItem[0].ContainerLabwareClassId == 3? labwareItem[0].LabwareBarcode: labwareItem[0].ContainerBarcode),
                    SourceApplication = c_sourceApplication,
                    ShipmentId = dtsShipmentId,
                    ParentGroupId = Convert.ToInt32(t1Result.Result),
                    CreatedBy = labwareItem[0].CreatedBy
                  };

                  m_dtsIntegratorApiHelper.CreateDistributionGroup(childDistributionGroup).ContinueWith((t2) =>
                  {
                    if (t2.IsCompleted && !t2.IsFaulted && !t2.IsCanceled)
                    {
                      var t2Result = t2.Result.Content.ReadAsStringAsync();

                      if (t2.Result.StatusCode==HttpStatusCode.OK)
                      {
                        foreach (var labware in labwareItem)
                        {
                          labwareSampleList = m_daDTSIntegrator.GetSampleForLabwareItem(ProcInstanceId, labware.LabwareItemId);
                          DTSChemical chemical = null;
                          if (labwareSampleList != null && labwareSampleList.Count > 0)
                          {
                            foreach(LabwareSampleDto sample in labwareSampleList)
                            {
                              chemical = new DTSChemical()
                              {
                                Amount = sample.Amount,
                                AmountUnit = sample.AmountUnit ?? string.Empty,
                                Concentration = sample.Concentration,
                                ConcentrationUnit = sample.ConcentrationUnit ?? string.Empty,
                                ContainerColumn = sample.XPosition,
                                ContainerRow = sample.YPosition,
                                ContainerType = Enum.GetName(typeof(DTSContainerType), labware.LabwareClassId).ToUpper(),
                                RegistrationId = sample.SubstanceId,
                                Solvent = sample.Solvent,
                                IsWeighable = sample.IsWeighable,
                              };

                              //Created child Distribution 
                              var distribution = new DTSDistribution()
                              {
                                Barcode = labware.LabwareBarcode,
                                Chemical = chemical,
                                EntityType = chemical == null ? null : c_entityType,
                                RecipientAddress = recipientAddress,
                                DistributionGroupId = Convert.ToInt32(t2Result.Result),
                                CreatedBy = labware.CreatedBy
                              };

                              m_dtsIntegratorApiHelper.CreateDistribution(distribution).ContinueWith((t3) =>
                              {
                                if (t3.IsCompleted && !t3.IsFaulted && !t3.IsCanceled)
                                {
                                  var t3Result = t3.Result.Content.ReadAsStringAsync();
                                  if (t3.Result.StatusCode == HttpStatusCode.OK)
                                  {
                                    var distributionId = t3Result.Result;
                                    Trace.WriteLine("New Distribution is created with id:" + distributionId);
                                  }
                                  else
                                  {
                                    Trace.WriteLine("Bad Request Error: call to distribution api end with error message " + t3Result.Result);
                                  }
                                }
                                else
                                {
                                  Trace.WriteLine("Error: call to distribution api " + t3.Exception.InnerException.Message);
                                }

                              }, TaskContinuationOptions.AttachedToParent);

                            }
                          }
                          else
                          {
                            //Created child Distribution 
                            var distribution = new DTSDistribution()
                            {
                              Barcode = labware.LabwareBarcode,
                              Chemical = chemical,
                              EntityType = chemical == null ? null : c_entityType,
                              RecipientAddress = recipientAddress,
                              DistributionGroupId = Convert.ToInt32(t2Result.Result),
                              CreatedBy = labware.CreatedBy
                            };

                            m_dtsIntegratorApiHelper.CreateDistribution(distribution).ContinueWith((t3) =>
                            {
                              if (t3.IsCompleted && !t3.IsFaulted && !t3.IsCanceled)
                              {
                                var t3Result = t3.Result.Content.ReadAsStringAsync();
                                if (t3.Result.StatusCode == HttpStatusCode.OK)
                                {
                                  var distributionId = t3Result.Result;
                                  Trace.WriteLine("New Distribution is created with id:" + distributionId);
                                }
                                else
                                {
                                  Trace.WriteLine("Bad Request Error: call to distribution api end with error message " + t3Result.Result);
                                }
                              }
                              else
                              {
                                Trace.WriteLine("Error: call to distribution api " + t3.Exception.InnerException.Message);
                              }

                            }, TaskContinuationOptions.AttachedToParent);
                          }
                                                  
                        }
                      }
                      else
                      {
                        Trace.WriteLine("BadRequest Error: call to child distribution group api " + t2Result.Result);

                        Trace.WriteLine("call to parent distribution Group status api Begin");

                        DTSStatus dtsStatus = new DTSStatus {
                          DistributionGroupId =Convert.ToInt32(t1Result.Result),
                          Status=c_cancelStatus,
                          StatusDate=DateTime.Now
                        };
                        m_dtsIntegratorApiHelper.UpdateDistributionGroupStatus(dtsStatus).ContinueWith((tStatus) =>
                        {
                          if (tStatus.IsCompleted && !tStatus.IsFaulted && !tStatus.IsCanceled)
                          {
                            var tStatusResult = tStatus.Result.Content.ReadAsStringAsync();
                            if (tStatus.Result.StatusCode==HttpStatusCode.OK)
                            {
                              Trace.WriteLine("call to parent distribution Group status api successfully Ended ");
                            }
                            else
                            {
                              Trace.WriteLine("call to parent distribution Group status api Ended with Bad Request " + tStatusResult.Result);
                            }
                          }
                        },TaskContinuationOptions.AttachedToParent);
                      }
                    }
                    else
                    {
                      Trace.WriteLine("Error: call to child distribution group api " + t2.Exception.InnerException.Message);
                    }
                  }, TaskContinuationOptions.AttachedToParent);

                }
              }
              else
              {
                Trace.WriteLine("Error with BadRequest: call to parent distribution group Api " + t1Result.Result);
              }

              #endregion "Remodaling for Chemical, Distribution,child Distribution Group"
            }
            else
            {
              Trace.WriteLine("Error: call to parent distribution group Api " + t1.Exception.InnerException.Message);
            }
          }, TaskContinuationOptions.AttachedToParent);

          tasks.Add(task);

        }

        Task.WaitAll(tasks.ToArray());

      }
    }

    /// <summary>
    /// Get BMS tracking number from DTS service
    /// </summary>
    /// <param name="dtsShipmentId">DTS shipment id</param>
    /// <returns>BMS Tracking Number</returns>
    public string GetBMSTrackingNumber(int dtsShipmentId)
    {
      string retVal = string.Empty;
      Task task = m_dtsIntegratorApiHelper.GetShipment(dtsShipmentId).ContinueWith((t1) =>
      {
        if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
        {
          var result = t1.Result.Content.ReadAsStringAsync();
          var dtsShipment = JsonConvert.DeserializeObject<DTSShipment>(result.Result);
          retVal = dtsShipment.BMSTrackingNumber;
        }
        else
        {
          Trace.WriteLine("Error: call to get shipment Api " + t1.Exception.InnerException.Message);
        }
      });
      task.Wait();

      return retVal;
    }
  }
}
