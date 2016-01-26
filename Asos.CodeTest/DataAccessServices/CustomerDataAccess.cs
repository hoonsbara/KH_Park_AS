
using Asos.CodeTest.CustomerServices;

namespace Asos.CodeTest.DataAccessServices
{
    public class CustomerDataAccess : ICustomerDataAccess
    {
        public CustomerResponse LoadCustomer(int customerId)
        {
            // rettrieve customer from 3rd party webservice
            return new CustomerResponse();
        }
    }
}