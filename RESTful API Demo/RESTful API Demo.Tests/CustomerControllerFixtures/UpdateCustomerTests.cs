/// ----------------------------------------------------------------------
/// <summary>
/// Defines the UpdateCustomer tests.
/// </summary>
/// ----------------------------------------------------------------------

namespace AntAlbelliTechnical.Tests.CustomerControllerFixtures
{
    using AntAlbelliTechnical.Models;
    using AntAlbelliTechnical.Models.ViewModels;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Net;

    /// <summary>
    /// Update Customer Tests.
    /// </summary>
    [TestFixture]
    public class UpdateCustomerTests : TestBaseInitialiser
    {
        /// <summary>
        /// Should Return BadRequest if invalid Id is passed.
        /// </summary>
        [Test]
        public void ShouldReturnBadRequestIfInvalidCustomerId()
        {
            // Arrange
            var controller = this.InitCustomerController();
            CustomerViewModel customer = new CustomerViewModel()
            {
                Id=0,
                Name = "Test1",
                Email = "TestEmail@mail.com"
            };

            // Act
            var result = controller.UpdateCustomer(customer);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

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
                Id = 1,
                Name = "",
                Email = "TestEmail@mail.com"
            };

            // Act
            var result = controller.UpdateCustomer(customer);

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
                Id = 1,
                Name = "Test1",
                Email = ""
            };

            // Act
            var result = controller.UpdateCustomer(customer);

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
                Id = 1,
                Name = "Test1",
                Email = "Test1"
            };

            // Act
            var result = controller.UpdateCustomer(customer);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// Should Return 200 if customer updated.
        /// </summary>
        [Test]
        public void ShouldReturn200IfValidId()
        {
            // Arrange
            var controller = this.InitCustomerController();
            CustomerViewModel customer = new CustomerViewModel()
            {
                Id = 1,
                Name = "Test1",
                Email = "Test1@mail.com"
            };
           

            // Mock Repository calls
            this.CustomerRepositoryMock.Setup(x => x.Update(It.IsAny<Customer>(), It.IsAny<Func<Customer, int>>())).Returns(new Customer()
            {
                Id = 1,
                Name = "Test1",
                Email = "TestEmail1@mail.com"
            });

            // Act
            var result = controller.UpdateCustomer(customer);
            var resultContent = this.GetContentFromResponse<CustomerViewModel>(result);

            // Assert
            Assert.IsNotNull(resultContent);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual("Test1", resultContent.Name);
        }
    }
}
