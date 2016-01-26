namespace Asos.CodeTest.DataAccessServices
{
    public class FactoryCustomerDataAccess : IFactoryCustomerDataAccess
    {
        public ICustomerDataAccess GetCustomerDataAccessObject(bool isFailoverMode)
        {
            if (isFailoverMode)
                return new FailoverCustomerDataAccess();

            return new CustomerDataAccess();
        }
    }
}
