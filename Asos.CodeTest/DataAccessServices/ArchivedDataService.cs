using Asos.CodeTest.CustomerServices;

namespace Asos.CodeTest.DataAccessServices
{
    public class ArchivedDataService : IArchivedDataService
    {
        public Customer GetArchivedCustomer(int customerId)
        {
            // retrieve customer from archive data service
            return new Customer();
        }
    }
}