using emsdemoapi.Data.Entities;
using emsdemoapi.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace emsdemoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly IState _state;
        public StatesController(IState state)
        {
            _state=state;
        }
        [HttpGet]
        public IActionResult GetAllSate() {
            return Ok(_state.GetAllState());
        }

        [HttpGet("{id}")]
        public IActionResult GetSateById(int id) {
           
            return Ok(_state.GetStateById(id));    
        }
        [HttpPost]
        public IActionResult AddState(State state) {    
            _state.AddState(state);
            return Ok("State Added Succssesfully!");
        }

        [HttpPut]
        public IActionResult UpdateState(State state) {
           _state.UpdateState(state);
            return Ok("State Updated Successfully!");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteState(int id)
        {
            _state.DeleteState(id);
            return Ok("State Deleted Successfully");
        }
    }
}
