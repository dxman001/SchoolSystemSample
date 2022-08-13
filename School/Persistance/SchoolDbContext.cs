namespace School.Persistance;
using Microsoft.EntityFrameworkCore;
using School.Models;

public class SchoolDbContext : DbContext
{
    //public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
    //{        
    //}

    public SchoolDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }

}

