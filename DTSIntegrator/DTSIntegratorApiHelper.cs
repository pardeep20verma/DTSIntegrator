//  Copyright © Titian Software Ltd
//
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Titian.BMS.Shipping.DTSIntegration.TasProd.DataModel;
using Titian.Configuration;
using Titian.Infrastructure.DataAccess;

namespace Titian.BMS.Shipping.DTSIntegration.TasProd
{
  /// <summary>
  /// class DTS Integrator Api Helper
  /// </summary>
  public class DTSIntegratorApiHelper : IDTSIntegratorApiHelper
  {
    const string c_dtsServerUrlkey = "DTSServer";
   

    const string c_postShipmentUrl = "Shipment/Post/";
    const string c_getShipmentUrl = "Shipment/Get/";
    const string c_postDistributionGroupUrl = "DistributionGroup/Post/";
    const string c_postDistributionUrl = "Distribution/Post/";
    const string c_postDistributionGroupStatusUrl = "DistributionGroupStatus/Post/";

    private HttpClient httpClient;


    /// <summary>
    /// Property for httpclient Instance
    /// </summary>
    public HttpClient HttpClient
    {
      get
      {
        if (httpClient == null)
        {
          ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
          httpClient = new HttpClient();
          httpClient.BaseAddress = new Uri(DTSServerUrl);
        }
        return httpClient;
      }
    }

    /// <summary>
    /// Property for DTS Server URL
    /// </summary>
    public string DTSServerUrl => ConfigEntry.GetString(
                  c_dtsServerUrlkey,
                  string.Empty);

    /// <summary>
    /// Create shipment method used to call the post api of DTS service to submit shipment
    /// </summary>
    /// <param name="dtsShipment">DTSShipment object</param>
    /// <returns>Task<HttpResponseMessage> object</returns>
    public Task<HttpResponseMessage> CreateShipment(DTSShipment dtsShipment)
    {
      var data = JsonConvert.SerializeObject(dtsShipment);
      Trace.WriteLine("Creating Shipment with data: " + data);
      return HttpClient.PostAsync(c_postShipmentUrl, new StringContent(data, Encoding.UTF8, "application/json"));
    }

    /// <summary>
    /// Get shipment method used to call get api of DTS service to get shipment object by shipment id
    /// </summary>
    /// <param name="shipmentId">shipment id</param>
    /// <returns>Task<HttpResponseMessage> object</returns>
    public Task<HttpResponseMessage> GetShipment(int shipmentId)
    {
      return HttpClient.GetAsync($"{ c_getShipmentUrl}{ shipmentId }");
    }

    /// <summary>
    /// Create Distribution Group method used to call post api of DTS service to submint distribution group object
    /// </summary>
    /// <param name="dTSDistributionGroup">DTSDistributionGroup object</param>
    /// <returns>Task<HttpResponseMessage> object</returns>
    public Task<HttpResponseMessage> CreateDistributionGroup(DTSDistributionGroup dTSDistributionGroup)
    {
      var data = JsonConvert.SerializeObject(dTSDistributionGroup);
      Trace.WriteLine("Creating Distribution Group with data: " + data);
      return HttpClient.PostAsync(c_postDistributionGroupUrl, new StringContent(data, Encoding.UTF8, "application/json"));
    }

    /// <summary>
    /// Create distritubtion method used to call post api of DTS service to submit distribution object
    /// </summary>
    /// <param name="dtsDistribution"> DTSDistribution object</param>
    /// <returns>Task<HttpResponseMessage> object</returns>
    public Task<HttpResponseMessage> CreateDistribution(DTSDistribution dtsDistribution)
    {
      var data = JsonConvert.SerializeObject(dtsDistribution);
      Trace.WriteLine("Creating Distribution with data: " + data);
      return HttpClient.PostAsync(c_postDistributionUrl, new StringContent(data, Encoding.UTF8, "application/json"));
    }

    /// <summary>
    /// Update Distribution Group Status method used to call the post api of DTS service to Update Distribution Group Status
    /// </summary>
    /// <param name="dtsStatus">DTSStatus object</param>
    /// <returns>Task<HttpResponseMessage> object</returns>
    public Task<HttpResponseMessage> UpdateDistributionGroupStatus(DTSStatus dtsStatus)
    {
      var data = JsonConvert.SerializeObject(dtsStatus);
      Trace.WriteLine("Updating Distribution group with data: " + data);
      return HttpClient.PostAsync(c_postDistributionGroupStatusUrl, new StringContent(data, Encoding.UTF8, "application/json"));
    }
  }
}
