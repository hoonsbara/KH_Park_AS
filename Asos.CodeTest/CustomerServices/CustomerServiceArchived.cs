using Asos.CodeTest.DataAccessServices;
using Asos.CodeTest.FailoverServices;

namespace Asos.CodeTest.CustomerServices
{
    public class CustomerServiceArchived : ICustomerService
    {
        private readonly IArchivedDataService _archivedDataService;
        
        public CustomerServiceArchived(IArchivedDataService archivedDataService)
        {
            _archivedDataService = archivedDataService;
        }

        public Customer GetCustomer(int customerId)
        {
            return _archivedDataService.GetArchivedCustomer(customerId);
        }

    }
}
