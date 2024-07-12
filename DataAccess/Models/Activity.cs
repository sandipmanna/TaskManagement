using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [DisplayName("Activity Date")]
        public DateTime ActivityDate { get; set; }

        [DisplayName("Done By")]
        public string DoneBy { get; set; }

        [DisplayName("Description")]
        public string ActivityDescription { get; set; }
        public AssignedTask Task { get; set; }
    }
}
