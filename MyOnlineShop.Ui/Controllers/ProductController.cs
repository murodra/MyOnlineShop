using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyOnlineShop.Data.Entities;
using MyOnlineShop.Services.Interfaces;
using MyOnlineShop.Ui.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;

namespace MyOnlineShop.Ui.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _productRepository;
        public ProductController(IRepository<Product> productRepository) // constructor injection
        {
            // _db = new 
            _productRepository = productRepository;
        }
        public IActionResult Index(int id)
        {
            var product = _productRepository.Get(expression: x => x.Id == id,
                includes: x => x
                 .Include(y => y.Category));
            var pvm = new ProductViewModel()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.CategoryName,
                UnitsInStock = product.UnitsInStock,
                UnitPrice = product.UnitPrice,
                Discontinued = product.Discontinued,
                PictureStr = Convert.ToBase64String(product.Picture)
            };
            return View(pvm);
        }



        public ActionResult List()
        {
            var products = _productRepository.GetAll().Select(x =>
            new ProductViewModel()
            {
                Id = x.Id,
                ProductName = x.ProductName,
                CategoryId = x.CategoryId,
                UnitsInStock = x.UnitsInStock,
                UnitPrice = x.UnitPrice,
                Discontinued = x.Discontinued,
                PictureStr = x.Picture != null ? Convert.ToBase64String(x.Picture) : "",
            });

            return View(products);
        }

        private List<SelectListItem> GetAllCategories()
        {
            return _productRepository.GetAll()
                .Select(x => new SelectListItem()
                {
                    Text = x.Category.CategoryName,
                    Value = x.Category.Id.ToString()
                })
                .ToList();
        }
        public ActionResult Create()
        {
            ViewBag.Categories = GetAllCategories();
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var entity = new Product()
            {
                ProductName = model.ProductName,
                CategoryId = model.CategoryId,
                UnitsInStock = model.UnitsInStock,
                UnitPrice = model.UnitPrice,
                Discontinued=model.Discontinued,

            };



            if (model.Picture != null && model.Picture.Length > 0)
            {
                if (model.Picture.Length > 20000)
                {
                    // draw
                    TempData["Message"] = "Picture size can't exceed 20kb!";
                    return View(model);
                }
                //if (model.StoreLogo==null)
                //{
                //    model.StoreLogo =
                //}

                using (var ms = new MemoryStream())
                {
                    model.Picture.CopyTo(ms);
                    entity.Picture = ms.ToArray();
                }
            }

            var res = _productRepository.Add(entity);
            if (res)
            {
                return RedirectToAction(nameof(List));
            }

            TempData["Message"] = "Product create failed!";
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var product = _productRepository.Get(id);

            var pvm = new ProductViewModel()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                CategoryId = product.CategoryId,
                UnitsInStock = product.UnitsInStock,
                UnitPrice = product.UnitPrice,
                Discontinued = product.Discontinued,
                PictureStr = Convert.ToBase64String(product.Picture)
            };
            return View(pvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var entity = _productRepository.Get(expression: x => x.Id == model.Id);

            entity.ProductName = model.ProductName;
            entity.CategoryId = model.CategoryId;
            entity.UnitsInStock = model.UnitsInStock;
            entity.UnitPrice = model.UnitPrice;
            entity.Discontinued = model.Discontinued;

            if (model.Picture != null && model.Picture.Length > 0)
            {
                if (model.Picture.Length > 20000)
                {
                    TempData["Message"] = "Picture size can not exceed 20kb!";
                    return View(model);
                }

                using (var ms = new MemoryStream())
                {
                    model.Picture.CopyTo(ms);
                    entity.Picture = ms.ToArray();
                }
            }

            var res = _productRepository.Update(entity);
            if (res)
            {
                TempData["Message"] = "Product update failed";
                return RedirectToAction("List");
            }
            TempData["Message"] = "Product update successful";
            return View();
        }
    }
}
