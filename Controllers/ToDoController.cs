using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ToDo_API.Models;
using ToDo_API.Repository;

namespace ToDo_API.Controllers
{
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ILogger<ToDoController> _logger;
        private readonly IConfiguration _configuration;

        public ToDoController(IConfiguration configuration, ILogger<ToDoController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        [Route("/api/todo")]
        public IActionResult GetToDos()
        {
            ToDoRepository toDoRepository = new ToDoRepository(_configuration);
            return Ok(toDoRepository.GetToDos());
        }

        [HttpGet]
        [Route("/api/todo/{id}")]
        public IActionResult GetToDo(int id)
        {
            ToDoRepository toDoRepository = new ToDoRepository(_configuration);
            return Ok(toDoRepository.GetToDo(id));
        }

        [HttpPost]
        [Route("/api/todo")]
        public IActionResult Insert(ToDoModel toDoModel)
        {
            ToDoRepository toDoRepository = new ToDoRepository(_configuration);
            return Ok(toDoRepository.Insert(toDoModel));
        }

        [HttpPost]
        [Route("/api/todo/{id}")]
        public IActionResult Edit(int id, ToDoModel toDoModel)
        {
            ToDoRepository toDoRepository = new ToDoRepository(_configuration);
            return Ok(toDoRepository.Update(id, toDoModel));
        }

        [HttpDelete]
        [Route("/api/todo/{id}")]
        public IActionResult Delete(int id)
        {
            ToDoRepository toDoRepository = new ToDoRepository(_configuration);
            return Ok(toDoRepository.Delete(id));
        }
    }
}