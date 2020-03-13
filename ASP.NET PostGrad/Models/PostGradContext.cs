using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_PostGrad.Models
{
    public class PostGradContext : DbContext
    {
        public PostGradContext(DbContextOptions<PostGradContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
    }
}
