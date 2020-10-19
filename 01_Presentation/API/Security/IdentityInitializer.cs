using System;
using System.Threading.Tasks;
using API.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Repository.Contexts;

namespace API.Security
{
    public class IdentityInitializer
    {
        private readonly ApiContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityInitializer(
            ApiContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if (_context.Database.EnsureCreated())
            {
                if (!_roleManager.RoleExistsAsync(RolesModel.Product).Result)
                {
                    IdentityResult resultado = _roleManager.CreateAsync(
                        new IdentityRole(RolesModel.Product)).Result;

                    if (!resultado.Succeeded)
                        throw new Exception(
                            $"Erro durante a criação da role {RolesModel.Product}.");
                }

                SeedUsers();
            }
        }

        private async void SeedUsers()
        {
            await CreateUser(
                new ApplicationUser()
                {
                    UserName = "awallan259@gmail.com",
                    Email = "awallan259@gmail.com",
                    EmailConfirmed = true
                }, "Pass123$", RolesModel.Product);

            await CreateUser(
                new ApplicationUser()
                {
                    UserName = "davi.demk@yahoo.com.br",
                    Email = "davi.demk@yahoo.com.br",
                    EmailConfirmed = true
                }, "Pass123$", RolesModel.Product);
        }

        private async Task CreateUser(
            ApplicationUser user,
            string password,
            string initialRole = null)
        {
            if (await _userManager.FindByNameAsync(user.UserName) == null)
            {
                IdentityResult resultado = await _userManager.CreateAsync(user, password);

                if (resultado.Succeeded && !String.IsNullOrWhiteSpace(initialRole))
                    _userManager.AddToRoleAsync(user, initialRole).Wait();
            }
        }
    }
}