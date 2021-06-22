//  Copyright © Titian Software Ltd
namespace Titian.BMS.Shipping.DTSIntegration.TasProd.DataTransferObjects
{
  /// <summary>
  /// Class for labware sample Data Object
  /// </summary>
  public class LabwareSampleDto
  {
    /// <summary>
    /// The substance id for Labware Item
    /// </summary>
    public long SubstanceId { get; set; }
    /// <summary>
    ///The solvent name
    /// </summary>
    public string Solvent { get; set; }
    /// <summary>
    /// IsWeighable flag for labware Item
    /// </summary>
    public bool IsWeighable { get; set; }
    /// <summary>
    /// The IsDry flag for labware item 
    /// </summary>
    public bool IsDry { get; set; }
    /// <summary>
    /// The unit to measure concentration
    /// </summary>
    public string ConcentrationUnit { get; set; }
    /// <summary>
    /// The concentration amount
    /// </summary>
    public double Concentration { get; set; }
    /// <summary>
    /// The unit to measure Amount
    /// </summary>
    public string AmountUnit { get; set; }
    /// <summary>
    /// The amount for labware item
    /// </summary>
    public double Amount { get; set; }
    /// <summary>
    /// The X Position of item
    /// </summary>
    public int XPosition { get; set; }
    /// <summary>
    /// The Y Position for Labware Item
    /// </summary>
    public int YPosition { get; set; }
    
  }
}
