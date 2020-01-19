/// ----------------------------------------------------------------------
/// <summary>
/// Defines the DeleteCustomer tests.
/// </summary>
/// ----------------------------------------------------------------------

namespace AntAlbelliTechnical.Tests.CustomerControllerFixtures
{
    using AntAlbelliTechnical.Models;
    using Moq;
    using NUnit.Framework;
    using System.Net;

    /// <summary>
    /// Delete Customer Tests.
    /// </summary>
    [TestFixture]
    public class DeleteCustomerTests : TestBaseInitialiser
    {
        /// <summary>
        /// Should Return BadRequest if invalid Id is passed.
        /// </summary>
        [Test]
        public void ShouldReturnNotFoundIfInvalidCustomerId()
        {
            // Arrange
            var controller = this.InitCustomerController();

            // Act
            var result = controller.DeleteCustomerById(-1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// Should Return BadRequest if invalid Id is passed.
        /// </summary>
        [Test]
        public void ShouldReturnNotFoundIfCustomerIdNotExist()
        {
            // Arrange
            var controller = this.InitCustomerController();

            // Mock Repository calls
            this.CustomerRepositoryMock.Setup(x => x.Delete<Customer, int>(It.IsAny<int>())).Throws(new System.Exception());

            // Act
            var result = controller.DeleteCustomerById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        /// <summary>
        /// Should Return 204NoContent if customer found.
        /// </summary>
        [Test]
        public void ShouldReturn204IfValidId()
        {
            // Arrange
            var controller = this.InitCustomerController();

            // Mock Repository calls
            this.CustomerRepositoryMock.Setup(x => x.Delete<Customer, int>(It.IsAny<int>()));

            // Act
            var result = controller.DeleteCustomerById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }
    }
}
