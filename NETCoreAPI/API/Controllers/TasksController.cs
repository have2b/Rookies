using System.Globalization;
using System.Net;
using API.DTOs;
using API.Models;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private static readonly List<TaskModel> _tasks = LoadFromCsv();
        private static ILogger<TasksController> _logger;

        public TasksController(ILogger<TasksController> logger)
        {
            _logger = logger;
        }

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
                _logger.LogError(ex, "Error loading data from CSV");
                return [];
            }
        }

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            try
            {
                return Ok(_tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all tasks");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult CreateNewTask([FromBody] TaskDTO model)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new task");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetTaskById(Guid id)
        {
            try
            {
                var task = _tasks.FirstOrDefault(x => x.Id == id);
                return task is null ? NotFound($"Can't found the task with id: {id}") : Ok(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting task by id");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteTask(Guid id)
        {
            try
            {
                var taskToDelete = _tasks.FirstOrDefault(x => x.Id == id);
                if (taskToDelete is null)
                {
                    return BadRequest("Task not found");
                }
                _tasks.Remove(taskToDelete);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting task");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateTask(Guid id, [FromBody] TaskDTO model)
        {
            try
            {

                var taskToUpdate = _tasks.FirstOrDefault(x => x.Id == id);
                if (taskToUpdate is null)
                {
                    return BadRequest("Task not found");
                }
                taskToUpdate.Title = model.Title;
                taskToUpdate.IsCompleted = model.IsCompleted;
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating task");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("bulk")]
        public IActionResult BulkAddTasks([FromBody] List<TaskDTO> models)
        {
            try
            {
                _tasks.AddRange(models.Select(m => new TaskModel()
                {
                    Id = Guid.NewGuid(),
                    Title = m.Title,
                    IsCompleted = m.IsCompleted
                }));
                return CreatedAtAction(nameof(GetAllTasks), new { });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error bulk adding tasks");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("bulk")]
        public IActionResult BulkDeleteTasks([FromBody] List<Guid> ids)
        {
            try
            {
                var deletedCount = _tasks.RemoveAll(x => ids.Contains(x.Id));

                if (deletedCount == ids.Count)
                {
                    return NoContent();
                }

                return BadRequest("Some tasks not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error bulk deleting tasks");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

    }
}