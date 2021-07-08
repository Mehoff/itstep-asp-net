using itstep_shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace itstep_shop
{
    public static class SampleData
    {
        public static void Initialize(ApplicationContext ctx)
        {
            if (!ctx.Users.Any())
            {
                ctx.Roles.AddRange
                    (
                        new Role { Id = 1, Name = "admin" },
                        new Role { Id = 2, Name = "user" }
                    );

                ctx.SaveChanges();

                ctx.Users.AddRange
                    (
                        new User
                        {
                            Id = 1,
                            Name = "Администратор",
                            Email = "admin@gmail.com",
                            Password = "qwerty",
                            RoleId = 1
                        },
                        new User
                        {
                            Id = 2,
                            Name = "Макс",
                            Email = "max@gmail.com",
                            Password = "qwerty",
                            RoleId = 2
                        }
                    );

                ctx.SaveChanges();
            }

            if (!ctx.Products.Any())
            {
                ctx.Categories.AddRange(
                    new Category { Id = 1, Name = "Фрукты" },
                    new Category { Id = 2, Name = "Овощи" },
                    new Category { Id = 3, Name = "Безалкогольные напитки" }
                    );


                ctx.SaveChanges();

                ctx.Products.AddRange
                    (
                        new Product
                        {
                            Id = 1,
                            Name = "Банан",
                            CategoryId = 1
                        },
                        new Product
                        {
                            Id = 2,
                            Name = "Огурец",
                            CategoryId = 2
                        },
                        new Product
                        {
                            Id = 2,
                            Name = "Coca-Cola",
                            CategoryId = 3
                        }
                    );

                ctx.SaveChanges();
            }
        }
    }
}
