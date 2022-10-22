using CleanArchMVC.Domain.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMVC.Infra.Data.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticateService(UserManager<ApplicationUser> userManger, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManger;
            _signInManager = signInManager;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);
            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            var user = new ApplicationUser { UserName = email, Email = email };
            
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
                await _signInManager.SignInAsync(user, isPersistent: false);

            return result.Succeeded;
        }

    }
}
