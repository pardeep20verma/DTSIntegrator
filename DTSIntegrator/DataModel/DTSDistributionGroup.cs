//  Copyright © Titian Software Ltd
using System;
using System.Collections.Generic;

namespace Titian.BMS.Shipping.DTSIntegration.TasProd.DataModel
{
  /// <summary>
  /// Class for DTS distribution group
  /// </summary>
 public class DTSDistributionGroup
  {
    /// <summary>
    /// Property will contains Handling Category
    /// </summary>
    public string HandlingCategory { get; set; }
    /// <summary>
    /// Property will contains Source Application
    /// </summary>
    public string SourceApplication { get; set; }
    /// <summary>
    /// Property will contains Source Site
    /// </summary>
    public string SourceSite { get; set; }
    /// <summary>
    /// Property will contains  Destination Zone Name
    /// </summary>
    public string DestinationSite { get; set; }
    /// <summary>
    /// Property will contains Recipient Name
    /// </summary>
    public string DestinationName { get; set; }
    /// <summary>
    /// Property will contains Destination Category
    /// </summary>
    public string DestinationCategory { get; set; }
    /// <summary>
    /// Property will contains Container Type
    /// </summary>
    public string ContainerType { get; set; }
    /// <summary>
    /// Property will contains Barcode of Labware item
    /// </summary>
    public string Barcode { get; set; }
    /// <summary>
    /// Property will contains Status 
    /// </summary>
    public string Status { get; set; }
    /// <summary>
    /// Property will contains Recipient
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
    /// Property will contains Distribution Group Id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// Property will contains parent Distribution Group Id
    /// </summary>
    public int ParentGroupId { get; set; }
    /// <summary>
    /// Property will contains Shipment Id
    /// </summary>
    public int ShipmentId { get; set; }
    /// <summary>
    /// Property will contains Group Level
    /// </summary>
    public int GroupLevel { get; set; }
    /// <summary>
    /// Property will contains list of Distributions
    /// </summary>
    public List<DTSDistribution> Distributions { get; set; }
    /// <summary>
    /// Property will contains List of child Distributions Groups
    /// </summary>
    public List<DTSDistributionGroup> DistributionGroups { get; set; }
    /// <summary>
    /// Property will contains List of DTS status object
    /// </summary>
    public List<object> Statuses { get; set; }
    /// <summary>
    /// Property will contains Total Number of Children
    /// </summary>
    public int TotalChildren { get; set; }
    /// <summary>
    /// Property will contains total number of distributions
    /// </summary>
    public int TotalDistributions { get; set; }
    /// <summary>
    /// Property will contains Created By User Name
    /// </summary>
    public string CreatedBy { get; set; }
    /// <summary>
    /// Property will contains Created Date
    /// </summary>
    public DateTime CreatedDate { get; set; }
    /// <summary>
    /// Property will contains Updated By User Name
    /// </summary>
    public string UpdatedBy { get; set; }
    /// <summary>
    /// Property will contains Updated Date
    /// </summary>
    public DateTime UpdatedDate { get; set; }
  }
}
