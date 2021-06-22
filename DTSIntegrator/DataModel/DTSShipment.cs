//  Copyright © Titian Software Ltd
using System;
using System.Collections.Generic;

namespace Titian.BMS.Shipping.DTSIntegration.TasProd.DataModel
{
  /// <summary>
  /// Class for DTS Shipment
  /// </summary>
  public class DTSShipment
  {
    /// <summary>
    /// Property will contains Status for shipment
    /// </summary>
    public string Status { get; set; }
    /// <summary>
    /// Property will contains the Source Application Name
    /// </summary>
    public string SourceApplication { get; set; }
    /// <summary>
    /// Property will contains the Source Site
    /// </summary>
    public string SourceSite { get; set; }
    /// <summary>
    /// Property will contains Destination Site 
    /// </summary>
    public string DestinationSite { get; set; }
    /// <summary>
    /// Property will contains the Destination Name
    /// </summary>
    public string DestinationName { get; set; }
    /// <summary>
    /// Property will contains Destination Category
    /// </summary>
    public string DestinationCategory { get; set; }
    /// <summary>
    /// Property will contains Ship Date at creation time
    /// </summary>
    public DateTime ShipDate { get; set; }
    /// <summary>
    /// Property will contains Sender
    /// </summary>
    public string Sender { get; set; }
    /// <summary>
    /// Property will contains the Sender Address id
    /// </summary>
    public int SenderAddressId { get; set; }
    /// <summary>
    /// Property will contains Address details of the Sender
    /// </summary>
    public DTSAddress SenderAddress { get; set; }
    /// <summary>
    /// Property will contains Recipient Name
    /// </summary>
    public string Recipient { get; set; }
    /// <summary>
    /// Property will contains Recipient Address Id
    /// </summary>
    public int RecipientAddressId { get; set; }
    /// <summary>
    /// Property will contains Recipient Address Details
    /// </summary>
    public DTSAddress RecipientAddress { get; set; }
    /// <summary>
    /// Property will contains DTS Shipment Id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// Property will contains Title
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// Property will contains Shipment Type
    /// </summary>
    public string ShipmentType { get; set; }
    /// <summary>
    /// Property will contains BMS Tracking Number
    /// </summary>
    public string BMSTrackingNumber { get; set; }
    /// <summary>
    /// Property will contains Carrier Tracking Number
    /// </summary>
    public string CarrierTrackingNumber { get; set; }
    /// <summary>
    /// Property will contains the list of parent Distribution Group
    /// </summary>
    public List<DTSDistributionGroup> DistributionGroups { get; set; }
    /// <summary>
    /// Property will contains List of Statuses
    /// </summary>
    public List<object> Statuses { get; set; }
    /// <summary>
    /// Property will contains Created By User Name
    /// </summary>
    public string CreatedBy { get; set; }
    /// <summary>
    /// Property will contains Created Date
    /// </summary>
    public DateTime CreatedDate { get; set; }
    /// <summary>
    /// Property will contains Updated By 
    /// </summary
    public string UpdatedBy { get; set; }
    /// <summary>
    /// Property will contains Updated Date
    /// </summary>
    public DateTime UpdatedDate { get; set; }
  }
}
