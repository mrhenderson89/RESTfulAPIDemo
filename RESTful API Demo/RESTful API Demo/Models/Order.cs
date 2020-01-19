using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntAlbelliTechnical.Models
{
    /// <summary>
    /// The order database entity
    /// </summary>
    public class Order
    {
        /// <summary>
        /// The order identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The customer Id
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// The order price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// The order status Id
        /// </summary>
        public int OrderStatusId { get; set; }
    }
}