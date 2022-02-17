using AsesoraApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsesoraApi.Context
{
    public class AppDbContext: DbContext
    {
        public DbSet<UserxImage> Userx { get; set; }
        public DbSet<School> School { get; set; }
        public DbSet<Building> Building { get; set; }
        public DbSet<Classroom> Classroom { get; set; }
        public DbSet<Career> Career { get; set; }
        public DbSet<Major> Major { get; set; }
        public DbSet<Subjectx> Subjectx { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Advisor> Advisor { get; set; }
        public DbSet<Advise> Advise { get; set; }
        public DbSet<Connection> Connection { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<Config> Config { get; set; }
        public DbSet<Props> Props { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions)
            :base(dbContextOptions)
        {

        }
    }
}
