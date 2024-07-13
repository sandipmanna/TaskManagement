using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess.Models;

namespace DataAccess.Components.Interface
{
    public interface ITaskRepository
    {
        Task<List<AssignedTask>> GetAllTaskAsync();
        Task<AssignedTask> GetTaskByIdAsync(int id);
        Task AddTaskAsync(AssignedTask task);
        Task UpdateTaskAsync(int id, AssignedTask task);
        Task DeleteTaskAsync(int id);
        Task<List<AssignedTask>> SearchAsync(string taskName, List<string> tags, DateTime? startDate, DateTime? endDate, List<string> statuses);

        Task AddActivityAsync(Activity activity);
        Task<Activity> GetActivityByIdAsync(int id);
        Task UpdateActivityAsync(Activity aactivity);
        Task DeleteActivityAsync(int Id);
    }
}
