/// ----------------------------------------------------------------------
/// <summary>
/// Defines the Customer repository.
/// </summary>
/// ----------------------------------------------------------------------

namespace AntAlbelliTechnical.Repositories
{
    using Models;
    using Interfaces;
    using Dapper;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using AntAlbelliTechnical.Enums;
    using AntAlbelliTechnical.Models.ViewModels;
    using AntAlbelliTechnical.ViewModels;

    /// <summary>
    /// The Customer repository.
    /// </summary>
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        /// <summary>
        /// Get Customer and Order entities by customer Id.
        /// </summary>
        public CustomerOrderViewModel GetCustomerWithOrders(int customerId)
        {
            using (IDbConnection db = this.Create())
            {
                db.Open();

                var lookup = new Dictionary<int, CustomerOrderViewModel>();
                    db.Query<Customer, Order, CustomerOrderViewModel>(@"
                    SELECT c.*, o.*
                    FROM Customer c
                    INNER JOIN [Order] o ON c.Id = o.CustomerId 
                    WHERE c.Id = @CustomerId
                    ", (c, o) =>
                    {   
                        CustomerOrderViewModel customer;
                        if (!lookup.TryGetValue(c.Id, out customer))
                        {
                            lookup.Add(c.Id, customer = new CustomerOrderViewModel()
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Email = c.Email
                            });
                        }
                        if (customer.Orders == null)
                            customer.Orders = new List<ChildOrderViewModel>();
                            customer.Orders.Add(new ChildOrderViewModel()
                            {
                                Id = o.Id,
                                Price = o.Price,
                                CreatedDate = o.CreatedDate,
                                OrderStatus = ((OrderStatus)o.OrderStatusId).ToString()
                    });
                            return customer;
                    }, param: new { CustomerId = customerId }).AsQueryable();

                var resultList = lookup.Values;

                return resultList.FirstOrDefault();
            }
        }
    }
}