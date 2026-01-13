using Dapper;
using emsdemoapi.Data.Entities;
using emsdemoapi.Data.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace emsdemoapi.Data.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly string _connectionString;
        public EmployeeService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlCon");
        }
        public bool AddEmployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("AddEmployee", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Position", employee.Position);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
        }

        public bool DeleteEmployee(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("DeleteEmployeeById", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
        }

        public List<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("GetALlEmployees", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Position = reader["Position"].ToString(),
                            Salary = Convert.ToDecimal(reader["Salary"])
                        });
                    }
                }
            }
            return employees;
        }

        public Employee GetEmployeeById(int id)
        {
            Employee employee = null;
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("GetEmployeeById", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employee = new Employee
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Position = reader["Position"].ToString(),
                            Salary = Convert.ToDecimal(reader["Salary"])
                        };
                    }
                }
            }
            return employee;
        }

        public bool UpdateEmployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("UpdateEmployee", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", employee.Id);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Position", employee.Position);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);

                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
        }
    }

    /*  public class EmployeeService : IEmployee
      {
          private readonly IConfiguration _configuration;
          private readonly string _connectionString;

          public EmployeeService(IConfiguration configuration)
          {
              _configuration = configuration;
              _connectionString = _configuration.GetConnectionString("SqlCon");
          }
          public bool AddEmployee(Employee employee)
          {
              using(var connection = new SqlConnection(_connectionString))
              {
                  var parameter = new DynamicParameters();
                  parameter.Add("@Name", employee.Name);
                  parameter.Add("@Position", employee.Position);
                  parameter.Add("@Salary", employee.Salary);

                  connection.Execute("AddEmployee", parameter, commandType: CommandType.StoredProcedure);
                  return true;
              }
          }

          public bool DeleteEmployee(int id)
          {
              using (var connection = new SqlConnection(_connectionString))
              {

                  var parameter = new DynamicParameters();
                  parameter.Add("@Id", id);
                  connection.Execute("DeleteEmployeeById", parameter, commandType: CommandType.StoredProcedure);
                  return true;
              }
          }

          public List<Employee> GetAllEmployees()
          {
              using (var connection = new SqlConnection(_connectionString))
              {
                  var employees = connection.Query<Employee>("GetALlEmployees", commandType:CommandType.StoredProcedure).ToList();
                  return employees;
              }
          }

          public Employee GetEmployeeById(int id)
          {
              using (var connection = new SqlConnection(_connectionString))
              {
                  var parameter = new DynamicParameters();
                  parameter.Add("@Id", id);
                  var employee = connection.QueryFirstOrDefault<Employee>("GetEmployeeById", parameter, commandType: CommandType.StoredProcedure);
                  return employee;

              }
          }

          public bool UpdateEmployee(Employee employee)
          {
              using (var connection = new SqlConnection(_connectionString))
              {
                  var parameter = new DynamicParameters();
                  parameter.Add("@Id", employee.Id);
                  parameter.Add("@Name", employee.Name);
                  parameter.Add("@Position", employee.Position);
                  parameter.Add("@Salary", employee.Salary);

                  connection.Execute("UpdateEmployee", parameter, commandType: CommandType.StoredProcedure);
                  return true;
              }
          }
      }
  */
}
