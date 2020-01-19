using AntAlbelliTechnical.Enums;
using AntAlbelliTechnical.Models;
using System;

namespace AntAlbelliTechnical.ViewModels
{
    /// <summary>
    /// The order
    /// </summary>
    public class OrderViewModel
    {
        /// <summary>
        /// The order identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The order price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// The order status
        /// </summary>
        public OrderStatus OrderStatus { get; set; }

        /// <summary>
        /// The customer Id
        /// </summary>
        public Customer Customer { get; set; }
    }
}
