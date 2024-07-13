using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [DisplayName("Activity Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ActivityDate { get; set; }

        [DisplayName("Done By")]
        public string DoneBy { get; set; }

        [DisplayName("Description")]
        public string ActivityDescription { get; set; }

        public int TaskId { get; set; }
        public AssignedTask Task { get; set; }
    }
}
