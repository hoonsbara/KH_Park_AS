using Asos.CodeTest.CustomerServices;
using Asos.CodeTest.DataAccessServices;
using Moq;
using NUnit.Framework;

namespace Asos.CodeTest.Test
{
    [TestFixture]
    public class CustomerServiceArchivedTest
    {
        [Test]
        public void GetCustomer_IsCustomerArchived_ReturnArchivedCustomer()
        {
            // Arrange
            const int customerId = 1;
            var archivedDataService = new Mock<IArchivedDataService>();
            var customer = new Customer { Name = "Customer", Id = customerId };
            archivedDataService.Setup(x => x.GetArchivedCustomer(customerId))
                               .Returns(customer);
            var customerService = new CustomerServiceArchived(archivedDataService.Object);

            // Act
            var returned = customerService.GetCustomer(customerId);

            // Assert
            Assert.That(returned, Is.EqualTo(customer));
        }
    }
}
