using Asos.CodeTest.DataAccessServices;
using Asos.CodeTest.FailoverServices;

namespace Asos.CodeTest.CustomerServices
{
    public class CustomerService : ICustomerService, ICustomerResponse
    {
        private readonly IArchivedDataService _archivedDataService;
        private readonly IFailoverService _failoverService;
        private readonly IFactoryCustomerDataAccess _factoryCustomerDataAccess;

        public CustomerService(IArchivedDataService archivedDataService, IFailoverService failoverService, IFactoryCustomerDataAccess factoryCustomerDataAccess)
        {
            _archivedDataService = archivedDataService;
            _failoverService = failoverService;
            _factoryCustomerDataAccess = factoryCustomerDataAccess;
        }

        public Customer GetCustomer(int customerId)
        {
            // Get Customer Response
            var customerResponse = GetCustomerResponse(customerId);
            
            // return customer
            return customerResponse.IsArchived ? _archivedDataService.GetArchivedCustomer(customerId) : customerResponse.Customer;
        }

        public CustomerResponse GetCustomerResponse(int customerId)
        {
            var isFailoverMode = _failoverService.IsFailoverMode();
            var customerData = _factoryCustomerDataAccess.GetCustomerDataAccessObject(isFailoverMode);
            
            return customerData.LoadCustomer(customerId);
        }
    }
}
