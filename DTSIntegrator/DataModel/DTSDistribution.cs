//  Copyright © Titian Software Ltd
using System;
using System.Collections.Generic;

namespace Titian.BMS.Shipping.DTSIntegration.TasProd.DataModel
{
  /// <summary>
  /// Class for DTS distribution
  /// </summary>
 public class DTSDistribution
  {
    /// <summary>
    /// Property will contains Distribution Id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// Property will contains Barcode 
    /// </summary>
    public string Barcode { get; set; }
    /// <summary>
    /// Property will contains Prepared By 
    /// </summary>
    public string PreparedBy { get; set; }
    /// <summary>
    /// Property will contain Recipient Name
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
    /// Property will contains Shipping Level
    /// </summary>
    public string ShippingLevel { get; set; }
    /// <summary>
    /// Property will contains Distribution Status
    /// </summary>
    public string Status { get; set; }
    /// <summary>
    /// Property will contains comments
    /// </summary>
    public string Comments { get; set; }
    /// <summary>
    /// Property will contains Distribution Group Id
    /// </summary>
    public int DistributionGroupId { get; set; }

    /// <summary>
    /// Property will contains Chemical Id
    /// </summary>
    public int ChemicalId { get; set; }
    /// <summary>
    /// Property will contains chemical details
    /// </summary>
    public DTSChemical Chemical { get; set; }
    /// <summary>
    /// Property will contains Entity Type 
    /// </summary>
    public string EntityType { get; set; }
    /// <summary>
    /// Property will contains Handling Category
    /// </summary>
    public string HandlingCategory { get; set; }
    /// <summary>
    /// Property will contains List of DTS status object
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
    /// Property will contains Updated By User Name
    /// </summary>
    public string UpdatedBy { get; set; }
    /// <summary>
    /// Property will contains Updated Date
    /// </summary>
    public DateTime UpdatedDate { get; set; }

  }
}
