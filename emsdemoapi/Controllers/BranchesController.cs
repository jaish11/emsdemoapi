using emsdemoapi.Data.Entities;
using emsdemoapi.Data.Excel;
using emsdemoapi.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace emsdemoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
       

        private readonly IBranch _branch;
        public BranchesController(IBranch branch)
        {
            _branch = branch;
        }

        [HttpGet]
        public IActionResult GetAllBranch()
        {
            return Ok(_branch.GetAllBranches());
        }

        [HttpGet("{id}")]
        public IActionResult GetBranchById(int id)
        {
            return Ok(_branch.GetBranchById(id));
        }

        [HttpPost]
        public IActionResult AddBranch(Branch branch)
        {
            return Ok(_branch.AddBranch(branch));
        }

        [HttpPut]
        public IActionResult UpdateBranch(Branch branch)
        {
            return Ok(_branch.UpdateBranch(branch));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBranchById(int id)
        {
            return Ok(_branch.DeleteBranch(id));
        }
    }
}
