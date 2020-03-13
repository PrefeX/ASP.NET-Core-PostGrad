using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ASP.NET_PostGrad.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Subject { get; set; }
        public int? SupervisorId { get; set; }
        [JsonIgnore]
        public Supervisor Supervisor { get; set; }
    }
}
