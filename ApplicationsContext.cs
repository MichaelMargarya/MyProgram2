using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProgram;

public class ApplicationsContext : DbContext
{

    public DbSet<User> Users { get; set; }


    public ApplicationsContext(DbContextOptions<ApplicationsContext> options) : base(options){}
    public ApplicationsContext() : base()
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

       
        modelBuilder.Entity<User>().ToTable("Users");

      
        modelBuilder.Entity<User>().HasKey(u => u.Id);

       
        modelBuilder.Entity<User>().Property(u => u.Login).HasMaxLength(50);
        modelBuilder.Entity<User>().Property(u => u.Password).HasMaxLength(100);
        modelBuilder.Entity<User>().Property(u => u.Email).HasMaxLength(100);   
    }

}
