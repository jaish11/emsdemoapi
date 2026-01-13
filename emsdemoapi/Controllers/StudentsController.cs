using emsdemoapi.Data;
using emsdemoapi.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace emsdemoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        //using Generic Interface
        private readonly IGeneric<Student> _student;
        public StudentsController(IGeneric<Student> student)
        {
            _student = student;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            return Ok( await _student.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _student.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Add(Student student)
        {
            await _student.AddAsync(student);
            await _student.SaveAsync();
            return Ok("Student Successfully Added");
        }
        [HttpPut]
        public async Task<IActionResult> Update(Student student)
        {
            await _student.UpdateAsync(student);
            await _student.SaveAsync();
            return Ok("Student Successfully Updated");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _student.DeleteAsync(id);
            await _student.SaveAsync();
            return Ok("Student Successfully Deleted");
        }

        ////* This is used when we are not using GENERIC Interface
        //private readonly AppDbContext _context;
        //public StudentsController(AppDbContext context)
        //{
        //    _context = context;
        //}
        //[HttpGet]
        //public IActionResult GetAllStudents()
        //{
        //    return Ok(_context.Students.ToList());
        //}
        //[HttpGet("{id}")]
        //public IActionResult GetStudentById(int id) {
        //    return Ok(_context.Students.Find(id));
        //}
        //[HttpPost]
        //public IActionResult AddStudent(Student student) { 
        //    _context.Students.Add(student);
        //    _context.SaveChanges();
        //    return Ok("Student Added Successfully!");
        //}
        //[HttpPut]
        //public IActionResult UpdateStudent(Student student)
        //{
        //    _context.Students.Update(student);
        //    _context.SaveChanges();
        //    return Ok("Student updated Successfully!");
        //}
        //[HttpDelete("{id}")]
        //public IActionResult DeleteStudent(int id) {
        //    Student student = _context.Students.Find(id);
        //    _context.Students.Remove(student);
        //    _context.SaveChanges();
        //    return Ok("Student Deleted Successfully!");
        //}
    }
}
