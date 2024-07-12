using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public DateTime ActivityDate { get; set; }
        public string DoneBy { get; set; }
        public string ActivityDescription { get; set; }
        public AssignedTask Task { get; set; }
    }
}
