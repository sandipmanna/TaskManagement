using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagement.ViewModels
{
    public class SearchViewModel
    {
        public TaskSearchCriteria Criteria { get; set; }
        public List<AssignedTask> Tasks { get; set; }
    }
}