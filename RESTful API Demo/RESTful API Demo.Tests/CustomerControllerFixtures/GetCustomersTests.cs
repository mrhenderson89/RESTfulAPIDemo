/// ----------------------------------------------------------------------
/// <summary>
/// Defines the GetCustomers tests.
/// </summary>
/// ----------------------------------------------------------------------

namespace AntAlbelliTechnical.Tests.CustomerControllerFixtures
{
    using AntAlbelliTechnical.Models;
    using AntAlbelliTechnical.Models.ViewModels;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    /// <summary>
    /// Get Customers Tests.
    /// </summary>
    [TestFixture]
    public class GetCustomersTests : TestBaseInitialiser
    {
        /// <summary>
        /// Should Return NotFound if no customers.
        /// </summary>
        [Test]
        public void ShouldReturnNotFoundIfNoCustomers()
        {
            // Arrange
            var controller = this.InitCustomerController();

            // Mock Repository calls
            this.CustomerRepositoryMock.Setup(x => x.GetList<Customer, int>()).Returns(new List<Customer>());

            // Act
            var result = controller.GetCustomers();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        /// <summary>
        /// Should Return NotFound if no customers.
        /// </summary>
        [Test]
        public void ShouldReturn200IfCustomers()
        {
            // Arrange
            var controller = this.InitCustomerController();

            // Mock Repository calls
            this.CustomerRepositoryMock.Setup(x => x.GetList<Customer, int>()).Returns(new List<Customer>()
            {
                new Customer()
                {
                    Id = 1,
                    Name = "Test1",
                    Email = "TestEmail1@mail.com"
                },
                new Customer()
                {
                    Id = 2,
                    Name = "Test2",
                    Email = "TestEmail2@mail.com"
                }
            });

            // Act
            var result = controller.GetCustomers();
            var resultContent = this.GetContentFromResponse<List<CustomerViewModel>>(result);

            // Assert
            Assert.IsNotNull(resultContent);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(2, resultContent.Count);
            Assert.AreEqual("Test1", resultContent.First().Name);
        }

    }
}
