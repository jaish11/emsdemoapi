using emsdemoapi.Data.Entities;

namespace emsdemoapi.Data.Interfaces
{
    public interface IEmployee
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployeeById(int id);
        public bool AddEmployee(Employee employee);
        public bool UpdateEmployee(Employee employee);

        public bool DeleteEmployee(int id);

    }
}
