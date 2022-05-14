using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOnlineShop.Data.Entities;
using MyOnlineShop.Services.Interfaces;
using MyOnlineShop.Ui.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyOnlineShop.Ui.Controllers
{
    public class AuthController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        public AuthController(ICustomerRepository customerRepository) // constructor injection
        {
            // _db = new 
            _customerRepository = customerRepository;
        }
        // GET: AuthController
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var customer = _customerRepository.Login(model.Email, model.Password);
            if (customer == null)
            {
                TempData["Message"] = "Incorrect E-mail or password";
                return View(model);
            }
            var claims = new List<Claim>()
            {
            new Claim(ClaimTypes.Name,customer.FirstName),
            new Claim(ClaimTypes.Surname,customer.Lastname),
            new Claim(ClaimTypes.Email,customer.Email),
            new Claim(ClaimTypes.NameIdentifier,customer.Id.ToString()),
            //new Claim(ClaimTypes.Role,customer.Title.ToString()),
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
            var customer = new Customer()
            {
                FirstName = model.FirstName,
                Lastname = model.LastName,
                Email = model.Email,
                Password = model.Password,

                IsActive = true,
                CreatedDate = DateTime.Now,
            };
            //user.CreatedById = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var res = _customerRepository.Add(customer);
            if (res)
            {
                return RedirectToAction("Index", "Home");
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
            TempData["Message"] = "Account creation failed!";
            return View(model);

        }
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}
