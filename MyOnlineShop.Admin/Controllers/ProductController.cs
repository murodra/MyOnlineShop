using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyOnlineShop.Admin.Models;
using MyOnlineShop.Data.Entities;
using MyOnlineShop.Services.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;

namespace MyOnlineShop.Admin.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _productRepository;
        public ProductController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: ProductController
        public ActionResult List()
        {
            var products = _productRepository.GetAll(includes: x => x.Include(y => y.Category)).Select(x =>
                new ProductViewModel()
                {
                    Id = x.Id,
                    ProductName = x.ProductName,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.CategoryName,
                    UnitPrice = x.UnitPrice,
                    UnitsInStock = x.UnitsInStock,
                    PictureStr = x.Picture != null ? Convert.ToBase64String(x.Picture) : "",
                    Discontinued = x.Discontinued
                });

            return View(products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = _productRepository.Get(id);
            if (product == null)
            {
                TempData["Message"] = "Product do not exist!";
            }
            var pvm = new ProductViewModel()
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                PictureStr = Convert.ToBase64String(product.Picture)
            };
            return View(pvm);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var entity = new Product()
            {
                Id = model.Id,
                ProductName = model.ProductName,
                CategoryId = model.CategoryId,
                UnitPrice = model.UnitPrice,
                UnitsInStock = model.UnitsInStock,
                Discontinued = model.Discontinued,
            };
            entity.UpdatedById = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

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
                TempData["Message"] = "Product update is successful!";
                return RedirectToAction("List");
            }
            TempData["Message"] = "Product update failed!";
            return View(model);

        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            var res = _productRepository.Delete(id);
            TempData["Message"] = res ? "Delete is successful" : "Delete failed";
            return RedirectToAction("List");
        }

    }
}
