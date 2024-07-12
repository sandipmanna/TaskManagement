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
        Task<List<AssignedTask>> GetAllAsync();
        Task<AssignedTask> GetByIdAsync(int id);
        Task AddAsync(AssignedTask task);
        Task UpdateAsync(int id, AssignedTask task);
        Task DeleteAsync(int id);
        Task<List<AssignedTask>> SearchAsync(string taskName, List<string> tags, DateTime? startDate, DateTime? endDate, List<string> statuses);
    }
}
