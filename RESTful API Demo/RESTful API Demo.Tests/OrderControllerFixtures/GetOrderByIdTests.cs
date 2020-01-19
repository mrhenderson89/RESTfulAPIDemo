/// ----------------------------------------------------------------------
/// <summary>
/// Defines the GetOrderById tests.
/// </summary>
/// ----------------------------------------------------------------------

namespace AntAlbelliTechnical.Tests.CustomerControllerFixtures
{
    using AntAlbelliTechnical.Models;
    using AntAlbelliTechnical.ViewModels;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Net;

    /// <summary>
    /// Get Order By Id Tests.
    /// </summary>
    [TestFixture]
    public class GetOrderByIdTests : TestBaseInitialiser
    {
        /// <summary>
        /// Should Return BadRequest if invalid orderId is passed.
        /// </summary>
        [Test]
        public void ShouldReturnBadRequestIfInvalidId()
        {
            // Arrange
            var controller = this.InitOrderController();

            // Act
            var result = controller.GetOrderById(-1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// Should Return 200 if order found.
        /// </summary>
        [Test]
        public void ShouldReturn200IfValidId()
        {
            // Arrange
            var controller = this.InitOrderController();

            // Mock Repository calls
            this.OrderRepositoryMock.Setup(x => x.Get<Order, int>(It.IsAny<int>())).Returns(new Order()
            {
                Id = 1,
                Price = 10,
                CreatedDate = DateTime.Now,
                CustomerId = 1
            });
            this.CustomerRepositoryMock.Setup(x => x.Get<Customer, int>(1)).Returns(new Customer()
            {
                Id = 1,
                Name = "Test1",
                Email = "TestEmail1@mail.com"
            });

            // Act
            var result = controller.GetOrderById(1);
            var resultContent = this.GetContentFromResponse<OrderViewModel>(result);

            // Assert
            Assert.IsNotNull(resultContent);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual("Test1", resultContent.Customer.Name);
        }

        /// <summary>
        /// Should Return BadRequest if no Order with that Id.
        /// </summary>
        [Test]
        public void ShouldReturnBadRequestIfNoOrder()
        {
            // Arrange
            var controller = this.InitOrderController();

            Order order = null;

            // Mock Repository calls
            this.OrderRepositoryMock.Setup(x => x.Get<Order, int>(It.IsAny<int>())).Returns(order);

            // Act
            var result = controller.GetOrderById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
