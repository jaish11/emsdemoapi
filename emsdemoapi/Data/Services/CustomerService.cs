using Dapper;
using emsdemoapi.Data.Entities;
using emsdemoapi.Data.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace emsdemoapi.Data.Services
{
    public class CustomerService : ICustomer
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public CustomerService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlCon");
        }
        public bool AddCustomer(Customer customer)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Name", customer.Name);
                parameters.Add("@Email", customer.Email);
                parameters.Add("@Mobile", customer.Mobile);
                parameters.Add("@Image", customer.Image);

                connection.Execute("sp_AddCustomers", parameters, commandType: CommandType.StoredProcedure);

                return true;
            }
        }

        public bool DeleteCustomer(int id)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                connection.Execute("sp_DeleteCustomerByID", parameters, commandType: CommandType.StoredProcedure);
                return true;
            }
        }

        public List<Customer> GetaAllCustomer()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var customers = connection.Query<Customer>("sp_GetAllCustomers", commandType: CommandType.StoredProcedure).ToList();
                return customers;
            }
        }

        public Customer GetCustomerById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                var customer = connection.QueryFirstOrDefault<Customer>("sp_GetCustomersById", parameters,commandType: CommandType.StoredProcedure);
                return customer;
            }
        }

        public bool UpdateCustomer(Customer customer)
        {
           using(var connection = new SqlConnection(_connectionString))
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Id", customer.Id);
                parameter.Add("@Name", customer.Name);
                parameter.Add("@Email", customer.Email);
                parameter.Add("@Mobile", customer.Mobile);
                parameter.Add("@Image", customer.Image);

                connection.Execute("sp_UpdateCustomers", parameter, commandType: CommandType.StoredProcedure);
                return true;
            }
        }
    }
}
