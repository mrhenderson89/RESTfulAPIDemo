/// ----------------------------------------------------------------------
/// <summary>
/// Defines the test base initialiser.
/// </summary>
/// ----------------------------------------------------------------------

namespace AntAlbelliTechnical.Tests
{
    using AntAlbelliTechnical.Repositories.Interfaces;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// The test base initialiser.
    /// </summary>
    [TestFixture]
    public abstract class TestBaseInitialiser : TestBase
    {
        /// <summary>
        /// Set up this test instance.
        /// </summary>
        [SetUp]
        public void BeforeTest()
        {
            this.CustomerRepositoryMock = new Mock<ICustomerRepository>(MockBehavior.Strict);
            this.OrderRepositoryMock = new Mock<IOrderRepository>(MockBehavior.Strict);
        }

        /// <summary>
        /// Tear down this test instance.
        /// </summary>
        [TearDown]
        public void AfterTest()
        {
            this.CustomerRepositoryMock.VerifyAll();
            this.OrderRepositoryMock.VerifyAll();
        }
    }
}
