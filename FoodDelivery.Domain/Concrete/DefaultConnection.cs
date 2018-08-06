 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodDelivery.Domain.Entities;
using System.Data.Entity;

namespace FoodDelivery.Domain.Concrete
{
    public class DefaultConnection : DbContext
    {
        public DbSet<Product> Products { get; set; }
        //public DbSet<AspNetUsers> AspNetUsers { get; set; }
        //public DbSet<AspNetRoles> AspNetRoles { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<AspNetUsers>().HasMany(x => x.AspNetRoles)
        //                                      .WithMany(x => x.AspNetUsers)
        //                                      .Map(m =>
        //                                      {
        //                                          m.ToTable("AspNetUserRoles");
        //                                          m.MapLeftKey("Userid");
        //                                          m.MapRightKey("RoleId");
        //                                      });
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}