using emsdemoapi.Data;
using Microsoft.AspNetCore.Mvc;

namespace emsdemoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CountriesController(AppDbContext context)
        {
            _context = context;            
        }

        [HttpGet]
        public IActionResult GetAllCountry()
        {
            return Ok(_context.Countries.ToList());
        }

        [HttpGet]
        public IActionResult GetCountryById(int id)
        {
            Country country = _context.Countries.Find(id);
            return Ok(country);
        }

        [HttpPost]
        public IActionResult AddCountry(Country country)
        {
            _context.Countries.Add(country);
            _context.SaveChanges();
            return Ok("Country Added Successfully!");
        }

        [HttpPut]
        public IActionResult UpdateCountry(Country country)
        {
            _context.Countries.Update(country);
            _context.SaveChanges();
            return Ok("Country Added Successfully!");
        }
        
        [HttpDelete]
        public IActionResult DeleteCountry(int id)
        {
            Country country = _context.Countries.Find(id);

            _context.Countries.Remove(country);
            _context.SaveChanges();
            return Ok("Country Added Successfully!");
        }
    }
}
