/// ----------------------------------------------------------------------
/// <summary>
/// Defines the GetOrdersForStatus tests.
/// </summary>
/// ----------------------------------------------------------------------

namespace AntAlbelliTechnical.Tests.OrderControllerFixtures
{
    using AntAlbelliTechnical.Enums;
    using AntAlbelliTechnical.Models;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    /// <summary>
    /// Get Orders By OrderStatus Tests.
    /// </summary>
    [TestFixture]
    public class GetOrdersForStatusTests : TestBaseInitialiser
    {
        /// <summary>
        /// Should Return BadRequest if invalid OrderStatus is passed.
        /// </summary>
        [Test]
        public void ShouldReturnBadRequestIfInvalidOrderStatus()
        {
            // Arrange
            var controller = this.InitOrderController();

            // Act
            var result = controller.GetOrdersForStatus("InvalidOrderStatus");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// Should Return 200 if Orders have OrderStatus.
        /// </summary>
        [Test]
        public void Should200OKIfOrdersHaveStatus()
        {
            // Arrange
            var controller = this.InitOrderController();

            // Mock Repository calls
            this.OrderRepositoryMock.Setup(x => x.GetOrdersForStatus(It.IsAny<OrderStatus>())).Returns(new List<Order>()
            {
               new Order()
            {
                Id = 1,
                Price = 10,
                CreatedDate = DateTime.Now,
                CustomerId = 1
            }
            });
            this.CustomerRepositoryMock.Setup(x => x.Get<Customer, int>(1)).Returns(new Customer()
            {
                Id = 1,
                Name = "Test1",
                Email = "TestEmail1@mail.com"
            });

            // Act
            var result = controller.GetOrdersForStatus(OrderStatus.Paid.ToString());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        /// <summary>
        /// Should Return NotFound if no Orders have OrderStatus.
        /// </summary>
        [Test]
        public void ShouldReturnNotFoundIfNoOrdersWithOrderStatus()
        {
            // Arrange
            var controller = this.InitOrderController();

            // Mock Repository calls
            this.OrderRepositoryMock.Setup(x => x.GetOrdersForStatus(It.IsAny<OrderStatus>())).Returns(Enumerable.Empty<Order>());

            // Act
            var result = controller.GetOrdersForStatus(OrderStatus.Paid.ToString());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
