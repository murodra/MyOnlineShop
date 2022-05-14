using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyOnlineShop.Data.Entities;
using MyOnlineShop.Services.Interfaces;
using MyOnlineShop.Ui.Models;
using System;
using System.Linq;
using System.Security.Claims;

namespace MyOnlineShop.Ui.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository) // constructor injection
        {
            // _db = new 
            _customerRepository = customerRepository;
        }
        public IActionResult Index()
        {
            int id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var customer = _customerRepository.Get(id);
            var cvm = new CustomerViewModel()
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    Lastname = customer.Lastname, 
                    Email = customer.Email, 
                    Address = customer.Address 
                };
            return View(cvm);
        }

        public ActionResult Edit(int id)
        {
            var customer = _customerRepository.Get(id);

            var cvm = new CustomerViewModel()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                Email = customer.Email,
                Password = customer.Password,
                Address = customer.Address,
                Phone = customer.Phone,

            };
            return View(cvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var entity = _customerRepository.Get(expression: x => x.Id == model.Id);

            entity.FirstName = model.FirstName;
            entity.Lastname = model.Lastname;
            entity.Email = model.Email;
            entity.Password = model.Password;
            entity.Address = model.Address;
            entity.Phone = model.Phone;



            var res = _customerRepository.Update(entity);
            if (res)
            {
                TempData["Message"] = "Profile update failed";
                return RedirectToAction("List");
            }
            TempData["Message"] = "Profile update successful";
            return View();
        }
    }
}
