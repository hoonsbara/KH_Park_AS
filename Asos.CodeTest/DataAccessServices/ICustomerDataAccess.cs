using Asos.CodeTest.CustomerServices;

namespace Asos.CodeTest.DataAccessServices
{
    public interface ICustomerDataAccess
    {
        CustomerResponse LoadCustomer(int customerId);
    }
}
