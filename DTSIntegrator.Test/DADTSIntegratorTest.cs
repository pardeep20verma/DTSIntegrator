//  Copyright © Titian Software Ltd
//
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Titian.BMS.Shipping.DTSIntegration.TasProd.DataAccess;
using Titian.BMS.Shipping.DTSIntegration.TasProd.DataTransferObjects;

namespace Titian.BMS.Shipping.DTSIntegration.TasProd.Test
{
  /// <summary>
  /// This is the test class for DADTSIntegrator
  /// </summary>
  [TestClass]
  public class DADTSIntegratorTest
  {
    private static Mock<IDADTSIntegrator> mockDADTSIntegrator = null;
    private static List<DespatchLabwareDto> s_DespatchLabwareItems = null;
    private static Dictionary<int,LabwareSampleDto> s_LabwareSampleItems = null;

    /// <summary>
    /// Use MyClassInitialize to run code before all tests in a class have run
    /// </summary>
    /// <param name="testContext">testContext</param>
    [ClassInitialize]
    public static void MyClassInitialize(TestContext testContext)
    {
      mockDADTSIntegrator = new Mock<IDADTSIntegrator>();
      s_DespatchLabwareItems = DespatchLabwareDataUtility.GetDespatchLabwareItems();
      s_LabwareSampleItems = DespatchLabwareDataUtility.GetSampleForLabwareItem();
    }

    /// <summary>
    /// Test Method for GetDespatchLabwareItemsForShipment with invalid ProcInstance Id and invalid Shipment Id
    /// </summary>
    [TestMethod]
    public void TestGetDespatchLabwareItemsForShipment_InvalidProcInstanceId_InvalidShipmentId()
    {
      // Arrange
      int testProcInstanceId = -1;
      int testShipmentId = -1;

      mockDADTSIntegrator.Setup(p => p.GetDespatchLabwareItemsForShipment(It.IsAny<int>(), It.IsAny<int>())).Returns((int procInstanceId, int shipmentId) =>
      {
        return null;
      });
      IDADTSIntegrator daDTSIntegrator = mockDADTSIntegrator.Object;

      // Act
      var result = daDTSIntegrator.GetDespatchLabwareItemsForShipment(testProcInstanceId, testShipmentId);

      // Assert
      Assert.IsNull(result);
    }

    /// <summary>
    /// Test Method for GetDespatchLabwareItemsForShipment with valid ProcInstance Id and invalid Shipment Id
    /// </summary>
    [TestMethod]
    public void TestGetDespatchLabwareItemsForShipment_ValidProcInstanceId_InvalidShipmentId()
    {
      // Arrange
      int testProcInstanceId = 25000;
      int testShipmentId = -1;

      mockDADTSIntegrator.Setup(p => p.GetDespatchLabwareItemsForShipment(It.IsAny<int>(), It.IsAny<int>())).Returns((int procInstanceId, int shipmentId) =>
      {
        return GetDespatchLabwareItemsForShipment(procInstanceId, shipmentId);
      });
      IDADTSIntegrator daDTSIntegrator = mockDADTSIntegrator.Object;

      // Act
      var result = daDTSIntegrator.GetDespatchLabwareItemsForShipment(testProcInstanceId, testShipmentId);

      // Assert
      Assert.IsNull(result);
    }

    /// <summary>
    /// Test Method for GetDespatchLabwareItemsForShipment with invalid ProcInstance Id and valid Shipment Id
    /// </summary>
    [TestMethod]
    public void TestGetDespatchLabwareItemsForShipment_InvalidProcInstanceId_ValidShipmentId()
    {
      // Arrange
      int testProcInstanceId = -1;
      int testShipmentId = 1;

      mockDADTSIntegrator.Setup(p => p.GetDespatchLabwareItemsForShipment(It.IsAny<int>(), It.IsAny<int>())).Returns((int procInstanceId, int shipmentId) =>
      {
        return GetDespatchLabwareItemsForShipment(procInstanceId, shipmentId);
      });
      IDADTSIntegrator daDTSIntegrator = mockDADTSIntegrator.Object;

      // Act
      var result = daDTSIntegrator.GetDespatchLabwareItemsForShipment(testProcInstanceId, testShipmentId);

      // Assert
      Assert.IsNull(result);
    }

    /// <summary>
    /// Test Method for GetDespatchLabwareItemsForShipment with valid ProcInstance Id and valid Shipment Id
    /// </summary>
    [TestMethod]
    public void TestGetDespatchLabwareItemsForShipment_ValidProcInstanceId_ValidShipmentId()
    {
      // Arrange
      int testProcInstanceId = 25000;
      int testShipmentId = 1;

      mockDADTSIntegrator.Setup(p => p.GetDespatchLabwareItemsForShipment(It.IsAny<int>(), It.IsAny<int>())).Returns((int procInstanceId, int shipmentId) =>
      {
        return GetDespatchLabwareItemsForShipment(procInstanceId, shipmentId);
      });
      IDADTSIntegrator daDTSIntegrator = mockDADTSIntegrator.Object;

      // Act
      var result = daDTSIntegrator.GetDespatchLabwareItemsForShipment(testProcInstanceId, testShipmentId);

      // Assert
      Assert.IsNotNull(result);
    }


    /// <summary>
    /// Test Method for GetSampleForLabwareItem with invalid ProcInstance Id and invalid Labware Item Id
    /// </summary>
    [TestMethod]
    public void TestGetSampleForLabwareItem_InvalidProcInstanceId_InvalidLabwareItemId()
    {
      // Arrange
      int testProcInstanceId = -1;
      int testLabwareItemId = -1;

      mockDADTSIntegrator.Setup(p => p.GetSampleForLabwareItem(It.IsAny<int>(), It.IsAny<int>())).Returns((int procInstanceId, int labwareItemId) =>
      {
        return null;
      });
      IDADTSIntegrator daDTSIntegrator = mockDADTSIntegrator.Object;

      // Act
      var result = daDTSIntegrator.GetSampleForLabwareItem(testProcInstanceId, testLabwareItemId);

      // Assert
      Assert.IsNull(result);
    }

    /// <summary>
    /// Test Method for GetSampleForLabwareItem with valid ProcInstance Id and invalid Labware Item Id
    /// </summary>
    [TestMethod]
    public void TestGetSampleForLabwareItem_ValidProcInstanceId_InvalidLabwareItemId()
    {
      // Arrange
      int testProcInstanceId = 25000;
      int testLabwareItemId = -1;

      mockDADTSIntegrator.Setup(p => p.GetSampleForLabwareItem(It.IsAny<int>(), It.IsAny<int>())).Returns((int procInstanceId, int labwareItemId) =>
      {
        return GetSampleForLabwareItem(procInstanceId, labwareItemId);
      });
      IDADTSIntegrator daDTSIntegrator = mockDADTSIntegrator.Object;

      // Act
      var result = daDTSIntegrator.GetSampleForLabwareItem(testProcInstanceId, testLabwareItemId);

      // Assert
      Assert.IsTrue(result.Count==0);
    }

   

    /// <summary>
    /// Test Method for GetSampleForLabwareItem with invalid ProcInstance Id and valid Labware Item Id
    /// </summary>
    [TestMethod]
    public void TestGetSampleForLabwareItem_InvalidProcInstanceId_ValidLabwareItemId()
    {
      // Arrange
      int testProcInstanceId = -1;
      int testLabwareItemId = 1;

      mockDADTSIntegrator.Setup(p => p.GetSampleForLabwareItem(It.IsAny<int>(), It.IsAny<int>())).Returns((int procInstanceId, int labwareItemId) =>
      {
        return GetSampleForLabwareItem(procInstanceId, labwareItemId);
      });
      IDADTSIntegrator daDTSIntegrator = mockDADTSIntegrator.Object;

      // Act
      var result = daDTSIntegrator.GetSampleForLabwareItem(testProcInstanceId, testLabwareItemId);

      // Assert
      Assert.IsNull(result);
    }

    /// <summary>
    /// Test Method for GetSampleForLabwareItem with valid ProcInstance Id and valid Labware Item Id
    /// </summary>
    [TestMethod]
    public void TestGetSampleForLabwareItem_ValidProcInstanceId_ValidLabwareItemId()
    {
      // Arrange
      int testProcInstanceId = 25000;
      int testLabwareItemId = 1;

      mockDADTSIntegrator.Setup(p => p.GetSampleForLabwareItem(It.IsAny<int>(), It.IsAny<int>())).Returns((int procInstanceId, int labwareItemId) =>
      {
        return GetSampleForLabwareItem(procInstanceId, labwareItemId);
      });
      IDADTSIntegrator daDTSIntegrator = mockDADTSIntegrator.Object;

      // Act
      var result = daDTSIntegrator.GetSampleForLabwareItem(testProcInstanceId, testLabwareItemId);

      // Assert
      Assert.IsNotNull(result);
    }

    //Use ClassCleanup to run code after all tests in a class have run
    [ClassCleanup]
    public static void MyClassCleanup()
    {
      mockDADTSIntegrator = null;
      s_DespatchLabwareItems = null;
      s_LabwareSampleItems = null;
    }

    /// <summary>
    /// Method is used to get despatch labware by shipment id
    /// </summary>
    /// <param name="procInstanceId">procInstanceId</param>
    /// <param name="shipmentId">shipmentId</param>
    /// <returns>Return List of  <see cref="DespatchLabwareDto"/></returns>
    public static List<DespatchLabwareDto> GetDespatchLabwareItemsForShipment(int procInstanceId, int shipmentId)
    {
      List<DespatchLabwareDto> despatchLabwareDto = null;
      if (procInstanceId == -1)
      {
        return null;
      }
      else
      {
        var selectedItems = s_DespatchLabwareItems.Where(f=>f.ShipmentId== shipmentId);

        if (selectedItems.Any())
        {
          despatchLabwareDto = new List<DespatchLabwareDto>();
          despatchLabwareDto.AddRange(selectedItems);
        }
        return despatchLabwareDto;
      }
    }

    /// <summary>
    /// Method is used to get labware sample by labware Id
    /// </summary>
    /// <param name="procInstanceId"></param>
    /// <param name="labwareItemId"></param>
    /// <returns>Return List of <see cref="LabwareSampleDto"/></returns>
    public static List<LabwareSampleDto> GetSampleForLabwareItem(int procInstanceId, int labwareItemId)
    {
      List<LabwareSampleDto> labwareSampleItems = null;
      if (procInstanceId == -1)
      {
        return null;
      }
      else
      {
        labwareSampleItems = s_LabwareSampleItems.Where(f => f.Key == labwareItemId).Select(s=>s.Value).ToList();
        
        return labwareSampleItems;
      }
    }

  }
}
