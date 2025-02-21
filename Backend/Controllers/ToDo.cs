using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Models;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ToDo _ToDo;
        public ToDoController(ToDo ToDo)
        {
            _ToDo = ToDo;
        }

        [HttpPost("add")]
        public IActionResult AddItem([FromBody] ToDoItem toDoItem)
        {
            _ToDo.AddItem(toDoItem);
            return Ok(toDoItem);
        }

        [HttpGet("get")]
        public ActionResult GetList()
        {
            var itemList = _ToDo.GetList();
            return Ok(itemList);
        }

        [HttpPut("toggle/{id}")]
        public IActionResult ToggleItem(int id)
        {
            _ToDo.ToggleItem(id);
            return Ok("ok");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteItem(int id)
        {
            _ToDo.DeleteItem(id);
            return Ok("ok");
        }
    }
}