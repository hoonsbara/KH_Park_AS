using Asos.CodeTest.CustomerServices;

namespace Asos.CodeTest.DataAccessServices
{
    public class FailoverCustomerDataAccess : ICustomerDataAccess
    {
        // To keep the signature
        public CustomerResponse LoadCustomer(int customerId)
        {
            return GetCustomerById(customerId);
        }

        public static CustomerResponse GetCustomerById(int id)
        {
            // retrieve customer from database
            return new CustomerResponse();
        }
    }
}