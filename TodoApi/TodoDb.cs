using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using MySql.EntityFrameworkCore;

namespace TodoApi
{
    public class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions options) : base(options)
        {
        }
        public DbSet<TodoItem> TodoItems { get; set; } = null!;
        public DbSet<Member> Members { get; set; } = null!;
        public DbSet<Place> Places { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase("Todos");
            //optionsBuilder.UseSqlite("Data Source=todos.db");
            //optionsBuilder.UseMySQL("server=localhost;database=todos;user=root");

            var connectionString = "Server=localhost;Database=todos;User=root";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired();
            });

            modelBuilder.Entity<TodoItem>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x =>x.Name).IsRequired();
                entity.HasOne(t => t.Member).WithMany(m => m.TodoItems);
                entity.HasOne(t => t.Place).WithMany(p => p.TodoItems);
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired();
            });
        }
    }
}
