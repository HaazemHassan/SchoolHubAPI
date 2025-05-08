﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using School.Data.Entities.IdentityEntities;
using System.Text.Json;

namespace School.API.StartupExtension
{
    public static class ApplicationUserSeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> _userManager)
        {
            string usersJson = await File.ReadAllTextAsync("ApplicationUsers.json");
            List<ApplicationUser>? users = JsonSerializer.Deserialize<List<ApplicationUser>>(usersJson);

            int rolesInDb = await _userManager.Users.CountAsync();
            if (users is null || rolesInDb > 0)
                return;

            foreach (var user in users)
            {
                await _userManager.CreateAsync(user, "admin");
                await _userManager.AddToRoleAsync(user, "Admin");
            }
        }


    }
}
