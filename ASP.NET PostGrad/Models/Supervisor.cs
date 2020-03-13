using System.Text.Json.Serialization;

namespace ASP.NET_PostGrad.Models
{
    public class Supervisor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Subject { get; set; }
        [JsonIgnore]
        public Student Student { get; set; }
    }
}