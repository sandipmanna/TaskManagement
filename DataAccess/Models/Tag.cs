using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TaskId { get; set; }
        public AssignedTask Task { get; set; }
    }
}
