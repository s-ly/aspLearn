using Microsoft.EntityFrameworkCore;
using WebSchool.Models;

namespace WebSchool.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<WebUser> WebUsers { get; set; }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     // Опционально: настройка таблицы
    //     modelBuilder.Entity<User>().ToTable("Users");
    // }
}