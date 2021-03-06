using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;


namespace itstep_shop.Models
{
    public class ApplicationContext : DbContext
    {

        public DbSet<Category> Categories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }


        public ApplicationContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // 
            //modelBuilder.Entity<User>()
            //    .HasOne(a => a.Cart)
            //    .WithOne(a => a.User)
            //    .HasForeignKey<Cart>(c => c.UserId);

            //modelBuilder.Entity<User>().HasOne(a => a.Cart).WithOne(a => a.User).HasForeignKey<Cart>(c => c.UserId)


            //modelBuilder.Entity<User>()
            //    .HasOne(a => a.Cart)
            //    .WithOne(a => a.User)
            //    .HasForeignKey<Cart>(c => c.UserId);

            //modelBuilder.Entity<Cart>()
            //    .HasOne(a => a.User)
            //    .WithOne(a => a.Cart)
            //    .HasForeignKey<User>(c => c.CartId);

            Role[] roles = new Role[]
            {
                new Role {Id = 1, Name = "admin"},
                new Role {Id = 2, Name = "user"}
            };

            User[] users = new User[]
            {
                new User {Id = 1, Name = "Администратор", Email = "admin@gmail.com", Password = "qwerty", RoleId = 1},
                new User {Id = 2, Name = "Макс", Email = "max@gmail.com", Password = "qwerty", RoleId = 2},
            };

            Category[] categories = new Category[]
            {
                new Category {Id = 1, Name = "Фрукты"},
                new Category {Id = 2, Name = "Овощи"},
                new Category {Id = 3, Name = "Безалкогольные напитки"},
            };

            Product[] products = new Product[]
            {
                new Product {Id = 1, Name = "Банан", CategoryId = 1, ImageUri = "https://clipart-best.com/img/banana/banana-clip-art-18.png"},
                new Product {Id = 2, Name = "Огурец", CategoryId = 2, ImageUri = "https://purepng.com/public/uploads/large/purepng.com-cucumbercucumbervegetablespicklegreenfoodhealthycucumbers-481522161925n6wbx.png"},
                new Product {Id = 3, Name = "Coca-Cola", CategoryId = 3, ImageUri = "https://pngimg.com/uploads/cocacola/cocacola_PNG21.png"},
            };

            modelBuilder.Entity<Role>().HasData(roles);
            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Product>().HasData(products);

            base.OnModelCreating(modelBuilder);
        }
    }
}
