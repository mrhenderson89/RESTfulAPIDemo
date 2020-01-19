/// ----------------------------------------------------------------------
/// <summary>
/// Defines the AddOrder tests.
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
    using System.Net;

    /// <summary>
    /// Add Order Tests.
    /// </summary>
    [TestFixture]
    public class AddOrderTests : TestBaseInitialiser
    {
        /// <summary>
        /// Should Return BadRequest if invalid Price is passed.
        /// </summary>
        [Test]
        public void ShouldReturnBadRequestIfNoOrderPrice()
        {
            // Arrange
            var controller = this.InitOrderController();
            OrderInputViewModel order = new OrderInputViewModel()
            {
                Price = -1,
                OrderStatus = OrderStatus.Paid
            };

            // Act
            var result = controller.AddOrder(order);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// Should Return 200 if order added.
        /// </summary>
        [Test]
        public void ShouldReturn200IfValidId()
        {
            // Arrange
            var controller = this.InitOrderController();
            OrderInputViewModel order = new OrderInputViewModel()
            {
                Price = 10,
                OrderStatus = OrderStatus.Paid,
                CustomerId = 1
            };

            // Mock Repository calls
            this.OrderRepositoryMock.Setup(x => x.Add<Order, int>(It.IsAny<Order>())).Returns(new Order()
            {
                Id = 1,
                Price = 10,
                OrderStatusId = (int)OrderStatus.Paid,
                CustomerId = 1
            });
            this.CustomerRepositoryMock.Setup(x => x.Get<Customer, int>(1)).Returns(new Customer()
            {
                Id = 1,
                Name = "Test1",
                Email = "TestEmail1@mail.com"
            });

            // Act
            var result = controller.AddOrder(order);
            var resultContent = this.GetContentFromResponse<OrderViewModel>(result);

            // Assert
            Assert.IsNotNull(resultContent);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual("Test1", resultContent.Customer.Name);
        }
    }
}
