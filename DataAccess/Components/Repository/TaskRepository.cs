using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

using DataAccess.Components.Interface;
using DataAccess.DataContext;
using DataAccess.Models;

namespace DataAccess.Components.Repository
{
    public class TaskRepository : ITaskRepository
    {
        EFDataContext _dataContext;
        public TaskRepository()
        {
            _dataContext = new EFDataContext();
        }

        private void GetTags(AssignedTask task)
        {
            task.Tags = _dataContext
                    .Tags
                    .Where(t => t.Task.Id == task.Id)
                    .Select(t => t.Name)
                    .ToList();
        }

        public async Task<List<AssignedTask>> GetAllTaskAsync()
        {
            var tasks = _dataContext.Tasks.ToList();
            foreach (var task in tasks)
            {
                GetTags(task);
            }

            return await Task.FromResult(tasks);
        }

        public async Task<AssignedTask> GetTaskByIdAsync(int id)
        {
            try
            {
                var task = _dataContext
                    .Tasks
                    .Where(t => t.Id == id)
                    .Include(t => t.Activities)
                    .FirstOrDefault();
                GetTags(task);

                return await Task.FromResult(task);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task AddTaskAsync(AssignedTask task)
        {
            _dataContext.Tasks.Add(task);

            await _dataContext.SaveChangesAsync();

            foreach (var item in task.Tags)
            {
                _dataContext.Tags.Add(new Tag { Name = item, Task = task });
            }
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(int id, AssignedTask task)
        {
            var entity = _dataContext.Tasks.FirstOrDefault(n => n.Id == id);

            if (entity != null)
            {
                entity.TaskName = task.TaskName;
                entity.Tags = task.Tags;
                entity.DueDate = task.DueDate;
                entity.Color = task.Color;
                entity.AssignedTo = task.AssignedTo;
                entity.Status = task.Status;
                entity.Activities = task.Activities;
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = _dataContext.Tasks.SingleOrDefault(t => t.Id == id);
            var tags = _dataContext.Tags.Where(t => t.Task.Id == id);
            var activities = _dataContext.Activities.Where(a => a.Task.Id == id);

            _dataContext.Tags.RemoveRange(tags);
            _dataContext.Activities.RemoveRange(activities);
            _dataContext.Tasks.Remove(task);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<AssignedTask>> SearchAsync(string taskName, List<string> tags, DateTime? startDate, DateTime? endDate, List<string> statuses)
        {
            var query = _dataContext.Tasks.AsQueryable();
            List<int> taskIds = new List<int>(); 
            if (tags != null && tags.Any())
                taskIds = _dataContext
                    .Tags
                    .Where(t => tags.Contains(t.Name))
                    .Select(tag => tag.Task.Id)
                    .ToList();

            if (!string.IsNullOrEmpty(taskName))
            {
                query = query.Where(t => t.TaskName.Equals(taskName, StringComparison.OrdinalIgnoreCase));
            }

            if (taskIds.Any())
            {
                query = query.Where(t => taskIds.Contains(t.Id));
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(t => t.DueDate >= startDate.Value && t.DueDate <= endDate.Value);
            }

            if (statuses != null && statuses.Any())
            {
                query = query.Where(t => statuses.Contains(t.Status));
            }

            var tasks = query.Include(t => t.Activities).ToList();

            if (tags?.Count() > 0 && taskIds.Count() == 0)
                tasks = new List<AssignedTask>();

            foreach (var task in tasks)
            {
                GetTags(task);
            }

            return await Task.FromResult(tasks);
        }

        public async Task AddActivityAsync(Activity activity)
        {
            _dataContext.Activities.Add(activity);

            await _dataContext.SaveChangesAsync();
        }

        public async Task<Activity> GetActivityByIdAsync(int id)
        {
            var activity = _dataContext
                .Activities
                .Where(a => a.Id == id)
                .Include(a => a.Task)
                .FirstOrDefault();

            return await Task.FromResult(activity);
        }

        public async Task UpdateActivityAsync(Activity activity)
        {
            var entity = _dataContext
                .Activities
                .Include(a => a.Task)
                .FirstOrDefault(a => a.Id == activity.Id);

            if (entity != null)
            {
                entity.ActivityDate = activity.ActivityDate;
                entity.DoneBy = activity.DoneBy;
                entity.ActivityDescription = activity.ActivityDescription;
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task DeleteActivityAsync(int id)
        {
            var activity = await _dataContext.Activities.FindAsync(id);

            _dataContext.Activities.Remove(activity);
            await _dataContext.SaveChangesAsync();
        }
    }
}
