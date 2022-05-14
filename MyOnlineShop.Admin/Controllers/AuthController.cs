using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyOnlineShop.Admin.Models;
using MyOnlineShop.Data.Entities;
using MyOnlineShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyOnlineShop.Admin.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        public AuthController(IUserRepository userRepository) // constructor injection
        {
            // _db = new 
            _userRepository = userRepository;
        }
        // GET: AuthController
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login (LoginViewModel model)
        {
            var user = _userRepository.Login(model.Email, model.Password);
            if (user == null)
            {
                TempData["Message"] = "Incorrect E-mail or password";
                return View(model);
            }
            var claims = new List<Claim>()
            {
            new Claim(ClaimTypes.Name,user.FirstName),
            new Claim(ClaimTypes.Surname,user.LastName),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Role,user.Title.ToString()),
            };

            //2.2.sign in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);

            var returnUrl = Request.Form["returnurl"];
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: AuthController/Create
        public IActionResult Register()
        {
            return View();
        }

        // POST: AuthController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User() 
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,

                IsActive= true,
                CreatedDate= DateTime.Now,
            };
            //user.CreatedById = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var res = _userRepository.Add(user);
            if (res)
            {
                return RedirectToAction("Index","Home");
            }

            //var user1 = new User()
            //{
            //    FirstName = model.FirstName,
            //    Email = model.Email,
            //    LastName = model.LastName
            //};
            //var result = await UserManager.CreateAsync(user1,
            //    model.Password);
            //if (result.Succeeded)
            //{
            //    await SignInManager.SignInAsync(user1, isPersistent: false,
            //       rememberBrowser: false);
            //}
                TempData["Message"] = "User create failed!";
            return View(model);

        }
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}
