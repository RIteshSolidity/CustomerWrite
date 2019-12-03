using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using OrderWrite.Models;

namespace OrderWrite.Repository
{
    public class OrderDBContext: DbContext
    {
        public OrderDBContext()
        {

        }

        public OrderDBContext(DbContextOptions options): base(options)
        {

        }

        public virtual DbSet<Orders> orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<Orders>().HasKey("OrderId");
            builder.Entity<Orders>().Property("OrderId").ValueGeneratedNever();
            builder.Entity<Orders>().OwnsOne(a => a.customer).Property("FirstName");
            builder.Entity<Orders>().OwnsOne(a => a.customer).Property("LastName");
            builder.Entity<Orders>().OwnsOne(a => a.customer).Property("Location");
            builder.Entity<Orders>().OwnsOne(a => a.customer).ToTable("CustomerInfo");

            builder.Entity<Orders>().OwnsMany(a => a.OrderItems).Property("ProductId");
            builder.Entity<Orders>().OwnsMany(a => a.OrderItems).Property("Quantity");
            builder.Entity<Orders>().OwnsMany(a => a.OrderItems).Property("Price");
            builder.Entity<Orders>().OwnsMany(a => a.OrderItems, w =>
            {
                w.Property<int>("OrderLineItemId");
                w.HasKey("OrderLineItemId");
                w.HasForeignKey("OrderId");
                w.ToTable("OrderLineItem");
            });
            base.OnModelCreating(builder);
        }
    }
}
