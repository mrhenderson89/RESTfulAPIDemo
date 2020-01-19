/// ----------------------------------------------------------------------
/// <summary>
/// Defines the Order repository interface.
/// </summary>
/// ----------------------------------------------------------------------

namespace AntAlbelliTechnical.Repositories.Interfaces
{
    using AntAlbelliTechnical.Enums;
    using AntAlbelliTechnical.Models;
    using System.Collections.Generic;

    /// <summary>
    /// The Order repository interface.
    /// </summary>
    public interface IOrderRepository : IBaseRepository
    {
        /// <summary>
        /// Get Orders with a given OrderStatus.
        /// </summary>
        IEnumerable<Order> GetOrdersForStatus(OrderStatus status);
    }
}