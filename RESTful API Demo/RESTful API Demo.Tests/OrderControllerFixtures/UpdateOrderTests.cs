/// ----------------------------------------------------------------------
/// <summary>
/// Defines the UpdateOrder tests.
/// </summary>
/// ----------------------------------------------------------------------

namespace AntAlbelliTechnical.Tests.CustomerControllerFixtures
{
    using AntAlbelliTechnical.Enums;
    using AntAlbelliTechnical.Models;
    using AntAlbelliTechnical.Models.ViewModels;
    using AntAlbelliTechnical.ViewModels;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Net;

    /// <summary>
    /// Update Order Tests.
    /// </summary>
    [TestFixture]
    public class UpdateOrderTests : TestBaseInitialiser
    {
        /// <summary>
        /// Should Return BadRequest if invalid Id is passed.
        /// </summary>
        [Test]
        public void ShouldReturnBadRequestIfInvalidOrderId()
        {
            // Arrange
            var controller = this.InitOrderController();
            OrderInputViewModel order = new OrderInputViewModel()
            {
                Id = 0,
                Price = 10,
                CreatedDate = DateTime.Now,
                CustomerId = 1
            };

            // Act
            var result = controller.UpdateOrder(order);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// Should Return BadRequest if invalid CustomerId is passed.
        /// </summary>
        [Test]
        public void ShouldReturnBadRequestIfInvalidCustomerId()
        {
            // Arrange
            var controller = this.InitOrderController();
            OrderInputViewModel order = new OrderInputViewModel()
            {
                Id = 1,
                Price = 10,
                CreatedDate = DateTime.Now,
                CustomerId = -1
            };

            // Act
            var result = controller.UpdateOrder(order);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// Should Return BadRequest if invalid Price is passed.
        /// </summary>
        [Test]
        public void ShouldReturnBadRequestIfInvalidPrice()
        {
            // Arrange
            var controller = this.InitOrderController();
            OrderInputViewModel order = new OrderInputViewModel()
            {
                Id = 1,
                Price = -10,
                CreatedDate = DateTime.Now,
                CustomerId = 1
            };

            // Act
            var result = controller.UpdateOrder(order);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// Should Return BadRequest if invalid CreatedDate is passed.
        /// </summary>
        [Test]
        public void ShouldReturnBadRequestIfInvalidCreatedDate()
        {
            // Arrange
            var controller = this.InitOrderController();
            OrderInputViewModel order = new OrderInputViewModel()
            {
                Id = 1,
                Price = 10,
                CreatedDate = DateTime.Now.AddDays(1),
                CustomerId = 1
            };

            // Act
            var result = controller.UpdateOrder(order);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// Should Return 200 if order updated.
        /// </summary>
        [Test]
        public void ShouldReturn200IfValidId()
        {
            // Arrange
            var controller = this.InitOrderController();
            OrderInputViewModel order = new OrderInputViewModel()
            {
                Id = 1,
                Price = 10,
                CreatedDate = DateTime.Now,
                OrderStatus = OrderStatus.Placed,
                CustomerId = 1
            };

            this.OrderRepositoryMock.Setup(x => x.Update(It.IsAny<Order>(), It.IsAny<Func<Order, int>>())).Returns(new Order()
            {
                Id = 1,
                Price = 10,
                CreatedDate = DateTime.Now,
                CustomerId = 1
            });
            // Mock Repository calls
            this.CustomerRepositoryMock.Setup(x => x.Get<Customer, int>(1)).Returns(new Customer()
            {
                Id = 1,
                Name = "Test1",
                Email = "TestEmail1@mail.com"
            });

            // Act
            var result = controller.UpdateOrder(order);
            var resultContent = this.GetContentFromResponse<OrderViewModel>(result);

            // Assert
            Assert.IsNotNull(resultContent);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual("Test1", resultContent.Customer.Name);
        }
    }
}
