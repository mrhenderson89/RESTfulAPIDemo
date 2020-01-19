using AntAlbelliTechnical.Models.ViewModels;
using System.Collections.Generic;

namespace AntAlbelliTechnical.ViewModels
{
    /// <summary>
    /// The Customer view model, with Orders
    /// </summary>
    public class CustomerOrderViewModel
    {
        /// <summary>
        /// The customer identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The customer name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The customer email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The customer orders
        /// </summary>
        public List<ChildOrderViewModel> Orders { get; set; }
    }
}
