using Microsoft.EntityFrameworkCore;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.DataContext
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

    }
}
