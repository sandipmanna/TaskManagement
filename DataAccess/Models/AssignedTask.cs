using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

using System.Text.Json.Serialization;

namespace DataAccess.Models
{
    public class AssignedTask
    {
        public int Id { get; set; }

        [JsonPropertyName("task_name")]
        public string TaskName { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }

        [NotMapped]
        public string TagValues { get; set; }

        [JsonPropertyName("due_date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }

        [JsonPropertyName("assigned_to")]
        public string AssignedTo { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
        public List<Activity> Activities { get; set; }
    }
}
