/// ----------------------------------------------------------------------
/// <summary>
/// Defines the Customer controller.
/// </summary>
/// ----------------------------------------------------------------------

namespace AntAlbelliTechnical.Controllers
{
    using AntAlbelliTechnical.Models;
    using AntAlbelliTechnical.Models.ViewModels;
    using AntAlbelliTechnical.Repositories.Interfaces;
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text.RegularExpressions;
    using System.Web.Http;

    /// <summary>
    /// Represents the Customer controller.
    /// </summary>
    [RoutePrefix("api/Customers")]
    public class CustomerController : ApiController
    {
        #region Fields

        /// <summary>
        /// The Customer Repository.
        /// </summary>
        private readonly ICustomerRepository customerRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises the Customer controller.
        /// </summary>
        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the list of customers.
        /// </summary>
        // GET: /Customers
        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetCustomers()
        {
            var customers = this.customerRepository.GetList<Customer, int>().Select(c => new CustomerViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email
            }).ToList();

            if (!customers.Any())
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound, new HttpError("Unable to find customers at this time"));
            }
            
            return this.Request.CreateResponse(HttpStatusCode.OK, customers);
        }

        /// <summary>
        /// Gets a customer by Id.
        /// </summary>
        // GET: /Customers/5
        [Route("{id}")]
        [HttpGet]
        public HttpResponseMessage GetCustomerById(int id)
        {
            if (id <= 0)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid Customer Id"));
            }

            var result = this.customerRepository.Get<Customer, int>(id);

            if (result == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound, new HttpError("Unable to find customer with this Id"));
            }

            var customer = new CustomerViewModel()
            {
                Id = result.Id,
                Name = result.Name,
                Email = result.Email
            };

            return this.Request.CreateResponse(HttpStatusCode.OK, customer);
        }

        /// <summary>
        /// Gets a customer by Id, with their Order details included.
        /// </summary>
        // GET: /Customers/5/orders
        [Route("{customerId:int}/orders")]
        [HttpGet]
        public HttpResponseMessage GetCustomerOrdersByCustomerId(int customerId)
        {
            if (customerId <= 0)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid Customer Id"));
            }
            
            var result = this.customerRepository.GetCustomerWithOrders(customerId);

            if (result == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound, new HttpError("Unable to find customer with this Id"));
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Adds a Customer.
        /// </summary>
        // POST: /Customers
        [Route("")]
        [HttpPost]
        public HttpResponseMessage AddCustomer(CustomerViewModel customer)
        {
            if (string.IsNullOrWhiteSpace(customer.Name))
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid Customer Name"));
            }

            if (string.IsNullOrWhiteSpace(customer.Email))
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid Customer Email"));
            }

            Regex regex = new Regex(@"^\S+@\S+$");
            Match match = regex.Match(customer.Email);
            if (!match.Success)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid Customer email address"));
            }

            var customerEntity = new Customer()
            {
                Name = customer.Name,
                Email = customer.Email
            };

            var result = this.customerRepository.Add<Customer, int>(customerEntity);
            customer.Id = result.Id;

            return this.Request.CreateResponse(HttpStatusCode.OK, customer);
        }

        /// <summary>
        /// Updates a Customer.
        /// </summary>
        // PUT: /Customers
        [Route("")]
        [HttpPut]
        public HttpResponseMessage UpdateCustomer(CustomerViewModel customer)
        {
            if (customer.Id <= 0)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid Customer Id"));
            }

            if (string.IsNullOrWhiteSpace(customer.Name))
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid Customer Name"));
            }

            if (string.IsNullOrWhiteSpace(customer.Email))
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid Customer Email"));
            }

            Regex regex = new Regex(@"^\S+@\S+$");
            Match match = regex.Match(customer.Email);
            if (!match.Success)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid Customer Email address"));
            }

            var customerEntity = new Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            };

            var result = this.customerRepository.Update(customerEntity, c=> c.Id);

            var customerObj = new CustomerViewModel()
            {
                Id = result.Id,
                Name = result.Name,
                Email = result.Email
            };

            return this.Request.CreateResponse(HttpStatusCode.OK, customerObj);
        }

        /// <summary>
        /// Deletes a Customer.
        /// </summary>
        // DELETE: /Customers/5
        [Route("{id}")]
        [HttpDelete]
        public HttpResponseMessage DeleteCustomerById(int id)
        {
            if (id <= 0)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid Customer Id"));
            }

            try
            {
                this.customerRepository.Delete<Customer, int>(id);

                return this.Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch(Exception ex)
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound, new HttpError("Unable to find customer with this Id"));
            }
        }

        #endregion
    }
}