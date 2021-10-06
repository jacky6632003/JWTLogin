using JWTLogin.Models.ViewModel;
using JWTLogin.Seed.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTLogin.Seed.Data
{
    public class UserSeed
    {
        public static async Task SeedAsync(UserContext context)
        {
            context.Database.Migrate();
            if (!context.Users.Any())
            {
                UserViewModel data = new UserViewModel()
                {
                    Username = "test",
                    Password = "gWdAhIz7ENJhGHnBXcoCtg=="
                };
                context.Users.Add(data);
               await  context.SaveChangesAsync();
            }
        }

     
    }
}
