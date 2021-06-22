//  Copyright © Titian Software Ltd
using System.Net.Http;
using System.Threading.Tasks;
using Titian.BMS.Shipping.DTSIntegration.TasProd.DataModel;

namespace Titian.BMS.Shipping.DTSIntegration.TasProd
{
  /// <summary>
  /// Interface DTS Integrator Api Helper
  /// </summary>
  public interface IDTSIntegratorApiHelper
  {
    /// <summary>
    /// Create shipment method used to call the post api of DTS service to submit shipment
    /// </summary>
    /// <param name="dtsShipment">DTSShipment object</param>
    /// <returns>Task<HttpResponseMessage> object</returns>
    Task<HttpResponseMessage> CreateShipment(DTSShipment dtsShipment);

    /// <summary>
    /// Get shipment method used to call get api of DTS service to get shipment object by shipment id
    /// </summary>
    /// <param name="shipmentId">shipment id</param>
    /// <returns>Task<HttpResponseMessage> object</returns>
    Task<HttpResponseMessage> GetShipment(int shipmentId);

    /// <summary>
    /// Create Distribution Group method used to call post api of DTS service to submint distribution group object
    /// </summary>
    /// <param name="dTSDistributionGroup">DTSDistributionGroup object</param>
    /// <returns>Task<HttpResponseMessage> object</returns>
    Task<HttpResponseMessage> CreateDistributionGroup(DTSDistributionGroup dTSDistributionGroup);

    /// <summary>
    /// Create distritubtion method used to call post api of DTS service to submit distribution object
    /// </summary>
    /// <param name="dtsDistribution"> DTSDistribution object</param>
    /// <returns>Task<HttpResponseMessage> object</returns>
    Task<HttpResponseMessage> CreateDistribution(DTSDistribution dtsDistribution);

    /// <summary>
    /// Update Distribution Group Status method used to call the post api of DTS service to Update Distribution Group Status
    /// </summary>
    /// <param name="dtsStatus">DTSStatus object</param>
    /// <returns>Task<HttpResponseMessage> object</returns>
    Task<HttpResponseMessage> UpdateDistributionGroupStatus(DTSStatus dtsStatus);
  }
}
