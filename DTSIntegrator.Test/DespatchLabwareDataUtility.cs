//  Copyright © Titian Software Ltd
//
using System.Collections.Generic;
using Titian.BMS.Shipping.DTSIntegration.TasProd.DataTransferObjects;

namespace Titian.BMS.Shipping.DTSIntegration.TasProd.Test
{
  /// <summary>
  /// class DespatchLabwareDataUtility used to get Despatch Labware data
  /// </summary>
  public static class DespatchLabwareDataUtility
  {
    /// <summary>
    /// Method is used to get despatch labware items 
    /// </summary>
    /// <returns>Return List of <see cref="DespatchLabwareDto"/></returns>
    public static List<DespatchLabwareDto> GetDespatchLabwareItems()
    {
      List<DespatchLabwareDto> despatchLabwareDtos = new List<DespatchLabwareDto>();
      DespatchLabwareDto labwareDto = null;

      for (int i = 1; i <= 5; i++)
      {
        labwareDto = new DespatchLabwareDto();
        labwareDto.ShipmentId = i;
        labwareDto.LabwareItemId = i;
        labwareDto.LabwareClassId = (short)(i==3?2:i==5?4:i);
        labwareDto.LabwareBarcode = "Labware Barcode " + i;
        labwareDto.DespatchId = i;
        labwareDto.Recipient = "Recipient " + i;
        labwareDto.Organisation = "Organisation " + i;
        labwareDto.AddressLine1 = "Address Line " + i;
        labwareDto.AddressLine2 = "Address Line " + i;
        labwareDto.City = "City " + i;
        labwareDto.State = "State " + i;
        labwareDto.Country = "Country " + i;
        labwareDto.PostalCode = "Postal Code " + i;
        labwareDto.Phone = "Phone " + i;
        labwareDto.Email = "Email " + i;
        labwareDto.ContainerLabwareClassId = (short)(i == 3 ? 2 : i == 5 ? 1 : i);
        labwareDto.ContainerBarcode = "Labware Barcode " + i;
        despatchLabwareDtos.Add(labwareDto);
      }

      return despatchLabwareDtos;
    }
    /// <summary>
    /// Method is used to get Sample for labware items 
    /// </summary>
    /// <returns>Return Dictionary of <see cref="LabwareSampleDto"/></returns>
    public static Dictionary<int,LabwareSampleDto> GetSampleForLabwareItem()
    {
      Dictionary<int, LabwareSampleDto> sampleDtos = new Dictionary<int, LabwareSampleDto>();
      LabwareSampleDto sampleDto = null;
      for (int i = 1; i <= 5; i++)
      {
        sampleDto = new LabwareSampleDto();
        sampleDto.XPosition = i;
        sampleDto.YPosition = i;
        sampleDto.Amount = i;
        sampleDto.AmountUnit = "Amount Unit " + i;
        sampleDto.Concentration = i;
        sampleDto.ConcentrationUnit = "Concentration Unit " + i;
        sampleDto.IsDry = (i % 2 == 0);
        sampleDto.IsWeighable = (i % 2 == 0);
        sampleDto.Solvent = "Solvent " + i;
        sampleDto.SubstanceId = i;
        sampleDtos.Add(i,sampleDto);  
      }
      return sampleDtos;
    }
  }
}
