﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;




namespace MyProgram;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
  
    public User(string login,string email,string pass)
    {
        Login = login;
        Password = pass;
        Email = email;
    }
    public User() { }
}




//public class UserDbContext : DbContext
//{
//    public DbSet<User> Users { get; set; }


//    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
//    {
//    }
//}
