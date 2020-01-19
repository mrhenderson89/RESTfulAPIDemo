/// ----------------------------------------------------------------------
/// <summary>
/// Defines the OrderInputViewModel.
/// </summary>
/// ----------------------------------------------------------------------

namespace AntAlbelliTechnical.ViewModels
{
    using AntAlbelliTechnical.Enums;
    using AntAlbelliTechnical.Models;
    using System;

    /// <summary>
    /// The order input view model
    /// </summary>
    public class OrderInputViewModel
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
        public int CustomerId { get; set; }
    }
}
