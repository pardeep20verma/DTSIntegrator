//  Copyright © Titian Software Ltd
using System;

namespace Titian.BMS.Shipping.DTSIntegration.TasProd.DataModel
{
  /// <summary>
  /// class for DTS chemical
  /// </summary>
 public class DTSChemical
  {
    /// <summary>
    /// Property will contains Chemical Id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// Property will contains Amount of chemical
    /// </summary>
    public double Amount { get; set; }
    /// <summary>
    /// Property will contains Amount Unit
    /// </summary>
    public string AmountUnit { get; set; }
    /// <summary>
    /// Property will contains Concentration
    /// </summary>
    public double Concentration { get; set; }
    /// <summary>
    /// Property will contains Concentration Unit
    /// </summary>
    public string ConcentrationUnit { get; set; }
    /// <summary>
    /// Property will contains Is VST
    /// </summary>
    public bool IsVST { get; set; }
    /// <summary>
    /// Property will contains Is Weighable
    /// </summary>
    public bool IsWeighable { get; set; }
    /// <summary>
    /// Property will contains Registration Id (Mosaic Substance Id)
    /// </summary>
    public long RegistrationId { get; set; }
    /// <summary>
    /// Property will contains Solvent
    /// </summary>
    public string Solvent { get; set; }
    /// <summary>
    /// Property will contains Sample State
    /// </summary>
    public string SampleState { get; set; }
    /// <summary>
    /// Property will contains Container Type
    /// </summary>
    public string ContainerType { get; set; }
    /// <summary>
    /// Property will contains Y Position of Labware item
    /// </summary>
    public int ContainerRow { get; set; }
    /// <summary>
    /// Property will contains X Position of Labware item
    /// </summary>
    public int ContainerColumn { get; set; }
    /// <summary>
    /// Property will contains Created By User name
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
