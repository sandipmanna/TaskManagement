using DataAccess.Components.Interface;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Components.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<AssignedTask>> GetAllTasksAsync()
        {
            return await _taskRepository.GetAllAsync();
        }

        public async Task<AssignedTask> GetTaskByIdAsync(int id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }

        public async Task AddTaskAsync(AssignedTask task)
        {
            await _taskRepository.AddAsync(task);
        }
        public async Task UpdateTaskAsync(int id,  AssignedTask task)
        {
            await _taskRepository.UpdateAsync(id, task);
        }

        public async Task DeleteTaskAsync(int id)
        {
            await _taskRepository.DeleteAsync(id);
        }

        public async Task<List<AssignedTask>> SearchTasksAsync(string taskName, List<string> tags, DateTime? startDate, DateTime? endDate, List<string> statuses)
        {
            return await _taskRepository.SearchAsync(taskName, tags, startDate, endDate, statuses);
        }
    }
}
