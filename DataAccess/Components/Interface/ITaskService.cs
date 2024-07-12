using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess.Models;

namespace DataAccess.Components.Interface
{
    public interface ITaskService
    {
        Task<List<AssignedTask>> GetAllTasksAsync();
        Task<AssignedTask> GetTaskByIdAsync(int id);
        Task AddTaskAsync(AssignedTask task);
        Task UpdateTaskAsync(int id, AssignedTask task);
        Task DeleteTaskAsync(int id);
        Task<List<AssignedTask>> SearchTasksAsync(string taskName, List<string> tags, DateTime? startDate, DateTime? endDate, List<string> statuses);
    }
}
