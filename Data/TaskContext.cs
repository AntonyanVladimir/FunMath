using FunMath.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunMath.Data
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options):base(options)
        {

        }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Challenge> Challenges { get; set; }

    }
}
