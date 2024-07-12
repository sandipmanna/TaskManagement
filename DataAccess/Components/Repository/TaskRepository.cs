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

        public async Task<List<AssignedTask>> GetAllAsync()
        {
            var tasks = _dataContext.Tasks.ToList();
            foreach (var task in tasks)
            {
                GetTags(task);
            }

            return await Task.FromResult(tasks);
        }

        public async Task<AssignedTask> GetByIdAsync(int id)
        {
            var task = _dataContext
                .Tasks
                .Where(t => t.Id == id)
                .FirstOrDefault();
            GetTags(task);

            return await Task.FromResult(task);
        }

        public async Task AddAsync(AssignedTask task)
        {
            _dataContext.Tasks.Add(task);

            await _dataContext.SaveChangesAsync();

            foreach (var item in task.Tags)
            {
                _dataContext.Tags.Add(new Tag { Name = item, Task = task });
            }
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, AssignedTask task)
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

        public async Task DeleteAsync(int id)
        {
            var task = await _dataContext.Tasks.FindAsync(id);
            var tags = _dataContext.Tags.Where(t => t.Task.Id == id);

            _dataContext.Tags.RemoveRange(tags);
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
            foreach (var task in tasks)
            {
                GetTags(task);
            }

            return await Task.FromResult(tasks);
        }

        public async Task<Activity> GetActivityByIdAsync(int id)
        {
            var activity = _dataContext
                .Activities
                .Where(a => a.Task.Id == id)
                .Include(t => t.Task)
                .FirstOrDefault();

            return await Task.FromResult(activity);
        }

        public async Task AddActivityAsync(Activity activity)
        {
            _dataContext.Activities.Add(activity);

            await _dataContext.SaveChangesAsync();
        }
    }
}
