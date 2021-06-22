//  Copyright © Titian Software Ltd
//
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Titian.BMS.Shipping.DTSIntegration.TasProd.DataAccess;
using Titian.BMS.Shipping.DTSIntegration.TasProd.DataModel;
using Titian.BMS.Shipping.DTSIntegration.TasProd.DataTransferObjects;
using Titian.Shipping.Constants;
using Titian.Shipping.DataAccess.DataTransferObject;
using Titian.Shipping.DataAccess.Interfaces;
using Titian.Shipping.DataAccess.Test;
using Assert = Titian.TestFramework.AssertExtensions.Assert;

namespace Titian.BMS.Shipping.DTSIntegration.TasProd.Test
{
  /// <summary>
  /// This is the test class for DTSIntegratorProcessor
  /// </summary>
  [TestClass]
  public class DTSIntegratorProcessorTest:TestShipmentBase
  {
    private static Mock<IDTSIntegratorApiHelper> mockDTSIntegratorApiHelper = null;
    private static Mock<IDADTSIntegrator> mockDADTSIntegrator = null;
    private static Mock<IDAShipment> mockDAShipment = null;
    private static DTSIntegratorProcessor dtsIntegratorProcessor = null;
    private static List<DTSShipment> s_DTSShipmentList = null;
    private static List<DTSDistribution> s_DTSDistributionList = null;
    private static List<DTSDistributionGroup> s_DTSDistributionGroupList = null;
    private static List<DespatchLabwareDto> s_DespatchLabwareData = null;
    

    /// <summary>
    /// Use MyClassInitialize to run code before all tests in a class have run
    /// </summary>
    /// <param name="testContext">testContext</param>
    [ClassInitialize]
    public static void MyClassInitialize(TestContext testContext)
    {
      mockDTSIntegratorApiHelper = new Mock<IDTSIntegratorApiHelper>();
      mockDADTSIntegrator = new Mock<IDADTSIntegrator>();
      mockDAShipment = new Mock<IDAShipment>();
      s_DTSShipmentList = GetDTSShipmentList();
      s_DTSDistributionList = GetDTSDistributionList();
      s_DTSDistributionGroupList = GetDTSDistributionGroupList();
      s_DespatchLabwareData = DespatchLabwareDataUtility.GetDespatchLabwareItems();
    }

    /// <summary>
    /// Test Method for CreateShipment with NULL DTSShipment Object
    /// </summary>
    [TestMethod]
    public void TestCreateShipment_NULLDTSShipmentObject()
    {
      // Arrange
      DTSShipment testDTSShipmentObj = null;
      string message = string.Empty;

      mockDTSIntegratorApiHelper.Setup(p => p.CreateShipment(It.IsAny<DTSShipment>())).Returns((DTSShipment dtsShipmentObj) =>
      {
        return CreateShipment(dtsShipmentObj);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, null);

      // Act
      var shipmentResult = dtsIntegratorProcessor.CreateShipment(testDTSShipmentObj);
      Task task = shipmentResult
            .ContinueWith((t1) =>
            {
              if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
              {
                var taskResult = t1.Result.Content.ReadAsStringAsync();
                message = taskResult.Result;
              }
            });
      task.Wait();

      // Assert
      Assert.IsTrue(shipmentResult.Result.StatusCode == HttpStatusCode.BadRequest, message);
    }

    /// <summary>
    /// Test Method for CreateShipment with empty Source Application
    /// </summary>
    [TestMethod]
    public void TestCreateShipment_EmptySourceApplication()
    {
      // Arrange
      DTSShipment testDTSShipmentObj = GetDTSShipmentWithEmptySource();
      string message = string.Empty;

      mockDTSIntegratorApiHelper.Setup(p => p.CreateShipment(It.IsAny<DTSShipment>())).Returns((DTSShipment dtsShipmentObj) =>
      {
        return CreateShipment(dtsShipmentObj);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, null);

      // Act
      var shipmentResult = dtsIntegratorProcessor.CreateShipment(testDTSShipmentObj);
      Task task = shipmentResult
            .ContinueWith((t1) =>
            {
              if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
              {
                var taskResult = t1.Result.Content.ReadAsStringAsync();
                message = taskResult.Result;
              }
            });
      task.Wait();

      // Assert
      Assert.IsTrue(shipmentResult.Result.StatusCode == HttpStatusCode.BadRequest, message);
    }

    /// <summary>
    /// Test Method for CreateShipment with invalid Shipment ID
    /// </summary>
    [TestMethod]
    public void TestCreateShipment_InvalidShipmentID()
    {
      // Arrange
      DTSShipment testDTSShipmentObj = GetDTSShipmentWithInvalidShipmentId();
      string message = string.Empty;

      mockDTSIntegratorApiHelper.Setup(p => p.CreateShipment(It.IsAny<DTSShipment>())).Returns((DTSShipment dtsShipmentObj) =>
      {
        return CreateShipment(dtsShipmentObj);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, null);

      // Act
      var shipmentResult = dtsIntegratorProcessor.CreateShipment(testDTSShipmentObj);
      Task task = shipmentResult
            .ContinueWith((t1) =>
            {
              if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
              {
                var taskResult = t1.Result.Content.ReadAsStringAsync();
                message = taskResult.Result;
              }
            });
      task.Wait();

      // Assert
      Assert.IsTrue(shipmentResult.Result.StatusCode == HttpStatusCode.BadRequest, message);
    }

    /// <summary>
    /// Test Method for CreateShipment with empty Title
    /// </summary>
    [TestMethod]
    public void TestCreateShipment_EmptyTitle()
    {
      // Arrange
      DTSShipment testDTSShipmentObj = GetDTSShipmentWithEmptyTitle();
      string message = string.Empty;

      mockDTSIntegratorApiHelper.Setup(p => p.CreateShipment(It.IsAny<DTSShipment>())).Returns((DTSShipment dtsShipmentObj) =>
      {
        return CreateShipment(dtsShipmentObj);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, null);

      // Act
      var shipmentResult = dtsIntegratorProcessor.CreateShipment(testDTSShipmentObj);
      Task task = shipmentResult
            .ContinueWith((t1) =>
            {
              if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
              {
                var taskResult = t1.Result.Content.ReadAsStringAsync();
                message = taskResult.Result;
              }
            });
      task.Wait();

      // Assert
      Assert.IsTrue(shipmentResult.Result.StatusCode == HttpStatusCode.BadRequest, message);
    }

    /// <summary>
    /// Test Method for CreateShipment with valid data
    /// </summary>
    [TestMethod]
    public void TestCreateShipment_ValidData()
    {
      // Arrange
      DTSShipment testDTSShipmentObj = GetDTSShipmentObject(6);
      string message = string.Empty;

      mockDTSIntegratorApiHelper.Setup(p => p.CreateShipment(It.IsAny<DTSShipment>())).Returns((DTSShipment dtsShipmentObj) =>
      {
        return CreateShipment(dtsShipmentObj);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, null);

      // Act
      var shipmentResult = dtsIntegratorProcessor.CreateShipment(testDTSShipmentObj);
      Task task = shipmentResult
            .ContinueWith((t1) =>
            {
              if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
              {
                var taskResult = t1.Result.Content.ReadAsStringAsync();
                message = taskResult.Result;
              }
            });
      task.Wait();

      // Assert
      message = shipmentResult.Result.Content.ReadAsStringAsync().Result;
      Assert.IsTrue(shipmentResult.Result.StatusCode == HttpStatusCode.OK, "Shipment created with BMS tracking number : " + message + ".");
      Assert.IsTrue(Convert.ToInt32(message) > 0, "Shipment created with valid BMS tracking number.");
    }

    /// <summary>
    /// Test Method for CreateDistributionGroup with NULL DTSDistributionGroup Object
    /// </summary>
    [TestMethod]
    public void TestCreateDistributionGroup_NULLDTSDistributionGroupObject()
    {
      // Arrange
      DTSDistributionGroup testDTSDistributionGroupObj = null;
      string message = string.Empty;

      mockDTSIntegratorApiHelper.Setup(p => p.CreateDistributionGroup(It.IsAny<DTSDistributionGroup>())).Returns((DTSDistributionGroup dtsDistributionGroupObj) =>
      {
        return CreateDistributionGroup(dtsDistributionGroupObj);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, null);

      // Act
      var distributionGroupResult = dtsIntegratorProcessor.CreateDistributionGroup(testDTSDistributionGroupObj);
      Task task = distributionGroupResult
            .ContinueWith((t1) =>
            {
              if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
              {
                var taskResult = t1.Result.Content.ReadAsStringAsync();
                message = taskResult.Result;
              }
            });
      task.Wait();

      // Assert
      Assert.IsTrue(distributionGroupResult.Result.StatusCode == HttpStatusCode.BadRequest, message);
    }

    /// <summary>
    /// Test Method for CreateDistributionGroup with invalid ID
    /// </summary>
    [TestMethod]
    public void TestCreateDistributionGroup_InvalidID()
    {
      // Arrange
      DTSDistributionGroup testDTSDistributionGroupObj = GetDTSDistributionGroupWithInvalidId(6);
      string message = string.Empty;

      mockDTSIntegratorApiHelper.Setup(p => p.CreateDistributionGroup(It.IsAny<DTSDistributionGroup>())).Returns((DTSDistributionGroup dtsDistributionGroupObj) =>
      {
        return CreateDistributionGroup(dtsDistributionGroupObj);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, null);

      // Act
      var distributionGroupResult = dtsIntegratorProcessor.CreateDistributionGroup(testDTSDistributionGroupObj);
      Task task = distributionGroupResult
            .ContinueWith((t1) =>
            {
              if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
              {
                var taskResult = t1.Result.Content.ReadAsStringAsync();
                message = taskResult.Result;
              }
            });
      task.Wait();

      // Assert
      Assert.IsTrue(distributionGroupResult.Result.StatusCode == HttpStatusCode.BadRequest, message);
    }

    /// <summary>
    /// Test Method for CreateDistributionGroup with valid data
    /// </summary>
    [TestMethod]
    public void TestCreateDistributionGroup_ValidData()
    {
      // Arrange
      DTSDistributionGroup testDTSDistributionGroupObj = GetDTSDistributionGroupObject(6);
      string message = string.Empty;

      mockDTSIntegratorApiHelper.Setup(p => p.CreateDistributionGroup(It.IsAny<DTSDistributionGroup>())).Returns((DTSDistributionGroup dtsDistributionGroupObj) =>
      {
        return CreateDistributionGroup(dtsDistributionGroupObj);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, null);

      // Act
      var distributionGroupResult = dtsIntegratorProcessor.CreateDistributionGroup(testDTSDistributionGroupObj);
      Task task = distributionGroupResult
            .ContinueWith((t1) =>
            {
              if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
              {
                var taskResult = t1.Result.Content.ReadAsStringAsync();
                message = taskResult.Result;
              }
            });
      task.Wait();

      // Assert
      Assert.IsTrue(distributionGroupResult.Result.StatusCode == HttpStatusCode.OK, "Distribution Group created with Id: " + message);
      Assert.IsTrue(Convert.ToInt32(message) > 0, "Valid Distribution group created.");
    }

    /// <summary>
    /// Test Method for CreateDistribution with NULL DTSDistribution Object
    /// </summary>
    [TestMethod]
    public void TestCreateDistribution_NULLDTSDistributionObject()
    {
      // Arrange
      DTSDistribution testDTSDistributionObj = null;
      string message = string.Empty;

      mockDTSIntegratorApiHelper.Setup(p => p.CreateDistribution(It.IsAny<DTSDistribution>())).Returns((DTSDistribution dtsDistributionObj) =>
      {
        return CreateDistribution(dtsDistributionObj);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, null);

      // Act
      var distributionResult = dtsIntegratorProcessor.CreateDistribution(testDTSDistributionObj);
      Task task = distributionResult
            .ContinueWith((t1) =>
            {
              if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
              {
                var taskResult = t1.Result.Content.ReadAsStringAsync();
                message = taskResult.Result;
              }
            });
      task.Wait();

      // Assert
      Assert.IsTrue(distributionResult.Result.StatusCode == HttpStatusCode.BadRequest, message);
    }

    /// <summary>
    /// Test Method for CreateDistribution with invalid ID
    /// </summary>
    [TestMethod]
    public void TestCreateDistribution_InvalidID()
    {
      // Arrange
      DTSDistribution testDTSDistributionObj = GetDTSDistributionWithInvalidId(6);
      string message = string.Empty;

      mockDTSIntegratorApiHelper.Setup(p => p.CreateDistribution(It.IsAny<DTSDistribution>())).Returns((DTSDistribution dtsDistributionObj) =>
      {
        return CreateDistribution(dtsDistributionObj);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, null);

      // Act
      var distributionResult = dtsIntegratorProcessor.CreateDistribution(testDTSDistributionObj);
      Task task = distributionResult
            .ContinueWith((t1) =>
            {
              if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
              {
                var taskResult = t1.Result.Content.ReadAsStringAsync();
                message = taskResult.Result;
              }
            });
      task.Wait();

      // Assert
      Assert.IsTrue(distributionResult.Result.StatusCode == HttpStatusCode.BadRequest, message);
    }

    /// <summary>
    /// Test Method for CreateDistribution with empty barcode
    /// </summary>
    [TestMethod]
    public void TestCreateDistribution_EmptyBarcode()
    {
      // Arrange
      DTSDistribution testDTSDistributionObj = GetDTSDistributionWithEmptyBarcode(6);
      string message = string.Empty;

      mockDTSIntegratorApiHelper.Setup(p => p.CreateDistribution(It.IsAny<DTSDistribution>())).Returns((DTSDistribution dtsDistributionObj) =>
      {
        return CreateDistribution(dtsDistributionObj);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, null);

      // Act
      var distributionResult = dtsIntegratorProcessor.CreateDistribution(testDTSDistributionObj);
      Task task = distributionResult
            .ContinueWith((t1) =>
            {
              if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
              {
                var taskResult = t1.Result.Content.ReadAsStringAsync();
                message = taskResult.Result;
              }
            });
      task.Wait();

      // Assert
      Assert.IsTrue(distributionResult.Result.StatusCode == HttpStatusCode.BadRequest, message);
    }

    /// <summary>
    /// Test Method for CreateDistribution with valid data
    /// </summary>
    [TestMethod]
    public void TestCreateDistribution_ValidData()
    {
      // Arrange
      DTSDistribution testDTSDistributionObj = GetDTSDistributionObject(6);
      string message = string.Empty;

      mockDTSIntegratorApiHelper.Setup(p => p.CreateDistribution(It.IsAny<DTSDistribution>())).Returns((DTSDistribution dtsDistributionObj) =>
      {
        return CreateDistribution(dtsDistributionObj);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, null);

      // Act
      var distributionResult = dtsIntegratorProcessor.CreateDistribution(testDTSDistributionObj);
      Task task = distributionResult
            .ContinueWith((t1) =>
            {
              if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
              {
                var taskResult = t1.Result.Content.ReadAsStringAsync();
                message = taskResult.Result;
              }
            });
      task.Wait();

      // Assert
      Assert.IsTrue(distributionResult.Result.StatusCode == HttpStatusCode.OK, "Distribution created with Id: " + message);
      Assert.IsTrue(Convert.ToInt32(message) > 0, "Valid Distribution created.");
    }

    /// <summary>
    /// Test Method for GetShipment with invalid Shipment Id
    /// </summary>
    [TestMethod]
    public void TestGetShipment_InvalidShipmentId()
    {
      // Arrange
      int testShipmentId = -1;
      string message = string.Empty;

      mockDTSIntegratorApiHelper.Setup(p => p.GetShipment(It.IsAny<int>())).Returns((int shipmentId) =>
      {
        return GetShipment(shipmentId);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, null);

      // Act
      var shipmentResult = dtsIntegratorProcessor.GetShipment(testShipmentId);
      Task task = shipmentResult
            .ContinueWith((t1) =>
            {
              if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
              {
                var taskResult = t1.Result.Content.ReadAsStringAsync();
                message = taskResult.Result;
              }
            });
      task.Wait();

      // Assert
      Assert.IsTrue(shipmentResult.Result.StatusCode == HttpStatusCode.BadRequest, "Shipment Id can not be invalid.");
    }

    /// <summary>
    /// Test Method for GetShipment with Shipment Id not exists
    /// </summary>
    [TestMethod]
    public void TestGetShipment_ShipmentId_NotExists()
    {
      // Arrange
      int testShipmentId = 1001;
      string message = string.Empty;

      mockDTSIntegratorApiHelper.Setup(p => p.GetShipment(It.IsAny<int>())).Returns((int shipmentId) =>
      {
        return GetShipment(shipmentId);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, null);

      // Act
      var shipmentResult = dtsIntegratorProcessor.GetShipment(testShipmentId);
      Task task = shipmentResult
            .ContinueWith((t1) =>
            {
              if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
              {
                var taskResult = t1.Result.Content.ReadAsStringAsync();
                message = taskResult.Result;
              }
            });
      task.Wait();

      // Assert
      Assert.IsTrue(shipmentResult.Result.StatusCode == HttpStatusCode.BadRequest, "Data does not exists for shipment id: " + testShipmentId);
    }

    /// <summary>
    /// Test Method for GetShipment with Shipment Id exists
    /// </summary>
    [TestMethod]
    public void TestGetShipment_ShipmentId_Exists()
    {
      // Arrange
      int testShipmentId = 1;
      string message = string.Empty;

      mockDTSIntegratorApiHelper.Setup(p => p.GetShipment(It.IsAny<int>())).Returns((int shipmentId) =>
      {
        return GetShipment(shipmentId);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, null);

      // Act
      var shipmentResult = dtsIntegratorProcessor.GetShipment(testShipmentId);
      Task task = shipmentResult
            .ContinueWith((t1) =>
            {
              if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
              {
                var taskResult = t1.Result.Content.ReadAsStringAsync();
                message = taskResult.Result;
              }
            });
      task.Wait();

      // Assert
      DTSShipment dtsShipment = JsonConvert.DeserializeObject<DTSShipment>(message);
      Assert.IsTrue(shipmentResult.Result.StatusCode == HttpStatusCode.OK, "Data exists for Shipment Id :" + testShipmentId);
      Assert.IsTrue(dtsShipment != null && dtsShipment.ID == testShipmentId, "Valid data exists for Shipment Id :" + testShipmentId);
    }

    /// <summary>
    /// Test Method for GetBMSTrackingNumber with invalid Shipment Id
    /// </summary>
    [TestMethod]
    public void TestGetBMSTrackingNumber_InvalidShipmentId()
    {
      // Arrange
      int testShipmentId = -1;
      string message = string.Empty;

      mockDTSIntegratorApiHelper.Setup(p => p.GetShipment(It.IsAny<int>())).Returns((int shipmentId) =>
      {
        return GetShipment(shipmentId);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, null);

      // Act
      string bmsTrackingNumber = dtsIntegratorProcessor.GetBMSTrackingNumber(testShipmentId);

      // Assert
      Assert.IsTrue(string.IsNullOrEmpty(bmsTrackingNumber), "Shipment Id can not be invalid.");
    }

    /// <summary>
    /// Test Method for GetBMSTrackingNumber with Shipment Id not exists
    /// </summary>
    [TestMethod]
    public void TestGetBMSTrackingNumber_ShipmentId_NotExists()
    {
      // Arrange
      int testShipmentId = 1001;
      string message = string.Empty;

      mockDTSIntegratorApiHelper.Setup(p => p.GetShipment(It.IsAny<int>())).Returns((int shipmentId) =>
      {
        return GetShipment(shipmentId);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, null);

      // Act
      string bmsTrackingNumber = dtsIntegratorProcessor.GetBMSTrackingNumber(testShipmentId);

      // Assert
      Assert.IsTrue(string.IsNullOrEmpty(bmsTrackingNumber) && testShipmentId >= 0, "Data does not exists for Shipment Id :" + testShipmentId);
    }

    /// <summary>
    /// Test Method for GetBMSTrackingNumber with Shipment Id exists
    /// </summary>
    [TestMethod]
    public void TestGetBMSTrackingNumber_ShipmentId_Exists()
    {
      // Arrange
      int testShipmentId = 1;
      string message = string.Empty;

      mockDTSIntegratorApiHelper.Setup(p => p.GetShipment(It.IsAny<int>())).Returns((int shipmentId) =>
      {
        return GetShipment(shipmentId);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, null);

      // Act
      string bmsTrackingNumber = dtsIntegratorProcessor.GetBMSTrackingNumber(testShipmentId);

      // Assert
      Assert.IsTrue(!string.IsNullOrEmpty(bmsTrackingNumber), "Data exists for Shipment Id :" + testShipmentId);
    }

    /// <summary>
    /// Test Method for GetListOfInternalShipment with valid data
    /// </summary>
    [TestMethod]
    public void TestGetListOfInternalShipment_ValidData()
    {
      // Arrange
      int testProcInstanceId = ProcInstanceId;
      int[] testShipmentStateId = { (int)ShipmentState.Complete };
      var expectedShipment = ShipmentList.Where(f=> testShipmentStateId.Contains(f.StateId) && string.IsNullOrEmpty(f.ExternalReference)).ToList();
      mockDAShipment.Setup(p => p.GetShipmentByStates(It.IsAny<int>(), It.IsAny<int[]>())).Returns((int procInstanceId, int[] shipmentStates) =>
      {
        return GetShipmentByStates(testProcInstanceId, testShipmentStateId);
      });
      mockDAShipment.Setup(p => p.GetZoneAddressByZoneId(It.IsAny<int>(), It.IsAny<long>())).Returns((int procInstanceId, long zoneId) => 
      {
        return GetZoneAddressByZoneId(testProcInstanceId, zoneId);
      });
      mockDTSIntegratorApiHelper.Setup(p => p.CreateShipment(It.IsAny<DTSShipment>())).Returns((DTSShipment dtsShipmentObj) =>
      {
        return CreateShipment(dtsShipmentObj);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, mockDAShipment.Object);
      // Act
      var actualShipment = dtsIntegratorProcessor.GetListOfInternalShipment();

      // Assert
      Assert.IsTrue(actualShipment.Count > 0, "Shipment list returned with item count: " + actualShipment.Count);
      Assert.AreEqual(expectedShipment.Count, actualShipment.Count, "Expected shipment count are not the same with Actual shipment count");
      Assert.IsTrue(IsActualShipmentValid(expectedShipment, actualShipment), "Actual shipment are not valid");
      Assert.IsFalse(actualShipment.Values.Any(p => p == null), "One or more Actual shipments are Null");
    }

    /// <summary>
    /// Test Method for GetListOfInternalShipment with empty data
    /// </summary>
    [TestMethod]
    public void TestGetListOfInternalShipment_EmptyData()
    {
      // Arrange
      int testProcInstanceId = ProcInstanceId;
      int[] testShipmentStateId = { };

      mockDAShipment.Setup(p => p.GetShipmentByStates(It.IsAny<int>(), It.IsAny<int[]>())).Returns((int procInstanceId, int[] shipmentStates) =>
      {
        return GetShipmentByStates(testProcInstanceId, testShipmentStateId);
      });
      mockDAShipment.Setup(p => p.GetZoneAddressByZoneId(It.IsAny<int>(), It.IsAny<long>())).Returns((int procInstanceId, long zoneId) =>
      {
        return GetZoneAddressByZoneId(testProcInstanceId, zoneId);
      });
      mockDTSIntegratorApiHelper.Setup(p => p.CreateShipment(It.IsAny<DTSShipment>())).Returns((DTSShipment dtsShipmentObj) =>
      {
        return CreateShipment(dtsShipmentObj);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, mockDAShipment.Object);

      // Act
      var result = dtsIntegratorProcessor.GetListOfInternalShipment();

      // Assert
      Assert.IsTrue(result.Count == 0, "Shipment data does not returned for empty shipment state.");
    }

    /// <summary>
    /// Test Method for GetListOfInternalShipment with data not exists
    /// </summary>
    [TestMethod]
    public void TestGetListOfInternalShipment_DataNotExists()
    {
      // Arrange
      int testProcInstanceId = ProcInstanceId;
      int[] testShipmentStateId = { (int)ShipmentState.InTransit };

      mockDAShipment.Setup(p => p.GetShipmentByStates(It.IsAny<int>(), It.IsAny<int[]>())).Returns((int procInstanceId, int[] shipmentStates) =>
      {
        return GetShipmentByStates(testProcInstanceId, testShipmentStateId);
      });
      mockDAShipment.Setup(p => p.GetZoneAddressByZoneId(It.IsAny<int>(), It.IsAny<long>())).Returns((int procInstanceId, long zoneId) =>
      {
        return GetZoneAddressByZoneId(testProcInstanceId, zoneId);
      });
      mockDTSIntegratorApiHelper.Setup(p => p.CreateShipment(It.IsAny<DTSShipment>())).Returns((DTSShipment dtsShipmentObj) =>
      {
        return CreateShipment(dtsShipmentObj);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, mockDAShipment.Object);

      // Act
      var result = dtsIntegratorProcessor.GetListOfInternalShipment();

      // Assert
      Assert.IsTrue(result.Count == 0, "Shipment data does not returned for shipment state not exists in database.");
    }

    /// <summary>
    /// Test Method for GetDespatchLabwareByShipmentId with invalid Shipment Id
    /// </summary>
    [TestMethod]
    public void TestGetDespatchLabwareByShipmentId_InvalidShipmentId()
    {
      // Arrange
      int testShipmentId = -1;

      mockDADTSIntegrator.Setup(p => p.GetDespatchLabwareItemsForShipment(It.IsAny<int>(), It.IsAny<int>())).Returns((int procInstanceId, int shipmentId) =>
      {
        return GetDespatchLabwareByShipmentId(shipmentId);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(null, mockDADTSIntegrator.Object, null);

      // Act
      var result = dtsIntegratorProcessor.GetDespatchLabwareByShipmentId(testShipmentId);

      // Assert
      Assert.IsNull(result, "Shipment Id should not be invalid.");
    }

    /// <summary>
    /// Test Method for GetDespatchLabwareByShipmentId Shipment Id not exists
    /// </summary>
    [TestMethod]
    public void TestGetDespatchLabwareByShipmentId_ShipmentIdNotExists()
    {
      // Arrange
      int testShipmentId = 1001;

      mockDADTSIntegrator.Setup(p => p.GetDespatchLabwareItemsForShipment(It.IsAny<int>(), It.IsAny<int>())).Returns((int procInstanceId, int shipmentId) =>
      {
        return GetDespatchLabwareByShipmentId(shipmentId);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(null, mockDADTSIntegrator.Object, null);

      // Act
      var result = dtsIntegratorProcessor.GetDespatchLabwareByShipmentId(testShipmentId);

      // Assert
      Assert.IsNull(result, "Data does not exists for shipment id : " + testShipmentId);
    }

    /// <summary>
    /// Test Method for GetDespatchLabwareByShipmentId with Shipment Id exists
    /// </summary>
    [TestMethod]
    public void TestGetDespatchLabwareByShipmentId_ShipmentIdExists()
    {
      // Arrange
      int testShipmentId = 1;

      mockDADTSIntegrator.Setup(p => p.GetDespatchLabwareItemsForShipment(It.IsAny<int>(), It.IsAny<int>())).Returns((int procInstanceId, int shipmentId) =>
      {
        return GetDespatchLabwareByShipmentId(shipmentId);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(null, mockDADTSIntegrator.Object, null);

      // Act
      var result = dtsIntegratorProcessor.GetDespatchLabwareByShipmentId(testShipmentId);

      // Assert
      Assert.IsNotNull(result, "Data exists for shipment id : " + testShipmentId);
    }

    /// <summary>
    /// Test Method for UpdateDistributionGroupStatus with NULL DTSStatus Object
    /// </summary>
    [TestMethod]
    public void TestUpdateDistributionGroupStatus_NULLDTSStatusObject()
    {
      // Arrange
      DTSStatus testDTSStatusObj = null;
      string message = string.Empty;

      mockDTSIntegratorApiHelper.Setup(p => p.UpdateDistributionGroupStatus(It.IsAny<DTSStatus>())).Returns((DTSStatus dtsStatusObj) =>
      {
        return UpdateDistributionGroupStatus(dtsStatusObj);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, null);

      // Act
      var statusResult = dtsIntegratorProcessor.UpdateDistributionGroupStatus(testDTSStatusObj);
      Task task = statusResult
            .ContinueWith((t1) =>
            {
              if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
              {
                var taskResult = t1.Result.Content.ReadAsStringAsync();
                message = taskResult.Result;
              }
            });
      task.Wait();

      // Assert
      Assert.IsTrue(statusResult.Result.StatusCode == HttpStatusCode.BadRequest, message);
    }

    /// <summary>
    /// Test Method for UpdateDistributionGroupStatus with Distribution Group not exists
    /// </summary>
    [TestMethod]
    public void TestUpdateDistributionGroupStatus_DistributionGroupNotExists()
    {
      // Arrange
      DTSStatus testDTSStatusObj = GetDTSStatusObject(1001);
      string message = string.Empty;

      mockDTSIntegratorApiHelper.Setup(p => p.UpdateDistributionGroupStatus(It.IsAny<DTSStatus>())).Returns((DTSStatus dtsStatusObj) =>
      {
        return UpdateDistributionGroupStatus(dtsStatusObj);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, null);

      // Act
      var statusResult = dtsIntegratorProcessor.UpdateDistributionGroupStatus(testDTSStatusObj);
      Task task = statusResult
            .ContinueWith((t1) =>
            {
              if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
              {
                var taskResult = t1.Result.Content.ReadAsStringAsync();
                message = taskResult.Result;
              }
            });
      task.Wait();

      // Assert
      Assert.IsTrue(statusResult.Result.StatusCode == HttpStatusCode.BadRequest, message);
    }

    /// <summary>
    /// Test Method for UpdateDistributionGroupStatus with Distribution Group exists
    /// </summary>
    [TestMethod]
    public void TestUpdateDistributionGroupStatus_DistributionGroupExists()
    {
      // Arrange
      DTSStatus testDTSStatusObj = GetDTSStatusObject(1);
      string message = string.Empty;

      mockDTSIntegratorApiHelper.Setup(p => p.UpdateDistributionGroupStatus(It.IsAny<DTSStatus>())).Returns((DTSStatus dtsStatusObj) =>
      {
        return UpdateDistributionGroupStatus(dtsStatusObj);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, null);

      // Act
      var statusResult = dtsIntegratorProcessor.UpdateDistributionGroupStatus(testDTSStatusObj);
      Task task = statusResult
            .ContinueWith((t1) =>
            {
              if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
              {
                var taskResult = t1.Result.Content.ReadAsStringAsync();
                message = taskResult.Result;
              }
            });
      task.Wait();

      // Assert
      DTSDistributionGroup updatedGroup = s_DTSDistributionGroupList.FirstOrDefault(p => p.ID == testDTSStatusObj.DistributionGroupId);
      Assert.IsTrue(statusResult.Result.StatusCode == HttpStatusCode.OK, message);
      Assert.IsTrue(updatedGroup.Status == testDTSStatusObj.Status, "Data successfully updated.");
    }

    /// <summary>
    /// Test Method for ParseDespatchItemToDistributionGroup with valid data
    /// </summary>
    [TestMethod]
    public void TestParseDespatchItemToDistributionGroup_ValidData()
    {
      // Arrange
      List<DespatchLabwareDto> testDespatchLabwareData = DespatchLabwareDataUtility.GetDespatchLabwareItems();
      int testDTSShipmentId = 1;
      int testInitialDistributionGroupCount = 0;
      int testFinalDistributionGroupCount = 0;
      s_DTSDistributionGroupList.Clear();
      s_DTSDistributionList.Clear();
      testInitialDistributionGroupCount = s_DTSDistributionGroupList.Count;

      mockDTSIntegratorApiHelper.Setup(p => p.CreateDistributionGroup(It.IsAny<DTSDistributionGroup>())).Returns((DTSDistributionGroup dtsDistributionGroupObj) =>
      {
        return CreateDistributionGroup(dtsDistributionGroupObj);
      });
      mockDTSIntegratorApiHelper.Setup(p => p.CreateDistribution(It.IsAny<DTSDistribution>())).Returns((DTSDistribution dtsDistributionObj) =>
      {
        return CreateDistribution(dtsDistributionObj);
      });
      mockDTSIntegratorApiHelper.Setup(p => p.UpdateDistributionGroupStatus(It.IsAny<DTSStatus>())).Returns((DTSStatus dtsStatusObj) =>
      {
        return UpdateDistributionGroupStatus(dtsStatusObj);
      });
      

      mockDADTSIntegrator.Setup(p => p.GetSampleForLabwareItem(It.IsAny<int>(), It.IsAny<int>())).Returns((int procInstanceId, int labwareItemId) =>
      {
        return DADTSIntegratorTest.GetSampleForLabwareItem(procInstanceId, labwareItemId);
      });

      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object,mockDADTSIntegrator.Object, null);
      // Act
      dtsIntegratorProcessor.ParseDespatchItemToDistributionGroup(testDespatchLabwareData, testDTSShipmentId);
      testFinalDistributionGroupCount = s_DTSDistributionGroupList.Count;

      // Assert
      Assert.IsTrue(testFinalDistributionGroupCount > 0 && testInitialDistributionGroupCount < testFinalDistributionGroupCount, "Data successfully parsed.");
      Assert.IsTrue(s_DTSDistributionGroupList.All(p => p.ShipmentId == testDTSShipmentId), "Data is correctly parsed.");
    }

    /// <summary>
    /// Test Method for ParseDespatchItemToDistributionGroup with null Despatch Labware Data Set
    /// </summary>
    [TestMethod]
    public void TestParseDespatchItemToDistributionGroup_NULLDespatchLabwareDataSet()
    {
      // Arrange
      List<DespatchLabwareDto> testDespatchLabwareData = null;
      int testDTSShipmentId = 1;
      int testInitialDistributionGroupCount = 0;
      s_DTSDistributionGroupList.Clear();
      s_DTSDistributionList.Clear();
      testInitialDistributionGroupCount = s_DTSDistributionGroupList.Count;

      mockDTSIntegratorApiHelper.Setup(p => p.CreateDistributionGroup(It.IsAny<DTSDistributionGroup>())).Returns((DTSDistributionGroup dtsDistributionGroupObj) =>
      {
        return CreateDistributionGroup(dtsDistributionGroupObj);
      });
      mockDTSIntegratorApiHelper.Setup(p => p.CreateDistribution(It.IsAny<DTSDistribution>())).Returns((DTSDistribution dtsDistributionObj) =>
      {
        return CreateDistribution(dtsDistributionObj);
      });
      mockDTSIntegratorApiHelper.Setup(p => p.UpdateDistributionGroupStatus(It.IsAny<DTSStatus>())).Returns((DTSStatus dtsStatusObj) =>
      {
        return UpdateDistributionGroupStatus(dtsStatusObj);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, null);

      // Act
      Action act = () => dtsIntegratorProcessor.ParseDespatchItemToDistributionGroup(testDespatchLabwareData, testDTSShipmentId);

      // Assert
      Assert.Throws<NullReferenceException>(act);
    }

    /// <summary>
    /// Test Method for ParseDespatchItemToDistributionGroup with Empty Despatch Labweare Data Set
    /// </summary>
    [TestMethod]
    public void TestParseDespatchItemToDistributionGroup_EmptyDespatchLabweareDataSet()
    {
      // Arrange
      List<DespatchLabwareDto> testDespatchLabwareData = new List<DespatchLabwareDto>();
      int testDTSShipmentId = 1;
      s_DTSDistributionGroupList.Clear();
      s_DTSDistributionList.Clear();

      mockDTSIntegratorApiHelper.Setup(p => p.CreateDistributionGroup(It.IsAny<DTSDistributionGroup>())).Returns((DTSDistributionGroup dtsDistributionGroupObj) =>
      {
        return CreateDistributionGroup(dtsDistributionGroupObj);
      });
      mockDTSIntegratorApiHelper.Setup(p => p.CreateDistribution(It.IsAny<DTSDistribution>())).Returns((DTSDistribution dtsDistributionObj) =>
      {
        return CreateDistribution(dtsDistributionObj);
      });
      mockDTSIntegratorApiHelper.Setup(p => p.UpdateDistributionGroupStatus(It.IsAny<DTSStatus>())).Returns((DTSStatus dtsStatusObj) =>
      {
        return UpdateDistributionGroupStatus(dtsStatusObj);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, null);

      // Act
      dtsIntegratorProcessor.ParseDespatchItemToDistributionGroup(testDespatchLabwareData, testDTSShipmentId);

      // Assert
      Assert.IsTrue(s_DTSDistributionGroupList.Count == 0, "Despatch labware dataset can not be empty.");
    }

    /// <summary>
    /// Test Method for ParseDespatchItemToDistributionGroup with invalid DTS Shipment Id
    /// </summary>
    [TestMethod]
    public void TestParseDespatchItemToDistributionGroup_InvalidDTSShipmentId()
    {
      // Arrange
      List<DespatchLabwareDto> testDespatchLabwareDataSet = DespatchLabwareDataUtility.GetDespatchLabwareItems();
      int testDTSShipmentId = -1;
      s_DTSDistributionGroupList.Clear();
      s_DTSDistributionList.Clear();

      mockDTSIntegratorApiHelper.Setup(p => p.CreateDistributionGroup(It.IsAny<DTSDistributionGroup>())).Returns((DTSDistributionGroup dtsDistributionGroupObj) =>
      {
        return CreateDistributionGroup(dtsDistributionGroupObj);
      });
      mockDTSIntegratorApiHelper.Setup(p => p.CreateDistribution(It.IsAny<DTSDistribution>())).Returns((DTSDistribution dtsDistributionObj) =>
      {
        return CreateDistribution(dtsDistributionObj);
      });
      mockDTSIntegratorApiHelper.Setup(p => p.UpdateDistributionGroupStatus(It.IsAny<DTSStatus>())).Returns((DTSStatus dtsStatusObj) =>
      {
        return UpdateDistributionGroupStatus(dtsStatusObj);
      });
      dtsIntegratorProcessor = new DTSIntegratorProcessor(mockDTSIntegratorApiHelper.Object, null, null);

      // Act
      dtsIntegratorProcessor.ParseDespatchItemToDistributionGroup(testDespatchLabwareDataSet, testDTSShipmentId);

      // Assert
      Assert.IsTrue(s_DTSDistributionGroupList.Count == 0, "dts Shipment Id can not be invalid.");
    }

    //Use ClassCleanup to run code after all tests in a class have run
    [ClassCleanup]
    public static void MyClassCleanup()
    {
      mockDTSIntegratorApiHelper = null;
      mockDADTSIntegrator = null;
      mockDAShipment = null;
      dtsIntegratorProcessor = null;
      s_DTSShipmentList = null;
      s_DTSDistributionList = null;
      s_DTSDistributionGroupList = null;
      s_DespatchLabwareData = null;
    }

    /// <summary>
    /// Method is used to create Shipment
    /// </summary>
    /// <param name="dtsShipmentObj">dtsShipmentObj</param>
    /// <returns>Returns Task<HttpResponseMessage> object</returns>
    private Task<HttpResponseMessage> CreateShipment(DTSShipment dtsShipmentObj)
    {
      HttpResponseMessage response = new HttpResponseMessage();
      if (dtsShipmentObj == null)
      {
        response.StatusCode = HttpStatusCode.BadRequest;
        response.Content = new StringContent("DTSShipment object cannot be null.");
      }
      else
      {
        if (dtsShipmentObj.SourceApplication == string.Empty)
        {
          response.StatusCode = HttpStatusCode.BadRequest;
          response.Content = new StringContent("Source Application name cannot be empty within DTSShipment object.");
        }
        else if (dtsShipmentObj.ID < 0)
        {
          response.StatusCode = HttpStatusCode.BadRequest;
          response.Content = new StringContent("Shipment ID cannot be invalid value within DTSShipment object.");
        }
        else if (dtsShipmentObj.Title == string.Empty)
        {
          response.StatusCode = HttpStatusCode.BadRequest;
          response.Content = new StringContent("Title name cannot be empty within DTSShipment object.");
        }
        else
        {
          response.StatusCode = HttpStatusCode.OK;
          response.Content = new StringContent(AddShipmentIntoList(dtsShipmentObj).ToString());
        }
      }
      return Task.FromResult(response);
    }

    /// <summary>
    /// Method is used to get shipment based on shipment id
    /// </summary>
    /// <param name="shipmentId">shipmentId</param>
    /// <returns>Returns Task<HttpResponseMessage> object</returns>h
    private Task<HttpResponseMessage> GetShipment(int shipmentId)
    {
      HttpResponseMessage response = new HttpResponseMessage();
      DTSShipment dtsShipment = null;
      string json = string.Empty;
      if (shipmentId < 0)
      {
        response.StatusCode = HttpStatusCode.BadRequest;
        json = new JavaScriptSerializer().Serialize(new DTSShipment());
        response.Content = new StringContent(json);
      }
      else
      {
        dtsShipment = s_DTSShipmentList.FirstOrDefault(p => p.ID == shipmentId);
        if (dtsShipment != null)
        {
          response.StatusCode = HttpStatusCode.OK;
          json = new JavaScriptSerializer().Serialize(dtsShipment);
        }
        else
        {
          response.StatusCode = HttpStatusCode.BadRequest;
          json = new JavaScriptSerializer().Serialize(new DTSShipment());
        }
        response.Content = new StringContent(json);
      }
      return Task.FromResult(response);
    }

    /// <summary>
    /// Method is used to create Distribution
    /// </summary>
    /// <param name="dtsDistributionObj">dtsDistributionObj</param>
    /// <returns>Returns Task<HttpResponseMessage> object</returns>
    private Task<HttpResponseMessage> CreateDistribution(DTSDistribution dtsDistributionObj)
    {
      HttpResponseMessage response = new HttpResponseMessage();
      if (dtsDistributionObj == null)
      {
        response.StatusCode = HttpStatusCode.BadRequest;
        response.Content = new StringContent("DTSDistribution object cannot be null.");
      }
      else
      {
        if (dtsDistributionObj.Barcode == string.Empty)
        {
          response.StatusCode = HttpStatusCode.BadRequest;
          response.Content = new StringContent("Barcode value cannot be empty within DTSDistribution object.");
        }
        else if (dtsDistributionObj.ID < 0)
        {
          response.StatusCode = HttpStatusCode.BadRequest;
          response.Content = new StringContent("ID cannot be invalid value within DTSDistribution object.");
        }
        else
        {
          response.StatusCode = HttpStatusCode.OK;
          response.Content = new StringContent(AddDistributionIntoList(dtsDistributionObj).ToString());
        }
      }
      return Task.FromResult(response);
    }

    /// <summary>
    /// Method is used to create Distribution Group
    /// </summary>
    /// <param name="dtsDistributionGroupObj">dtsDistributionGroupObj</param>
    /// <returns>Returns Task<HttpResponseMessage> object</returns>
    private Task<HttpResponseMessage> CreateDistributionGroup(DTSDistributionGroup dtsDistributionGroupObj)
    {
      HttpResponseMessage response = new HttpResponseMessage();
      if (dtsDistributionGroupObj == null)
      {
        response.StatusCode = HttpStatusCode.BadRequest;
        response.Content = new StringContent("DTSDistributionGroup object cannot be null.");
      }
      else
      {
        if (dtsDistributionGroupObj.ID < 0)
        {
          response.StatusCode = HttpStatusCode.BadRequest;
          response.Content = new StringContent("ID cannot be invalid value within DTSDistributionGroup object.");
        }
        else
        {
          response.StatusCode = HttpStatusCode.OK;
          response.Content = new StringContent(AddDistributionGroupIntoList(dtsDistributionGroupObj).ToString());
        }
      }
      return Task.FromResult(response);
    }

    /// <summary>
    /// Method is used to get DTSShipment object with empty source
    /// </summary>
    /// <returns>Return DTSShipment object</returns>
    private DTSShipment GetDTSShipmentWithEmptySource()
    {
      DTSShipment dtsShipment = new DTSShipment();
      dtsShipment.SourceApplication = string.Empty;
      return dtsShipment;
    }

    /// <summary>
    /// Method is used to get DTSShipment object with invalid shipment id
    /// </summary>
    /// <returns>Return DTSShipment object</returns>
    private DTSShipment GetDTSShipmentWithInvalidShipmentId()
    {
      DTSShipment dtsShipment = new DTSShipment();
      dtsShipment.ID = -1;
      return dtsShipment;
    }

    /// <summary>
    /// Method is used to get DTSShipment object with empty title
    /// </summary>
    /// <returns>Return DTSShipment object</returns>
    private DTSShipment GetDTSShipmentWithEmptyTitle()
    {
      DTSShipment dtsShipment = new DTSShipment();
      dtsShipment.Title = string.Empty;
      return dtsShipment;
    }

    /// <summary>
    /// Method is used to get DTSShipment object
    /// <param name="id">id</param>
    /// </summary>
    /// <returns>Return DTSShipment object</returns>
    private static DTSShipment GetDTSShipmentObject(int id)
    {
      DTSShipment dtsShipment = new DTSShipment();
      dtsShipment.BMSTrackingNumber = id.ToString();
      dtsShipment.CarrierTrackingNumber = "Carrier Tracking Number " + id;
      dtsShipment.CreatedBy = "Rahul Goel";
      dtsShipment.CreatedDate = DateTime.Now;
      dtsShipment.DestinationCategory = "Destination Category " + id;
      dtsShipment.DestinationName = "Destination Name" + id;
      dtsShipment.DestinationSite = "Destination Site " + id;
      dtsShipment.DistributionGroups = s_DTSDistributionGroupList;
      dtsShipment.ID = id;
      dtsShipment.Recipient = "Recipient " + id;
      dtsShipment.RecipientAddress = GetDTSAddressObject(id);
      dtsShipment.RecipientAddressId = id;
      dtsShipment.Sender = "Sender " + id;
      dtsShipment.SenderAddress = GetDTSAddressObject(id);
      dtsShipment.SenderAddressId = id;
      dtsShipment.ShipDate = DateTime.Now;
      dtsShipment.ShipmentType = "Shipment Type " + id;
      dtsShipment.SourceApplication = "Source Application " + id;
      dtsShipment.SourceSite = "Source Site " + id;
      dtsShipment.Status = "Status " + id;
      dtsShipment.Statuses = new List<object>();
      dtsShipment.Title = "Title " + id;
      dtsShipment.UpdatedBy = string.Empty;
      dtsShipment.UpdatedDate = DateTime.Now;
      return dtsShipment;
    }

    /// <summary>
    /// Method is used to get DTSDistribution object with invalid id
    /// <param name="id">id</param>
    /// </summary>
    /// <returns>Return DTSDistribution object</returns>
    private DTSDistribution GetDTSDistributionWithInvalidId(int id)
    {
      DTSDistribution dtsDistribution = GetDTSDistributionObject(id);
      dtsDistribution.ID = -1;
      return dtsDistribution;
    }

    /// <summary>
    /// Method is used to get DTSDistribution object with empty barcode
    /// <param name="id">id</param>
    /// </summary>
    /// <returns>Return DTSDistribution object</returns>
    private DTSDistribution GetDTSDistributionWithEmptyBarcode(int id)
    {
      DTSDistribution dtsDistribution = GetDTSDistributionObject(id);
      dtsDistribution.Barcode = string.Empty;
      return dtsDistribution;
    }

    /// <summary>
    /// Method is used to get DTSDistribution object
    /// <param name="id">id</param>
    /// </summary>
    /// <returns>Return DTSDistribution object</returns>
    private static DTSDistribution GetDTSDistributionObject(int id)
    {
      DTSDistribution dtsDistribution = new DTSDistribution();
      dtsDistribution.Barcode = "Barcode " + id;
      dtsDistribution.Chemical = GetDTSChemical(id);
      dtsDistribution.ChemicalId = id;
      dtsDistribution.Comments = "Comments " + id;
      dtsDistribution.CreatedBy = "Rahul Goel";
      dtsDistribution.CreatedDate = DateTime.Now;
      dtsDistribution.DistributionGroupId = id;
      dtsDistribution.EntityType = "Entity Type " + id;
      dtsDistribution.HandlingCategory = "Handling Category " + id;
      dtsDistribution.ID = id;
      dtsDistribution.PreparedBy = "Rahul Goel";
      dtsDistribution.Recipient = "Recipient " + id;
      dtsDistribution.RecipientAddress = GetDTSAddressObject(id);
      dtsDistribution.RecipientAddressId = id;
      dtsDistribution.ShippingLevel = "Shipping Level " + id;
      dtsDistribution.Status = "Status " + id;
      dtsDistribution.Statuses = new List<object>();
      dtsDistribution.UpdatedBy = string.Empty;
      dtsDistribution.UpdatedDate = DateTime.Now;
      return dtsDistribution;
    }

    /// <summary>
    /// Method is used to get DTSDistributionGroup object with invalid id
    /// <param name="id">id</param>
    /// </summary>
    /// <returns>Return DTSDistributionGroup object</returns>
    private DTSDistributionGroup GetDTSDistributionGroupWithInvalidId(int id)
    {
      DTSDistributionGroup dtsDistributionGroup = GetDTSDistributionGroupObject(id);
      dtsDistributionGroup.ID = -1;
      return dtsDistributionGroup;
    }

    /// <summary>
    /// Method is used to get DTSDistributionGroup object
    /// <param name="id">id</param>
    /// </summary>
    /// <returns>Return DTSDistributionGroup object</returns>
    private static DTSDistributionGroup GetDTSDistributionGroupObject(int id)
    {
      DTSDistributionGroup dtsDistributionGroup = new DTSDistributionGroup();
      dtsDistributionGroup.Barcode = "Barcode " + id;
      dtsDistributionGroup.ContainerType = "Container Type " + id;
      dtsDistributionGroup.CreatedBy = "Rahul Goel";
      dtsDistributionGroup.CreatedDate = DateTime.Now;
      dtsDistributionGroup.DestinationCategory = "Destination Category " + id;
      dtsDistributionGroup.DestinationName = "Destination Name " + id;
      dtsDistributionGroup.DestinationSite = "Destination Site " + id;
      dtsDistributionGroup.DistributionGroups = new List<DTSDistributionGroup>();
      dtsDistributionGroup.Distributions = s_DTSDistributionList;
      dtsDistributionGroup.GroupLevel = id;
      dtsDistributionGroup.HandlingCategory = "Handling Category " + id;
      dtsDistributionGroup.ID = id;
      dtsDistributionGroup.ParentGroupId = id;
      dtsDistributionGroup.Recipient = "Recipient " + id;
      dtsDistributionGroup.RecipientAddress = GetDTSAddressObject(id);
      dtsDistributionGroup.RecipientAddressId = id;
      dtsDistributionGroup.ShipmentId = id;
      dtsDistributionGroup.SourceApplication = "Source Application " + id;
      dtsDistributionGroup.SourceSite = "Source Site " + id;
      dtsDistributionGroup.Status = "Status " + id;
      dtsDistributionGroup.Statuses = new List<object>();
      dtsDistributionGroup.TotalChildren = s_DTSDistributionList.Count;
      dtsDistributionGroup.TotalDistributions = s_DTSDistributionList.Count;
      dtsDistributionGroup.UpdatedBy = string.Empty;
      dtsDistributionGroup.UpdatedDate = DateTime.Now;
      return dtsDistributionGroup;
    }

    /// <summary>
    /// Method is used to get DTSAddress object
    /// <param name="id">id</param>
    /// </summary>
    /// <returns>Return DTSAddress object</returns>
    private static DTSAddress GetDTSAddressObject(int id)
    {
      DTSAddress dtsAddress = new DTSAddress();
      dtsAddress.City = "City " + id;
      dtsAddress.Country = "Country " + id;
      dtsAddress.CreatedBy = "Rahul Goel";
      dtsAddress.CreatedDate = DateTime.Now;
      dtsAddress.EMail = "rahul.goel@titian.co.uk";
      dtsAddress.ID = id;
      dtsAddress.MailStop = "Mail stop " + id;
      dtsAddress.Name = "Name " + id;
      dtsAddress.Phone = "Phone " + id;
      dtsAddress.SiteCode = "Site Code " + id;
      dtsAddress.State = "State " + id;
      dtsAddress.Street1 = "Street " + id;
      dtsAddress.Street2 = "Street " + id;
      dtsAddress.UpdatedBy = string.Empty;
      dtsAddress.UpdatedDate = DateTime.Now;
      dtsAddress.Zip = "Zip " + id;
      return dtsAddress;
    }

    /// <summary>
    /// Method is used to get DTSAddress object from despatch labware data row
    /// </summary>
    /// <param name="despatchLabwareDataRow">despatchLabwareDataRow</param>
    /// <returns>Return DTSAddress object</returns>
    private static DTSAddress GetDTSAddressObjectFromDataRow(DataRow despatchLabwareDataRow)
    {
      DTSAddress dtsAddress = new DTSAddress();
      dtsAddress.City = despatchLabwareDataRow["City"].ToString();
      dtsAddress.Country = despatchLabwareDataRow["Country"].ToString();
      dtsAddress.CreatedBy = "Rahul Goel";
      dtsAddress.CreatedDate = DateTime.Now;
      dtsAddress.EMail = despatchLabwareDataRow["Email"].ToString();
      dtsAddress.ID = 1;
      dtsAddress.MailStop = string.Empty;
      dtsAddress.Name = string.Empty;
      dtsAddress.Phone = despatchLabwareDataRow["Phone"].ToString();
      dtsAddress.SiteCode = string.Empty;
      dtsAddress.State = despatchLabwareDataRow["State"].ToString();
      dtsAddress.Street1 = despatchLabwareDataRow["AddressLine1"].ToString();
      dtsAddress.Street2 = despatchLabwareDataRow["AddressLine2"].ToString();
      dtsAddress.UpdatedBy = string.Empty;
      dtsAddress.UpdatedDate = DateTime.Now;
      dtsAddress.Zip = string.Empty;
      return dtsAddress;
    }

    /// <summary>
    /// Method is used to get DTSChemical object
    /// </summary>
    /// <param name="id">id</param>
    /// <returns>Return DTSChemical object</returns>
    private static DTSChemical GetDTSChemical(int id)
    {
      DTSChemical dtsChemical = new DTSChemical();
      dtsChemical.Amount = id;
      dtsChemical.AmountUnit = "Amount Unit " + id;
      dtsChemical.Concentration = id;
      dtsChemical.ConcentrationUnit = "Concentration Unit " + id;
      dtsChemical.ContainerColumn = id;
      dtsChemical.ContainerRow = id;
      dtsChemical.ContainerType = "Container Type " + id;
      dtsChemical.CreatedBy = "Rahul Goel";
      dtsChemical.CreatedDate = DateTime.Now;
      dtsChemical.ID = id;
      dtsChemical.IsVST = (id % 2 == 0);
      dtsChemical.IsWeighable = (id % 2 == 0);
      dtsChemical.RegistrationId = id;
      dtsChemical.SampleState = "Sample State" + id;
      dtsChemical.Solvent = "Solvent " + id;
      dtsChemical.UpdatedBy = string.Empty;
      dtsChemical.UpdatedDate = DateTime.Now;
      return dtsChemical;
    }

    /// <summary>
    /// Method is used to add distribution group object into distribution group list
    /// </summary>
    /// <param name="dtsDistributionGroupObj">dtsDistributionGroupObj</param>
    /// <returns>Return recently added distribution group id</returns>
    private int AddDistributionGroupIntoList(DTSDistributionGroup dtsDistributionGroupObj)
    {
      dtsDistributionGroupObj.ID = s_DTSDistributionGroupList.Count + 1;
      s_DTSDistributionGroupList.Add(dtsDistributionGroupObj);
      return s_DTSDistributionGroupList.Count;
    }

    /// <summary>
    /// Method is used to add distribution object into distribution list
    /// </summary>
    /// <param name="dtsDistributionObj">dtsDistributionObj</param>
    /// <returns>Return recently added distribution id</returns>
    private int AddDistributionIntoList(DTSDistribution dtsDistributionObj)
    {
      dtsDistributionObj.ID = s_DTSDistributionList.Count + 1;
      s_DTSDistributionList.Add(dtsDistributionObj);
      return s_DTSDistributionList.Count;
    }

    /// <summary>
    /// Method is used to add shipment object into shipment list
    /// </summary>
    /// <param name="dtsShipmentObj">dtsShipmentObj</param>
    /// <returns>Return recently added shipment id</returns>
    private int AddShipmentIntoList(DTSShipment dtsShipmentObj)
    {
      s_DTSShipmentList.Add(dtsShipmentObj);
      return s_DTSShipmentList.Count;
    }

    /// <summary>
    /// Method is used to get shipment list
    /// </summary>
    /// <returns>Return shipment list</returns>
    private static List<DTSShipment> GetDTSShipmentList()
    {
      List<DTSShipment> dtsShipmentList = new List<DTSShipment>();
      for (int i = 1; i <= 5; i++)
      {
        dtsShipmentList.Add(GetDTSShipmentObject(i));
      }
      return dtsShipmentList;
    }

    /// <summary>
    /// Method is used to get distribution list
    /// </summary>
    /// <returns>Return distribution list</returns>
    private static List<DTSDistribution> GetDTSDistributionList()
    {
      List<DTSDistribution> dtsDistributionList = new List<DTSDistribution>();
      for (int i = 1; i <= 5; i++)
      {
        dtsDistributionList.Add(GetDTSDistributionObject(i));
      }
      return dtsDistributionList;
    }

    /// <summary>
    /// Method is used to get distribution group list
    /// </summary>
    /// <returns>Return distribution group list</returns>
    private static List<DTSDistributionGroup> GetDTSDistributionGroupList()
    {
      List<DTSDistributionGroup> dtsDistributionGroupList = new List<DTSDistributionGroup>();
      for (int i = 1; i <= 5; i++)
      {
        dtsDistributionGroupList.Add(GetDTSDistributionGroupObject(i));
      }
      return dtsDistributionGroupList;
    }

    /// <summary>
    /// Method is used to get despatch labware data by shipment id
    /// </summary>
    /// <param name="shipmentId">shipmentId</param>
    /// <returns>Return List of <see cref="DespatchLabwareDto"/></returns>
    private List<DespatchLabwareDto> GetDespatchLabwareByShipmentId(int shipmentId)
    {
      List<DespatchLabwareDto> despatchLabwares = null;

      if (shipmentId >= 0)
      {
        var selecteditem = s_DespatchLabwareData.Where(f=>f.ShipmentId== shipmentId);
        if (selecteditem.Any())
        {
          despatchLabwares =new List<DespatchLabwareDto>();
          despatchLabwares.AddRange(selecteditem);
        }
      }
      return despatchLabwares;
    }

    

    
    

    /// <summary>
    /// Method is used to get DTSStatus object
    /// <param name="id">id</param>
    /// </summary>
    /// <returns>Return DTSStatus object</returns>
    private static DTSStatus GetDTSStatusObject(int id)
    {
      DTSStatus dtsStatus = new DTSStatus();
      dtsStatus.ID = id;
      dtsStatus.ShipmentId = id;
      dtsStatus.DistributionGroupId = id;
      dtsStatus.DistributionId = id;
      dtsStatus.StatusDate = DateTime.Now;
      dtsStatus.Status = "Status" + id;
      dtsStatus.CreatedBy = "Rahul Goel";
      dtsStatus.CreatedDate = DateTime.Now;
      return dtsStatus;
    }

    /// <summary>
    /// Method is used to update DTS Status
    /// </summary>
    /// <param name="dtsStatusObj">dtsStatusObj</param>
    /// <returns>Returns HttpResponseMessage object</returns>
    private Task<HttpResponseMessage> UpdateDistributionGroupStatus(DTSStatus dtsStatusObj)
    {
      HttpResponseMessage response = new HttpResponseMessage();
      DTSDistributionGroup dtsDistributionGroup = null;
      if (dtsStatusObj == null)
      {
        response.StatusCode = HttpStatusCode.BadRequest;
        response.Content = new StringContent("DTSStatus object cannot be null.");
      }
      else
      {
        dtsDistributionGroup = s_DTSDistributionGroupList.FirstOrDefault(p => p.ID == dtsStatusObj.DistributionGroupId);
        if (dtsDistributionGroup != null)
        {
          response.StatusCode = HttpStatusCode.OK;
          dtsDistributionGroup.Status = dtsStatusObj.Status;
          response.Content = new StringContent("Distribution Group exists and updated for the given DTSStatus object.");
        }
        else
        {
          response.StatusCode = HttpStatusCode.BadRequest;
          response.Content = new StringContent("Distribution Group does not exists for the given DTSStatus object.");
        }
      }
      return Task.FromResult(response);
    }

    /// <summary>
    /// Method is used to check whether Actual shipment are valid or not
    /// </summary>
    /// <param name="expectedShipments">expectedShipments</param>
    /// <param name="actualShipments">actualShipments</param>
    /// <returns>Returned true if Actual shipment are valid otherwise false</returns>
    private bool IsActualShipmentValid(List<ShipmentDto> expectedShipments, Dictionary<int, DTSShipment> actualShipments)
    {
      bool isValid = true;
      foreach (int shipmentId in actualShipments.Keys)
      {
        if (expectedShipments.FirstOrDefault(p => p.ShipmentId== shipmentId) == null)
        {
          isValid = false;
          break;
        }
      }
      return isValid;
    }

  }
}
