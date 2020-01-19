/// ----------------------------------------------------------------------
/// <summary>
/// Defines the GetOrders tests.
/// </summary>
/// ----------------------------------------------------------------------

namespace AntAlbelliTechnical.Tests.CustomerControllerFixtures
{
    using AntAlbelliTechnical.Models;
    using AntAlbelliTechnical.Models.ViewModels;
    using AntAlbelliTechnical.ViewModels;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    /// <summary>
    /// Get Orders Tests.
    /// </summary>
    [TestFixture]
    public class GetOrdersTests : TestBaseInitialiser
    {
        /// <summary>
        /// Should Return NotFound if no orders.
        /// </summary>
        [Test]
        public void ShouldReturnNotFoundIfNoCustomers()
        {
            // Arrange
            var controller = this.InitOrderController();

            // Mock Repository calls
            this.OrderRepositoryMock.Setup(x => x.GetList<Order, int>()).Returns(new List<Order>());

            // Act
            var result = controller.GetOrders();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        /// <summary>
        /// Should Return NotFound if no orders.
        /// </summary>
        [Test]
        public void ShouldReturn200IfOrders()
        {
            // Arrange
            var controller = this.InitOrderController();

            // Mock Repository calls
            this.OrderRepositoryMock.Setup(x => x.GetList<Order, int>()).Returns(new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    Price = 10,
                    CreatedDate = DateTime.Now,
                    CustomerId = 1
                },
                new Order()
                {
                    Id = 2,
                    Price = 20,
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
            var result = controller.GetOrders();
            var resultContent = this.GetContentFromResponse<List<OrderViewModel>>(result);

            // Assert
            Assert.IsNotNull(resultContent);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(2, resultContent.Count);
            Assert.AreEqual(10, resultContent.First().Price);
        }

    }
}
