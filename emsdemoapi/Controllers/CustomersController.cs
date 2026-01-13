using emsdemoapi.Data;
using emsdemoapi.Data.DTO;
using emsdemoapi.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace emsdemoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CustomersController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            return Ok(_context.Customers.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            return Ok(_context.Customers.Find(id));
        }

        [HttpPost]
        //[HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromForm] CustomerDTO custDto)
        {
            string fileName = null;

            if (custDto.Image != null)
            {
                fileName = $"{Guid.NewGuid()}{Path.GetExtension(custDto.Image.FileName)}";
                var path = Path.Combine(_env.WebRootPath, "api/Uploads", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                using var stream = new FileStream(path, FileMode.Create);
                await custDto.Image.CopyToAsync(stream);
            }

            var customer = new Customer
            {
                Name = custDto.Name,
                Email = custDto.Email,
                Mobile = custDto.Mobile,
                Image = fileName
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }

        [HttpPut]
        //[HttpPut("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromForm] CustomerDTO custDto)
        {
            var customer = await _context.Customers.FindAsync(custDto.Id);
            customer.Name = custDto.Name;
            customer.Email = custDto.Email;
            customer.Mobile = custDto.Mobile;

            if (custDto.Image != null)
            {
                if (!string.IsNullOrEmpty(customer.Image))
                {
                    string oldPath = Path.Combine(_env.WebRootPath, "api/Uploads", customer.Image);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(custDto.Image.FileName)}";
                var path = Path.Combine(_env.WebRootPath, "api/Uploads", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                using var stream = new FileStream(path, FileMode.Create);
                await custDto.Image.CopyToAsync(stream);

                customer.Image = fileName;
            }

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (!string.IsNullOrEmpty(customer.Image))
            {
                string filePath = Path.Combine(_env.WebRootPath, "api/Uploads", customer.Image);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return Ok("Deleted successfully");
        }
    }
    //[Route("api/[controller]")]
    //[ApiController]
    //public class CustomersController : ControllerBase
    //{
    //    private readonly ICustomer _customer;

    //    public CustomersController(ICustomer customer)
    //    {
    //        _customer = customer;
    //    }
    //    [HttpGet]
    //    public IActionResult GetAllCustomers()
    //    {
    //        return Ok(_customer.GetaAllCustomer());
    //    }
    //    [HttpGet("{id}")]
    //    public IActionResult GetCustomerById(int id)
    //    {
    //        return Ok(_customer.GetCustomerById(id));
    //    }
    //    [HttpPost]
    //    public IActionResult AddCustomer(Customer customer)
    //    {
    //       _customer.AddCustomer(customer);
    //        return Ok("Customer Addded Successfully!");
    //    }
    //    [HttpPut]
    //    public IActionResult UpdateCustomer(Customer customer) { 
    //       _customer.UpdateCustomer(customer);
    //        return Ok("Customer Updated Successfully!");
    //    }
    //    [HttpDelete("{id}")]
    //    public IActionResult DeleteCustomer(int id) { 
    //       _customer.DeleteCustomer(id);
    //        return Ok("Customer Deleted Successfully!");
    //    }
    //}
}
