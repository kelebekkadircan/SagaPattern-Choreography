﻿using Microsoft.EntityFrameworkCore;

namespace ORDER.API.Models.Context
{
    public class OrderAPIDbContext : DbContext
    {


        public OrderAPIDbContext(DbContextOptions options) : base(options)
        {
        }

      

       public DbSet<Order> Orders { get; set; }

       public DbSet<OrderItem> OrderItems { get; set; }
    }
}
