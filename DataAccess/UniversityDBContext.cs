using Microsoft.EntityFrameworkCore;
using RestApi.Entities.DataEntities;

namespace RestApi.DataAccess
{
    public class UniversityDBContext : DbContext
    {
        public UniversityDBContext(DbContextOptions<UniversityDBContext> options) : base(options)
        {
        }

        #nullable enable
        public DbSet<User>? Users { get; set; }

        public DbSet<Course>? Courses { get; set; }

        public DbSet<Category>? Categories { get; set; }

        public DbSet<Student>? Students { get; set; }

        public DbSet<Chapter>? Chapters { get; set; }

    }
}
