using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MoviePro.Data;
using MoviePro.Models;
using MoviePro.Models.Database;
using MoviePro.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviePro.Services
{
    // The SeedService is used to make sure our app has initial logins and a collection and keeps the database in sync with our code.
    public class SeedService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedService(IOptions<AppSettings> appSettings,
                           ApplicationDbContext dbContext,
                           UserManager<IdentityUser> userManager,
                           RoleManager<IdentityRole> roleManager)
        {
            _appSettings = appSettings.Value;
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // This method will make sure that our database is up to date. It checks and compares what migrations have been executed in our database.
        public async Task ManageDataAsync()
        {
            await _dbContext.Database.MigrateAsync();
            await SeedRolesAsync();
            await SeedUsersAsync();
            await SeedCollections();
        }

        // This method creates an admin role if no roles exist in the database.
        private async Task SeedRolesAsync()
        {
            if (_dbContext.Roles.Any()) return;

            var adminRole = _appSettings.MovieProSettings.DefaultCredentials.Role;
            await _roleManager.CreateAsync(new IdentityRole(adminRole));
        }

        // This method creates an admin user from the default settings if no users exist.
        private async Task SeedUsersAsync()
        {
            if (_userManager.Users.Any()) return;

            var credentials = _appSettings.MovieProSettings.DefaultCredentials;
            var newUser = new IdentityUser()
            {
                Email = credentials.Email,
                UserName = credentials.Email,
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(newUser, credentials.Password);
            await _userManager.AddToRoleAsync(newUser, credentials.Role);

        }

        // This method creates a default collection of movies if a collection does not exist. 
        private async Task SeedCollections()
        {
            if (_dbContext.Collection.Any()) return;

            _dbContext.Add(new Collection()
            {
                Name = _appSettings.MovieProSettings.DefaultCollection.Name,
                Description = _appSettings.MovieProSettings.DefaultCollection.Description
            });

            await _dbContext.SaveChangesAsync();
        }
    }
}