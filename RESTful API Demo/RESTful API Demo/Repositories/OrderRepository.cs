/// ----------------------------------------------------------------------
/// <summary>
/// Defines the Order repository.
/// </summary>
/// ----------------------------------------------------------------------

namespace AntAlbelliTechnical.Repositories
{
    using AntAlbelliTechnical.Enums;
    using AntAlbelliTechnical.Models;
    using AntAlbelliTechnical.Repositories.Interfaces;
    using Dapper;
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// The Order repository.
    /// </summary>
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        /// <summary>
        /// Get Orders with a given OrderStatus.
        /// </summary>
        public IEnumerable<Order> GetOrdersForStatus(OrderStatus status)
        {
            int statusId = (int)status;
            using (IDbConnection db = this.Create())
            {
                db.Open();

                var orders = db.Query<Order>(@"SELECT Id, CustomerId, Price, CreatedDate, OrderStatusId FROM [Order] WHERE OrderStatusId = @Status",
                    new { Status = statusId });

                return orders;
            }
        }
    }
}