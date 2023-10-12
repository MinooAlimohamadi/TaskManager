using Microsoft.EntityFrameworkCore;
using TaskManager.Logic.Domain;
using Task = TaskManager.Logic.Domain.Task;

namespace TaskManager.Logic.Data
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

        public DbSet<Task> Task { get; set; } = null!;
        public DbSet<User> User { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs) { fk.DeleteBehavior = DeleteBehavior.Restrict; }

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();

            //add user
            modelBuilder.Entity<User>().HasData(new Domain.User
            {
                Id = 1,
                FirstName = "Minoo",
                LastName = "Alimohammadi",
                UserName = "Admin",
                Password = "123456789",
                Email = "Minoo.2120@gmail.com",
                CreateDate = DateTime.Now
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
