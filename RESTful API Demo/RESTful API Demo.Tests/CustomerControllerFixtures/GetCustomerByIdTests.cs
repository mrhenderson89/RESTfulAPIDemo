
using AntAlbelliTechnical.Models;
using AntAlbelliTechnical.Models.ViewModels;
using Moq;
using NUnit.Framework;
using System.Net;
/// ----------------------------------------------------------------------
/// <summary>
/// Defines the GetCustomerById tests.
/// </summary>
/// ----------------------------------------------------------------------
namespace AntAlbelliTechnical.Tests.CustomerControllerFixtures
{
    /// <summary>
    /// Get Customer By Id Tests.
    /// </summary>
    [TestFixture]
    public class GetCustomerByIdTests : TestBaseInitialiser
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
            var result = controller.GetCustomerById(-1);

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
            this.CustomerRepositoryMock.Setup(x => x.Get<Customer, int>(It.IsAny<int>())).Returns(new Customer()
            {
                Id = 1,
                Name = "Test1",
                Email = "TestEmail1@mail.com"
            });

            // Act
            var result = controller.GetCustomerById(1);
            var resultContent = this.GetContentFromResponse<CustomerViewModel>(result);

            // Assert
            Assert.IsNotNull(resultContent);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual("Test1", resultContent.Name);
        }

        /// <summary>
        /// Should Return BadRequest if no Customer with that Id.
        /// </summary>
        [Test]
        public void ShouldReturnBadRequestIfNoCustomer()
        {
            // Arrange
            var controller = this.InitCustomerController();

            Customer customer = null;

            // Mock Repository calls
            this.CustomerRepositoryMock.Setup(x => x.Get<Customer, int>(It.IsAny<int>())).Returns(customer);

            // Act
            var result = controller.GetCustomerById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
