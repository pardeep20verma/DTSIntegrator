//  Copyright © Titian Software Ltd
using System.Collections.Generic;
using Titian.BMS.Shipping.DTSIntegration.TasProd.DataTransferObjects;

namespace Titian.BMS.Shipping.DTSIntegration.TasProd.DataAccess
{
  /// <summary>
  /// This is the interface for DADTSIntegrator class
  /// It is used to implement test case of DADTSIntegrator class
  /// </summary>
  public interface IDADTSIntegrator
  {
    /// <summary>
    /// Method to get Despatch Labware Items based on Shipment Id
    /// </summary>
    /// <param name="procInstanceId">procInstanceId</param>
    /// <param name="shipmentId">shipmentId</param>
    /// <returns>List of <see cref="DespatchLabwareDto"/></returns>
    List<DespatchLabwareDto> GetDespatchLabwareItemsForShipment(int procInstanceId, int shipmentId);

    /// <summary>
    /// Method to Get Sample for Labware Item
    /// </summary>
    /// <param name="procInstanceId">proc Instance Id</param>
    /// <param name="labwareItemId">labware Item Id</param>
    /// <returns>List of <see cref="LabwareSampleDto"/></returns>
    List<LabwareSampleDto> GetSampleForLabwareItem(int procInstanceId,int labwareItemId);
  }
}
