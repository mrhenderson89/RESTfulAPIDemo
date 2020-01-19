using AntAlbelliTechnical.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntAlbelliTechnical.Models.ViewModels
{
    /// <summary>
    /// The View Model for Order as child of Customer
    /// </summary>
    public class ChildOrderViewModel
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
        public string OrderStatus { get; set; }
    }
}