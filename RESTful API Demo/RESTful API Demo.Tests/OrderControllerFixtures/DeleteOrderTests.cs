
using AntAlbelliTechnical.Models;
using Moq;
using NUnit.Framework;
using System.Net;
/// ----------------------------------------------------------------------
/// <summary>
/// Defines the DeleteOrder tests.
/// </summary>
/// ----------------------------------------------------------------------
namespace AntAlbelliTechnical.Tests.CustomerControllerFixtures
{
    /// <summary>
    /// Delete Order Tests.
    /// </summary>
    [TestFixture]
    public class DeleteOrderTests : TestBaseInitialiser
    {
        /// <summary>
        /// Should Return BadRequest if invalid Id is passed.
        /// </summary>
        [Test]
        public void ShouldReturnNotFoundIfInvalidOrderId()
        {
            // Arrange
            var controller = this.InitOrderController();

            // Act
            var result = controller.DeleteOrderById(-1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// Should Return BadRequest if invalid Id is passed.
        /// </summary>
        [Test]
        public void ShouldReturnNotFoundIfOrderIdNotExist()
        {
            // Arrange
            var controller = this.InitOrderController();

            // Mock Repository calls
            this.OrderRepositoryMock.Setup(x => x.Delete<Order, int>(It.IsAny<int>())).Throws(new System.Exception());

            // Act
            var result = controller.DeleteOrderById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        /// <summary>
        /// Should Return 204NoContent if order found.
        /// </summary>
        [Test]
        public void ShouldReturn204IfValidId()
        {
            // Arrange
            var controller = this.InitOrderController();

            // Mock Repository calls
            this.OrderRepositoryMock.Setup(x => x.Delete<Order, int>(It.IsAny<int>()));

            // Act
            var result = controller.DeleteOrderById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }
    }
}
