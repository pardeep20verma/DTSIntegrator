//  Copyright © Titian Software Ltd
namespace Titian.BMS.Shipping.DTSIntegration.TasProd.DataTransferObjects
{
  /// <summary>
  /// Class for Despatch labware Data Object
  /// </summary>
  public class DespatchLabwareDto
  {
    /// <summary>
    /// The Despatch id
    /// </summary>
    public int DespatchId { get; set; }
    /// <summary>
    /// The Barcode for labware item
    /// </summary>
    public string LabwareBarcode { get; set; }
    /// <summary>
    /// The Labware class id
    /// </summary>
    public short LabwareClassId { get; set; }
    /// <summary>
    /// The labware Item id
    /// </summary>
    public int LabwareItemId { get; set; }
    /// <summary>
    /// The shipment Id 
    /// </summary>
    public int ShipmentId { get; set; }
    /// <summary>
    /// The Postal code
    /// </summary>
    public string PostalCode { get; set; }
    /// <summary>
    /// The Email Id
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// The Phone Number 
    /// </summary>
    public string Phone { get; set; }
    /// <summary>
    /// The country Name
    /// </summary>
    public string Country { get; set; }
    /// <summary>
    /// The State Name
    /// </summary>
    public string State { get; set; }
    /// <summary>
    /// The City Name
    /// </summary>
    public string City { get; set; }
    /// <summary>
    /// The Address Line 1 for shipment
    /// </summary>
    public string AddressLine1 {get; set;}
    /// <summary>
    /// The Address Line 2 for shipment
    /// </summary>
    public string AddressLine2 { get; set; }
    /// <summary>
    /// The organisation for shipment 
    /// </summary>
    public string Organisation { get; set; }
    /// <summary>
    /// The Recipient for shipment
    /// </summary>
    public string Recipient { get; set; }
    
    /// <summary>
    /// The Name of user who created the shipment
    /// </summary>
    public string CreatedBy { get; set; }
    /// <summary>
    /// The Barcode for Container
    /// </summary>
    public string ContainerBarcode { get; set; }

    /// <summary>
    /// The Container Labware class id
    /// </summary>
    public short ContainerLabwareClassId { get; set; }
  }
}
