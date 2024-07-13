using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TaskManagement.Services;
using TaskManagement.ViewModels;

namespace TaskManagement.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskService _taskService;

        public TaskController()
        {
            _taskService = new TaskService();
        }

        public async Task<ActionResult> Index()
        {
            var tasks = await _taskService.GetTasksAsync();
            return View(tasks);
        }

        public async Task<ActionResult> Details(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            return View(task);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(AssignedTask task)
        {
            if (ModelState.IsValid)
            {
                if (task.TagValues != null)
                    task.Tags = task.TagValues.Split(',').Select(tag => tag.Trim()).ToList();
                await _taskService.AddTaskAsync(task);
                return RedirectToAction("Index");
            }

            return View(task);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            return View(task);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(AssignedTask task)
        {
            if (ModelState.IsValid)
            {
                await _taskService.UpdateTaskAsync(task);
                return RedirectToAction("Index");
            }

            return View(task);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return RedirectToAction("Index");
        }

        public ActionResult Search()
        {
            return View(new SearchViewModel { Criteria = new TaskSearchCriteria(), Tasks = new List<AssignedTask>() });
        }

        [HttpPost]
        public async Task<ActionResult> Search(SearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Criteria.TagValues != null)
                    model.Criteria.Tags = model.Criteria.TagValues.Split(',').Select(tag => tag.Trim()).ToList();
                if (model.Criteria.Status != null)
                    model.Criteria.Statuses = model.Criteria.Status.Split(',').Select(Status => Status.Trim()).ToList();
                model.Tasks = await _taskService.SearchTasksAsync(model.Criteria);
            }

            return View(model);
        }

        public ActionResult AddActivity()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddActivity(Activity activity)
        {
            if (ModelState.IsValid)
            {
                await _taskService.AddActivityAsync(activity);
                return RedirectToAction($"Details/{activity.TaskId}");
            }

            return View(activity);
        }

        public async Task<ActionResult> EditActivity(int id)
        {
            var activity = await _taskService.GetActivityByIdAsync(id);
            return View(activity);
        }

        [HttpPost]
        public async Task<ActionResult> EditActivity(int taskId, Activity activity)
        {
            if (ModelState.IsValid)
            {
                await _taskService.UpdateActivityAsync(activity);
                return RedirectToAction($"Details/{taskId}");
            }

            return View(activity);
        }

        public async Task<ActionResult> DeleteActivity(int id)
        {
            var activity = await _taskService.GetActivityByIdAsync(id);
            return View(activity);
        }

        [HttpPost, ActionName("DeleteActivity")]
        public async Task<ActionResult> ActivityDeleteConfirmed(int taskId, int id)
        {
            await _taskService.DeleteActivityAsync(id);
            return RedirectToAction($"Details/{taskId}");
        }
    }
}