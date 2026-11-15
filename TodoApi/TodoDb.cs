using Microsoft.EntityFrameworkCore;

namespace TodoApi
{
    public class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions options) : base(options) { }
        public DbSet<TodoItem> TodoItems { get; set; } = null!;
        public DbSet<Member> Members { get; set; } = null!;
        public DbSet<Place> Places { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase("Todos");
            var connectionSrting = "Server=localhost;Database=todos;User=root";
            optionsBuilder.UseMySql(connectionSrting, ServerVersion.AutoDetect(connectionSrting));
            //optionsBuilder.UseMySQL("server=localhost;database=todos;user=root");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TodoItem>(entity =>{
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired();
                entity.HasOne(t => t.Member).WithMany(m => m.TodoItems);
                entity.HasOne(p => p.Place).WithMany(p => p.TodoItems);
            });
            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x =>x.Name).IsRequired();
            });
            modelBuilder.Entity<Place>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x =>x.Name).IsRequired();
            });
        }
    }
}
