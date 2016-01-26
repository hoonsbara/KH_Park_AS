namespace Asos.CodeTest.DataAccessServices
{
    public interface IFactoryCustomerDataAccess
    {
        ICustomerDataAccess GetCustomerDataAccessObject(bool isFailoverMode);
    }
}