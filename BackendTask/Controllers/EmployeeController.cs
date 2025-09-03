using BackendTask.Data;
using BackendTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _repo;

        public EmployeeController(EmployeeRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_repo.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var emp = _repo.GetById(id);
            if (emp == null) return NotFound();
            return Ok(emp);
        }

        [HttpPost]
        public IActionResult Add(Employee emp)
        {
            _repo.Add(emp);
            return Ok();
        }
    }
}
