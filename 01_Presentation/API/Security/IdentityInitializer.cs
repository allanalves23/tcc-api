﻿using System;
using API.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace API.Security
{
    public class IdentityInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityInitializer(
            ApplicationDbContext context,
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
                    var resultado = _roleManager.CreateAsync(
                        new IdentityRole(RolesModel.Product)).Result;
                    if (!resultado.Succeeded)
                    {
                        throw new Exception(
                            $"Erro durante a criação da role {RolesModel.Product}.");
                    }
                }

                CreateUser(
                    new ApplicationUser()
                    {
                        UserName = "awallan259@gmail.com",
                        Email = "awallan259@gmail.com",
                        EmailConfirmed = true
                    }, "Pass123$", RolesModel.Product);

                CreateUser(
                    new ApplicationUser()
                    {
                        UserName = "davi.demk@yahoo.com.br",
                        Email = "davi.demk@yahoo.com.br",
                        EmailConfirmed = true
                    }, "Pass123$", RolesModel.Product);
            }
        }
        private void CreateUser(
            ApplicationUser user,
            string password,
            string initialRole = null)
        {
            if (_userManager.FindByNameAsync(user.UserName).Result == null)
            {
                var resultado = _userManager
                    .CreateAsync(user, password).Result;

                if (resultado.Succeeded &&
                    !String.IsNullOrWhiteSpace(initialRole))
                {
                    _userManager.AddToRoleAsync(user, initialRole).Wait();
                }
            }
        }
    }
}