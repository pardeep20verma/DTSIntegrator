//  Copyright © Titian Software Ltd
//
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Titian.BMS.Shipping.DTSIntegration.TasProd.DataTransferObjects;
using Titian.Data;
using Titian.Data.Dapper;

namespace Titian.BMS.Shipping.DTSIntegration.TasProd.DataAccess
{
  /// <summary>
  /// class DADTSInegrator contains data access logic
  /// </summary>
  public class DADTSIntegrator: IDADTSIntegrator
  {
    /// <summary>
    /// Method Get Despatch Labware Items details for shipment
    /// </summary>
    /// <param name="procInstanceId">proc Instance Id</param>
    /// <param name="shipmentId">shipment Id</param>
    /// <returns>List of <see cref="DespatchLabwareDto"/></returns>
    public List<DespatchLabwareDto> GetDespatchLabwareItemsForShipment(
     int procInstanceId,
     int shipmentId)
    {
      List<DespatchLabwareDto> despatchLabwareDtos = new List<DespatchLabwareDto>();

      using (Connection conn = new Connection(procInstanceId))
      {
        var sql = String.Format(
          CultureInfo.InvariantCulture,
          @"select
              ssb.ShipmentId,
              ili.LabwareItemId,
              ili.LabwareClassId,
              ili.LabwareBarcode,             
              ssbi.DespatchId,
              ss.Recipient,
              ss.Organisation,
              ss.AddressLine1,
              ss.AddressLine2,
              ss.City,
              ss.StateCode State,
              ss.CountryCode Country,
              ss.PostalCode,
              ss.Phone,
              ss.EmailAddress Email,
              ss.CreateUserName as CreatedBy,
              case ili.LabwareClassId when 2 then ili.LabwareBarcode else Nvl((select LabwareBarcode from {0}.inv_labware_item where labwareItemId=sdli.CONTAININGLABWAREITEMID and LabwareClassId!=3 ),ili.LabwareBarcode) end ContainerBarcode,
              case ili.LabwareClassId when 2 then ili.LabwareClassId else Nvl((select LabwareClassId from {0}.inv_labware_item where labwareItemId=sdli.CONTAININGLABWAREITEMID and LabwareClassId!=3),ili.LabwareClassId) end ContainerLabwareClassId
            from {0}.shp_shipment_box ssb
            inner join {0}.shp_shipment ss on ss.shipmentId=ssb.shipmentId
            inner join {0}.shp_shipment_box_item ssbi on ssbi.ShipmentBoxId = ssb.ShipmentBoxId
            inner join {0}.shp_despatch_labware_item sdli on sdli.DespatchId = ssbi.DespatchId
            inner join {0}.inv_labware_item ili on ili.LabwareItemId = sdli.LabwareItemId
            where (ili.LabwareClassId=4 or ili.LabwareClassId=2) and ssb.ShipmentId = :shipmentId
            order by ili.LabwareClassId",
          conn.Schema);

        despatchLabwareDtos= conn.Query<DespatchLabwareDto>(sql,new { shipmentId }).ToList();
      }

      return despatchLabwareDtos;
    }

    /// <summary>
    /// Method to Get Sample for Labware Item
    /// </summary>
    /// <param name="procInstanceId">proc Instance Id</param>
    /// <param name="labwareItemId">labware Item Id</param>
    /// <returns>List of <see cref="LabwareSampleDto"/></returns>
    public List<LabwareSampleDto> GetSampleForLabwareItem(
     int procInstanceId,
     int labwareItemId)
    {
      List<LabwareSampleDto> sampleLabwareDtos = new List<LabwareSampleDto>();

      using (Connection conn = new Connection(procInstanceId))
      {
        var sql = String.Format(
          CultureInfo.InvariantCulture,
          @"select
              ich.XPosition,
              ich.YPosition,
              ich.Amount,
              case ich.IsNeat when 1 then 'mg' else 'uL' end AmountUnit,
              ich.Concentration,
              decode (ich.ConcentrationUnitType, 1, 'mM', 4, 'mg/mL', iut.UnitTypeName) ConcentrationUnit,
              ich.IsNeat IsDry,
              iis.SolventName Solvent,
              ich.SubstanceId,
              case ich.AccessibilityId when 0 then 1 else 0 end IsWeighable
            from {0}.inv_sample_holder ich
            left outer join {0}.inv_solvent iis on iis.SolventId=ich.SolventId
            left outer join {0}.inv_unit_type iut on iut.UnitTypeId=ich.ConcentrationUnitType
            where ich.LABWAREITEMID = :labwareItemId and ich.AMOUNT > 0",
          conn.Schema);

        sampleLabwareDtos = conn.Query<LabwareSampleDto>(sql, new { labwareItemId }).ToList();
      }

      return sampleLabwareDtos;
    }

  }
}
