using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;
using DataAccess.Components.Interface;
using DataAccess.Models;

namespace TaskManagement.Api.Controllers
{
    public class TaskController : ApiController
    {
        private readonly ITaskService _taskService;
        public TaskController()
        {
            
        }
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetTask(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateTask(AssignedTask task)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            await _taskService.AddTaskAsync(task);
            return Ok(task);
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateTask(int id, [FromBody] AssignedTask task)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            await _taskService.UpdateTaskAsync(id, task);
            return Ok(task);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return Ok();
        }

        [HttpPost]
        [Route("api/Task/search")]
        public async Task<IHttpActionResult> SearchTasks(TaskSearchCriteria criteria)
        {
            var tasks = await _taskService.SearchTasksAsync(criteria.TaskName, criteria.Tags, criteria.StartDate, criteria.EndDate, criteria.Statuses);
            return Ok(tasks);
        }

        [HttpGet]
        [Route("api/Task/{id}/Activity")]
        public async Task<IHttpActionResult> GetActivity(int id)
        {
            var tasks = await _taskService.GetActivityByIdAsync(id);
            return Ok(tasks);
        }

        [HttpPost]
        [Route("api/Task/{id}/AddActivity")]
        public async Task<IHttpActionResult> AddActivity(Activity activity)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            await _taskService.AddActivityAsync(activity);
            return Ok(activity);
        }
    }
}
