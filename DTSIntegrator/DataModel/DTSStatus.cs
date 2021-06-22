//  Copyright © Titian Software Ltd
using System;

namespace Titian.BMS.Shipping.DTSIntegration.TasProd.DataModel
{
  public class DTSStatus
  {

    /// <summary>
    /// Property will contains DTS Status Id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// Property will contains DTS ShipmentId
    /// </summary>
    public int ShipmentId { get; set; }
    /// <summary>
    /// Property will contains Distribution Group Id
    /// </summary>
    public int DistributionGroupId { get; set; }
    /// <summary>
    /// Property will contains Distribution Id
    /// </summary>
    public int DistributionId { get; set; }
    /// <summary>
    /// Property will contains Status Date
    /// </summary>
    public DateTime StatusDate { get; set; }
    /// <summary>
    /// Property will contains Status
    /// </summary>
    public string Status { get; set; }
    /// <summary>
    /// Property will contains Created By 
    /// /summary>
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
