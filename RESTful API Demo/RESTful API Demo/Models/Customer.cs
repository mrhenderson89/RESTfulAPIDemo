using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntAlbelliTechnical.Models
{
    /// <summary>
    /// The customer database entity
    /// </summary>
    public class Customer
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
    }
}