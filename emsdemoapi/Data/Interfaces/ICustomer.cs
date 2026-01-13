using emsdemoapi.Data.Entities;

namespace emsdemoapi.Data.Interfaces
{
    public interface ICustomer
    {
        List<Customer> GetaAllCustomer();
        Customer GetCustomerById(int id);
        public bool AddCustomer(Customer customer);
        public bool UpdateCustomer(Customer customer);
        public bool DeleteCustomer(int id);
    }
}
