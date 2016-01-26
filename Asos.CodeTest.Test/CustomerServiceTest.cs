using Asos.CodeTest.CustomerServices;
using Asos.CodeTest.DataAccessServices;
using Asos.CodeTest.FailoverServices;
using Moq;
using NUnit.Core;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Asos.CodeTest.Test
{
    [TestFixture]
    public class CustomerServiceTest
    {
        private Mock<IArchivedDataService> _archivedDataService;
        private Mock<IFailoverService> _failoverService;
        private Mock<IFactoryCustomerDataAccess> _factoryCustomerDataAccess;
        private Mock<ICustomerDataAccess> _customerDataAccess;

        private CustomerService _customerService;
        private const int CustomerId = 1;

        [SetUp]
        public void Init()
        {
            // mock objects
            _archivedDataService = new Mock<IArchivedDataService>();
            _failoverService = new Mock<IFailoverService>();
            _factoryCustomerDataAccess = new Mock<IFactoryCustomerDataAccess>();
            _customerDataAccess = new Mock<ICustomerDataAccess>();
            
            _factoryCustomerDataAccess.Setup(x => x.GetCustomerDataAccessObject(true))
                .Returns(_customerDataAccess.Object);
            _factoryCustomerDataAccess.Setup(x => x.GetCustomerDataAccessObject(false))
                .Returns(_customerDataAccess.Object);

            // service
            _customerService = new CustomerService(_archivedDataService.Object, _failoverService.Object, _factoryCustomerDataAccess.Object);
        }


        [Test]
        public void GetCustomerResponse_OnFailoverMode_ReturnCustomerResponse()
        {
            // Arrange
            SetFailoverServiceMock(true);
            var customerResponse = GenerateCustomerReponse(CustomerId, "Customer on failover", true);
            _customerDataAccess.Setup(x => x.LoadCustomer(CustomerId)).Returns(customerResponse);

            // Act
            var returned = _customerService.GetCustomerResponse(CustomerId);

            // Assert
            Assert.That(returned, Is.EqualTo(customerResponse));
        }

        [Test]
        public void GetCustomerResponse_OnNormalServiceMode_ReturnCustomerResponse()
        {
            // Arrange
            SetFailoverServiceMock(false);
            var customerResponse = GenerateCustomerReponse(CustomerId, "Customer on normal Service", true);
            _customerDataAccess.Setup(x => x.LoadCustomer(CustomerId)).Returns(customerResponse);

            // Act
            var returned = _customerService.GetCustomerResponse(CustomerId);

            // Assert
            Assert.That(returned, Is.EqualTo(customerResponse));
        }

        [Test]
        public void GetCustomer_CustomerResponseIndicateIsArchivedTrue_ReturnCustomerByArchivedService()
        {
            // Arrange
            var customerResponse = GenerateCustomerReponse(CustomerId, "Customer on normal Service", true);
            var customerArchived = GenerateCustomer(CustomerId, "Customer from archived service");

            _customerDataAccess.Setup(x => x.LoadCustomer(CustomerId)).Returns(customerResponse);
            _archivedDataService.Setup(x => x.GetArchivedCustomer(CustomerId)).Returns(customerArchived);
            
            // Act
            var returned = _customerService.GetCustomer(CustomerId);

            // Assert
            Assert.That(returned, Is.EqualTo(customerArchived));
        }

        [Test]
        public void GetCustomer_CustomerResponseIndicateIsArchivedFalse_ReturnCustomer()
        {
            // Arrange
            var customerName = "Customer on normal service";
            var customerResponse = GenerateCustomerReponse(CustomerId, customerName, false);
            _customerDataAccess.Setup(x => x.LoadCustomer(CustomerId)).Returns(customerResponse);
            
            // Act
            var retuned = _customerService.GetCustomer(CustomerId);

            // Assert
            Assert.That(retuned.Id, Is.EqualTo(CustomerId));
            Assert.That(retuned.Name, Is.EqualTo(customerName));
        }

        private CustomerResponse GenerateCustomerReponse(int id, string name, bool isArchived)
        {
            return GenerateCustomerReponse(GenerateCustomer(id, name), isArchived );
        }

        private CustomerResponse GenerateCustomerReponse(Customer customer, bool isArchived)
        {
            return new CustomerResponse { Customer = customer, IsArchived = isArchived };
        }
            
        private Customer GenerateCustomer(int id, string name)
        {
            return new Customer {Id = id, Name = name};
        }

        private void SetFailoverServiceMock(bool isFailoverMode)
        {
            _failoverService.Setup(x => x.IsFailoverMode()).Returns(isFailoverMode);
        }
    }
}