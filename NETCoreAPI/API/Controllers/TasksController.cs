using System.Globalization;
using API.DTOs;
using API.Models;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private static readonly List<TaskModel> _tasks = LoadFromCsv();

        private static List<TaskModel> LoadFromCsv()
        {
            try
            {
                using var reader = new StreamReader("./Data/MOCK_DATA.csv");
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                return csv.GetRecords<TaskModel>().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data from CSV: {ex.Message}");

                return [];
            }
        }

        [HttpGet]
        public IActionResult GetAllTasks() => Ok(_tasks);

        [HttpPost]
        public IActionResult CreateNewTask([FromBody] TaskDTO model)
        {
            if (model is not null)
            {
                var taskToReturn = new TaskModel()
                {
                    Id = Guid.NewGuid(),
                    Title = model.Title,
                    IsCompleted = model.IsCompleted
                };
                _tasks.Add(taskToReturn);
                return CreatedAtAction(nameof(GetTaskById), new { id = taskToReturn.Id }, taskToReturn);
            }
            return BadRequest("Task DTO is null");
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetTaskById(Guid id)
        {
            var task = _tasks.FirstOrDefault(x => x.Id == id);
            return task is null ? NotFound($"Can't found the task with id: {id}") : Ok(task);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteTask(Guid id)
        {
            var taskToDelete = _tasks.FirstOrDefault(x => x.Id == id);
            if (taskToDelete is null)
                return BadRequest("Task not found");
            _tasks.Remove(taskToDelete);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateTask(Guid id, [FromBody] TaskDTO model)
        {
            var taskToUpdate = _tasks.FirstOrDefault(x => x.Id == id);
            if (taskToUpdate is null)
                return BadRequest("Task not found");

            taskToUpdate.Title = model.Title;
            taskToUpdate.IsCompleted = model.IsCompleted;
            return NoContent();
        }

        [HttpPost("bulk")]
        public IActionResult BulkAddTasks([FromBody] List<TaskDTO> models)
        {
            foreach (var model in models)
            {
                var taskToReturn = new TaskModel()
                {
                    Id = Guid.NewGuid(),
                    Title = model.Title,
                    IsCompleted = model.IsCompleted
                };
                _tasks.Add(taskToReturn);
            }
            return CreatedAtAction(nameof(GetAllTasks), new { }, _tasks);
        }

        [HttpDelete("bulk")]
        public IActionResult BulkDeleteTasks([FromBody] List<Guid> ids)
        {
            foreach (var id in ids)
            {
                var taskToDelete = _tasks.FirstOrDefault(x => x.Id == id);
                if (taskToDelete is null)
                    return BadRequest("Task not found");
                _tasks.Remove(taskToDelete);
            }
            return NoContent();
        }
    }
}