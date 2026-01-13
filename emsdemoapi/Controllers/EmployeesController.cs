using emsdemoapi.Data;
using emsdemoapi.Data.Entities;
using emsdemoapi.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace emsdemoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase 
    {
        private readonly IEmployee _employee;
        public EmployeesController(IEmployee employee)
        {
            _employee = employee;
        }

        [HttpGet]
        public IActionResult GetAllEmployee() {
            return Ok(_employee.GetAllEmployees());
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id) {
            return Ok(_employee.GetEmployeeById(id));
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee employee) {
            _employee.AddEmployee(employee);
            return Ok("Employee Added Successfully!");
        }

        [HttpPut]
        public IActionResult UpdateEmployee(Employee employee) {
            _employee.UpdateEmployee(employee);
            return Ok("Update Employee Successfully!");
        }

        [HttpDelete]
        public IActionResult DeleteEmployee(int id) { 
           _employee.DeleteEmployee(id);
            return Ok("Delete Employee Successfully!");
        }
    }
}
