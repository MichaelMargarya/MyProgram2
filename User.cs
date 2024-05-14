using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MyProgram;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}

public class UserDbContext : DbContext
{
    public DbSet<User> Users { get; set; }


    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }
}
