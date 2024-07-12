using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Web;

namespace TaskManagement
{
    public class SystemTextJsonSamples : JsonNamingPolicy
    {
        public override string ConvertName(string name) =>
            name.Replace("_", string.Empty);
    }
}