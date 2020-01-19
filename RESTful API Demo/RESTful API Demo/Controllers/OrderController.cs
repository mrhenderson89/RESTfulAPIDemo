/// ----------------------------------------------------------------------
/// <summary>
/// Defines the Order controller.
/// </summary>
/// ----------------------------------------------------------------------

namespace AntAlbelliTechnical.Controllers
{
    using AntAlbelliTechnical.Enums;
    using AntAlbelliTechnical.Models;
    using AntAlbelliTechnical.Repositories.Interfaces;
    using AntAlbelliTechnical.ViewModels;
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    /// <summary>
    /// Represents the Customer controller.
    /// </summary>
    [RoutePrefix("api/Orders")]
    public class OrderController : ApiController
    {
        #region Fields

        /// <summary>
        /// the Order Repository.
        /// </summary>
        private readonly IOrderRepository orderRepository;

        /// <summary>
        /// The Customer Repository.
        /// </summary>
        private readonly ICustomerRepository customerRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises the Order controller.
        /// </summary>
        public OrderController(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all Orders.
        /// </summary>
        // GET: /Orders
        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetOrders()
        {
            var orders = this.orderRepository.GetList<Order, int>().Select(o => new OrderViewModel()
            {
                Id = o.Id,
                Price = o.Price,
                CreatedDate = o.CreatedDate,
                OrderStatus = (OrderStatus)o.OrderStatusId,
                Customer = this.customerRepository.Get<Customer, int>(o.CustomerId)
                }).ToList();

            if(!orders.Any())
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound, new HttpError("Unable to find orders at this time"));
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, orders);
        }

        /// <summary>
        /// Gets an Order by Id.
        /// </summary>
        // GET: /Orders/5
        [Route("{id}")]
        [HttpGet]
        public HttpResponseMessage GetOrderById(int id)
        {
            if (id <= 0)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid Order Id"));
            }

            var result = this.orderRepository.Get<Order, int>(id);

            if (result == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound, new HttpError("Unable to find order with this Id"));
            }

            var order = new OrderViewModel()
            {
                Id = result.Id,
                Price = result.Price,
                CreatedDate = result.CreatedDate,
                OrderStatus = (OrderStatus)result.OrderStatusId,
                Customer = this.customerRepository.Get<Customer, int>(result.CustomerId)
            };

            return this.Request.CreateResponse(HttpStatusCode.OK, order);
        }

        /// <summary>
        /// Gets all Orders with a given OrderStatus.
        /// </summary>
        // GET: /Orders
        [Route("Status")]
        [HttpGet]
        public HttpResponseMessage GetOrdersForStatus(string status)
        {
            //return "Hello World!";
            //TODO: Add enum of OrderStatus - Return error message if invalid status is given
            OrderStatus orderStatus;
            Enum.TryParse(status, true, out orderStatus);

            if(!Enum.IsDefined(typeof(OrderStatus), status))
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid Order Status"));
            }


            var orders = this.orderRepository.GetOrdersForStatus(orderStatus).Select(o => new OrderViewModel()
            {
                Id = o.Id,
                Price = o.Price,
                CreatedDate = o.CreatedDate,
                OrderStatus = (OrderStatus)o.OrderStatusId,
                Customer = this.customerRepository.Get<Customer, int>(o.CustomerId)
            }).ToList();

            if(!orders.Any())
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound, new HttpError("Unable to find orders at this time"));
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, orders);
        }

        /// <summary>
        /// Adds an Order
        /// </summary>
        // POST: /Orders
        [Route("")]
        [HttpPost]
        public HttpResponseMessage AddOrder(OrderInputViewModel order)
        {
            if (order.Price < 0)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid Order Price"));
            }

            OrderStatus status;
            Enum.TryParse(order.OrderStatus.ToString(), true, out status);
            var orderEntity = new Order()
            {
                Price = order.Price,
                CreatedDate = DateTime.Now,
                OrderStatusId = (Enum.IsDefined(typeof(OrderStatus), status)) ? (int)status : (int)OrderStatus.Placed,
                CustomerId = order.CustomerId
            };

            var result = this.orderRepository.Add<Order, int>(orderEntity);
            var responseOrder = new OrderViewModel();
            responseOrder.Id = result.Id;
            responseOrder.Price = result.Price;
            responseOrder.CreatedDate = result.CreatedDate;
            responseOrder.OrderStatus = (OrderStatus)result.OrderStatusId;
            responseOrder.Customer = this.customerRepository.Get<Customer, int>(result.CustomerId);

            
            return this.Request.CreateResponse(HttpStatusCode.OK, responseOrder);

        }

        /// <summary>
        /// Updates an Order.
        /// </summary>
        // PUT: /Orders
        [Route("")]
        [HttpPut]
        public HttpResponseMessage UpdateOrder(OrderInputViewModel order)
        {
            if (order.Id <= 0)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid Order Id"));
            }

            if (order.CustomerId <= 0)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid Customer Id"));
            }

            if (order.Price < 0)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid Order Price"));
            }

            if (order.CreatedDate > DateTime.Now)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid Order CreatedDate"));
            }

            OrderStatus status;
            Enum.TryParse(order.OrderStatus.ToString(), true, out status);

            if (!Enum.IsDefined(typeof(OrderStatus), status))
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid Order Status"));
            }

            var orderEntity = new Order()
            {
                Id = order.Id,
                Price = order.Price,
                CreatedDate = order.CreatedDate,
                OrderStatusId = (int)status,
                CustomerId = order.CustomerId
            };
            var result = this.orderRepository.Update(orderEntity, c => c.Id);

            var orderObj = new OrderViewModel()
            {
                Id = result.Id,
                Price = result.Price,
                CreatedDate = result.CreatedDate,
                OrderStatus = (OrderStatus)result.OrderStatusId,
                Customer = this.customerRepository.Get<Customer, int>(result.CustomerId)
            };

            return this.Request.CreateResponse(HttpStatusCode.OK, orderObj);
        }

        /// <summary>
        /// Deletes an Order.
        /// </summary>
        // DELETE: /Orders/5
        [Route("{id}")]
        [HttpDelete]
        public HttpResponseMessage DeleteOrderById(int id)
        {
            if (id <= 0)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Please enter a valid Order Id"));
            }

            try
            {
                this.orderRepository.Delete<Order, int>(id);

                return this.Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound, new HttpError("Unable to find order with this Id"));
            }
        }

        #endregion
    }
}