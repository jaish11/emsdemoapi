using emsdemoapi.Data.Entities;
using emsdemoapi.Data.Excel;
using emsdemoapi.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace emsdemoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictsController : ControllerBase
    {
        private readonly IDistrict _district;
        private readonly DistrictExcelService _excelService;
        public DistrictsController(IDistrict district, DistrictExcelService excelService) { 
           _district = district;
            _excelService = excelService;
        }

        [HttpGet]
        public IActionResult GetAllDistrict()
        {
            return Ok(_district.GetAllDistricts());
        }
        [HttpGet("{id}")]
        public IActionResult GetDistrictById(int id) {
            
            return Ok(_district.GetDistrictById(id));
        }
        [HttpPost]
        public IActionResult AddDistrict(District district) {
            _district.AddDistrcit(district);
           return Ok("District Added Successfully!");

        }

        [HttpPut]
        public IActionResult UpdateDistrict(District district) { 
            _district.UpdateDistrict(district);
            return Ok("District Updated Successfully!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDistrict(int id) {
            _district.DeleteDistrict(id);
            return Ok("District Delete Succesfully!");
        }

        [HttpGet("export")]
        public IActionResult ExportDistrictsToExcel()
        {
            var excelBytes = _excelService.ExportDistrictsToExcel();
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Districts.xlsx");
        }
        [HttpPost("import")]
        public IActionResult ImportDistrictsFromExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
            using var stream = file.OpenReadStream();
            _excelService.ImportDistrictsFromExcel(stream);
            return Ok("Districts imported successfully from Excel.");
        }
    }
}
