/// ----------------------------------------------------------------------
/// <summary>
/// Defines the test base.
/// </summary>
/// ----------------------------------------------------------------------

namespace AntAlbelliTechnical.Tests
{
    using AntAlbelliTechnical.Controllers;
    using AntAlbelliTechnical.Repositories.Interfaces;
    using Moq;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Web.Http;

    /// <summary>
    /// The test base.
    /// </summary>
    [TestFixture]
    public abstract class TestBase
    {
        /// <summary>
        /// Gets or sets the Customer Repository Mock.
        /// </summary>
        internal Mock<ICustomerRepository> CustomerRepositoryMock { get; set; }

        /// <summary>
        /// Gets or sets the Order Repository Mock.
        /// </summary>
        internal Mock<IOrderRepository> OrderRepositoryMock { get; set; }

        /// <summary>
        ///Initializes the Customer controller.
        /// </summary>
        /// <returns><see cref="CustomerController"/></returns>
        internal CustomerController InitCustomerController()
        {
            var controller = new CustomerController(
                this.CustomerRepositoryMock.Object);

            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());

            return controller;
        }

        /// <summary>
        ///Initializes the Order controller.
        /// </summary>
        /// <returns><see cref="OrderController"/></returns>
        internal OrderController InitOrderController()
        {
            var controller = new OrderController(
                this.OrderRepositoryMock.Object,
                this.CustomerRepositoryMock.Object);

            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());

            return controller;
        }

        internal T GetContentFromResponse<T>(HttpResponseMessage response) where T : class
        {
            var objectcontent = response.Content as ObjectContent;

            if (objectcontent == null)
            {
                return default(T);
            }

            return objectcontent.Value as T;
        }
    }
}
