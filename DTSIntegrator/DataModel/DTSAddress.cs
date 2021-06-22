//  Copyright © Titian Software Ltd
using System;

namespace Titian.BMS.Shipping.DTSIntegration.TasProd.DataModel
{
  /// <summary>
  /// Class for DTS Address
  /// </summary>
  public class DTSAddress
  {
    /// <summary>
    /// Property will contains Address Id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// Property will contains Name of Recipient
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Property will contains Street1
    /// </summary>
    public string Street1 { get; set; }
    /// <summary>
    /// Property will contains Street2
    /// </summary>
    public string Street2 { get; set; }
    /// <summary>
    /// Property will contains City
    /// </summary>
    public string City { get; set; }
    /// <summary>
    /// Property will contains State
    /// </summary>
    public string State { get; set; }
    /// <summary>
    /// Property will contains Zip
    /// </summary>
    public string Zip { get; set; }
    /// <summary>
    /// Property will contains MailStop
    /// </summary>
    public string MailStop { get; set; }
    /// <summary>
    /// Property will contains Country
    /// </summary>
    public string Country { get; set; }
    /// <summary>
    /// Property will contains EMail
    /// </summary>
    public string EMail { get; set; }
    /// <summary>
    /// Property will contains SiteCode
    /// </summary>
    public string SiteCode { get; set; }
    /// <summary>
    /// Property will contains Phone
    /// </summary>
    public string Phone { get; set; }
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
