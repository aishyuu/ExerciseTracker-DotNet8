using ExerciseTracker_DotNet8.Models;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTracker_DotNet8.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<ExerciseLog> ExerciseLog { get; set; }
    }
}
