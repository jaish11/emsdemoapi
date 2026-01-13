using emsdemoapi.Data.Entities;
using emsdemoapi.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace emsdemoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        //Using Generic 
        private readonly IGeneric<Country> _countries;
        public CountriesController(IGeneric<Country> countries)
        {
            _countries = countries;
        }

        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _countries.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _countries.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Add(Country country)
        {
            await _countries.AddAsync(country);
            await _countries.SaveAsync();
            return Ok("Added!");
        }
        [HttpPut]
        public async Task<IActionResult> Update(Country country)
        {
            await _countries.UpdateAsync(country);
            await _countries.SaveAsync();
            return Ok("Updated!");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _countries.DeleteAsync(id);
            await _countries.SaveAsync();
            return Ok("Deleted!");
        }

        //private readonly ICountry _country;
        //public CountriesController(ICountry country)
        //{
        //    _country = country;            
        //}

        //[HttpGet]
        //public IActionResult GetAllCountry()
        //{
        //    return Ok(_country.GetAllCountry());
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetCountryById(int id)
        //{
        //    return Ok(_country.GetCountryById(id));
        //}

        //[HttpPost]
        //public IActionResult AddCountry(Country country)
        //{
        //    _country.AddCountry(country);
        //    return Ok("Country Added Successfully!");
        //}

        //[HttpPut]
        //public IActionResult UpdateCountry(Country country)
        //{
        //    _country.UpdateCountry(country);
        //    return Ok("Country Updated Successfully!");
        //}
        
        //[HttpDelete("{id}")]
        //public IActionResult DeleteCountry(int id)
        //{
        //    _country.DeleteCountry(id);
        //    return Ok("Country Delete Successfully!");
        //}
    }
}
