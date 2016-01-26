using Asos.CodeTest.CustomerServices;

namespace Asos.CodeTest.DataAccessServices
{
    public interface IArchivedDataService
    {
        Customer GetArchivedCustomer(int customerId);
    }
}