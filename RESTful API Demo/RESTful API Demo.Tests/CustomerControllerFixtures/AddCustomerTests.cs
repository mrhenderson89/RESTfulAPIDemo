/// ----------------------------------------------------------------------
/// <summary>
/// Defines the AddCustomer tests.
/// </summary>
/// ----------------------------------------------------------------------

namespace AntAlbelliTechnical.Tests.CustomerControllerFixtures
{
    using AntAlbelliTechnical.Models;
    using AntAlbelliTechnical.Models.ViewModels;
    using Moq;
    using NUnit.Framework;
    using System.Linq;
    using System.Net;

    /// <summary>
    /// Add Customer Tests.
    /// </summary>
    [TestFixture]
    public class AddCustomerTests : TestBaseInitialiser
    {
        /// <summary>
        /// Should Return BadRequest if empty name is passed.
        /// </summary>
        [Test]
        public void ShouldReturnBadRequestIfNoCustomerName()
        {
            // Arrange
            var controller = this.InitCustomerController();
            CustomerViewModel customer = new CustomerViewModel()
            {
                Name = "",
                Email = "TestEmail@mail.com"
            };

            // Act
            var result = controller.AddCustomer(customer);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// Should Return BadRequest if empty email is passed.
        /// </summary>
        [Test]
        public void ShouldReturnBadRequestIfNoCustomerEmail()
        {
            // Arrange
            var controller = this.InitCustomerController();
            CustomerViewModel customer = new CustomerViewModel()
            {
                Name = "Test1",
                Email = ""
            };

            // Act
            var result = controller.AddCustomer(customer);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// Should Return BadRequest if empty email is passed.
        /// </summary>
        [Test]
        public void ShouldReturnBadRequestIfInvalidCustomerEmail()
        {
            // Arrange
            var controller = this.InitCustomerController();
            CustomerViewModel customer = new CustomerViewModel()
            {
                Name = "Test1",
                Email = "Test1"
            };

            // Act
            var result = controller.AddCustomer(customer);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// Should Return 200 if customer added.
        /// </summary>
        [Test]
        public void ShouldReturn200IfValidId()
        {
            // Arrange
            var controller = this.InitCustomerController();
            CustomerViewModel customer = new CustomerViewModel()
            {
                Name = "Test1",
                Email = "Test1@mail.com"
            };

            // Mock Repository calls
            this.CustomerRepositoryMock.Setup(x => x.Add<Customer, int>(It.IsAny<Customer>())).Returns(new Customer()
            {
                Id = 1,
                Name = "Test1",
                Email = "TestEmail1@mail.com"
            });

            // Act
            var result = controller.AddCustomer(customer);
            var resultContent = this.GetContentFromResponse<CustomerViewModel>(result);

            // Assert
            Assert.IsNotNull(resultContent);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual("Test1", resultContent.Name);
        }
    }
}
