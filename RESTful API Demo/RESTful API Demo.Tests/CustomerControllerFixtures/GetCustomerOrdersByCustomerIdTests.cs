/// ----------------------------------------------------------------------
/// <summary>
/// Defines the GetCustomerOrdersByCustomerId tests.
/// </summary>
/// ----------------------------------------------------------------------

namespace AntAlbelliTechnical.Tests.CustomerControllerFixtures
{
    using AntAlbelliTechnical.Models.ViewModels;
    using AntAlbelliTechnical.ViewModels;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    /// <summary>
    /// Get Customer  And Orders By Customer Id Tests.
    /// </summary>
    [TestFixture]
    public class GetCustomerOrdersByCustomerIdTests : TestBaseInitialiser
    {
        /// <summary>
        /// Should Return BadRequest if invalid customerId is passed.
        /// </summary>
        [Test]
        public void ShouldReturnBadRequestIfInvalidId()
        {
            // Arrange
            var controller = this.InitCustomerController();

            // Act
            var result = controller.GetCustomerOrdersByCustomerId(-1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// Should Return 200 if customer found.
        /// </summary>
        [Test]
        public void ShouldReturn200IfValidId()
        {
            // Arrange
            var controller = this.InitCustomerController();

            // Mock Repository calls
            this.CustomerRepositoryMock.Setup(x => x.GetCustomerWithOrders(It.IsAny<int>())).Returns(new CustomerOrderViewModel()
            {
                Id = 1,
                Name = "Test1",
                Email = "test1@mail.com",
                Orders = new List<ChildOrderViewModel>()
                    {
                        new ChildOrderViewModel()
                        {
                            Id = 1
                        }
                    }
            });

            // Act
            var result = controller.GetCustomerOrdersByCustomerId(1);
            var resultContent = this.GetContentFromResponse<CustomerOrderViewModel>(result);

            // Assert
            Assert.IsNotNull(resultContent);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual("Test1", resultContent.Name);
            Assert.AreEqual(1, resultContent.Orders.First().Id);
        }

        /// <summary>
        /// Should Return BadRequest if no Customer with that Id.
        /// </summary>
        [Test]
        public void ShouldReturnBadRequestIfNoCustomer()
        {
            // Arrange
            var controller = this.InitCustomerController();

            CustomerOrderViewModel customer = null;

            // Mock Repository calls
            this.CustomerRepositoryMock.Setup(x => x.GetCustomerWithOrders(It.IsAny<int>())).Returns(customer);

            // Act
            var result = controller.GetCustomerOrdersByCustomerId(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
