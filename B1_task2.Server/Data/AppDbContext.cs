using B1_task2.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Transactions;
using File = B1_task2.Server.Models.File;
namespace B1_task2.Server.Data;
public class AppDbContext : DbContext
{
    public DbSet<File> Files { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Balance> Balances { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=b1_task1;Username=postgres;Password=postgres;");
    }*/
}