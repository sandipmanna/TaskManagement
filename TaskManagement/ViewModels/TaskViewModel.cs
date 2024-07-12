using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagement.ViewModels
{
    public class TaskViewModel
    {
        public AssignedTask Task { get; set; }
        public List<Activity> Activities { get; set; }
    }
}