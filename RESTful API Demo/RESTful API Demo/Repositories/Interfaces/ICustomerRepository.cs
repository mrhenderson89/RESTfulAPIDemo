/// ----------------------------------------------------------------------
/// <summary>
/// Defines the Customer repository interface.
/// </summary>
/// ----------------------------------------------------------------------
namespace AntAlbelliTechnical.Repositories.Interfaces
{
    using AntAlbelliTechnical.ViewModels;
    using Models;
    using System.Collections.Generic;

    /// <summary>
    /// The Customer repository interface.
    /// </summary>
    public interface ICustomerRepository : IBaseRepository
    {
        /// <summary>
        /// Get Customer and Order entities by customer Id.
        /// </summary>
        CustomerOrderViewModel GetCustomerWithOrders(int customerId);
    }
}