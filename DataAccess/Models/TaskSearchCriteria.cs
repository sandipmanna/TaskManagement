using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class TaskSearchCriteria
    {
        [JsonPropertyName("task_name")]
        public string TaskName { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }

        [NotMapped]
        public string TagValues { get; set; }

        [JsonPropertyName("startDate")]
        public DateTime? StartDate { get; set; }
        
        [JsonPropertyName("endDate")]
        public DateTime? EndDate { get; set; }
        
        [JsonPropertyName("status")]
        public List<string> Statuses { get; set; }
    }
}
