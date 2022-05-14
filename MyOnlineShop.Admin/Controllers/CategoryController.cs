using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyOnlineShop.Admin.Models;
using MyOnlineShop.Data;
using MyOnlineShop.Data.Entities;
using MyOnlineShop.Services.Interfaces;

namespace MyOnlineShop.Admin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryController(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        private List<SelectListItem> GetAllCategories()
        {
            return _categoryRepository.GetAll()
                .Select(x => new SelectListItem()
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString()
                })
                .ToList();
        }

        // GET: Category
        public ActionResult List(int id)
        {
            if (id == 0)
            {
                ViewBag.parentname = "";
                ViewBag.parentid = 0;
                var products = _categoryRepository.GetAll(expression: x => x.CategoryParentId == null).Select(x =>
                new CategoryViewModel()
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName,
                    Description = x.Description,
                    PictureStr = x.Picture != null ? Convert.ToBase64String(x.Picture) : "",

                    ParentId =0
                });
                return View(products);
            }
            else
            {
                var parent = _categoryRepository.Get(id);
                ViewBag.parentname=parent.CategoryName;
                ViewBag.parentid=parent.Id;
                var products = _categoryRepository.GetAll(expression: x => x.CategoryParentId == id).Select(x =>
                new CategoryViewModel()
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName,
                    Description = x.Description,
                    PictureStr = x.Picture != null ? Convert.ToBase64String(x.Picture) : "",

                    ParentCategoryName = parent.CategoryName,
                    ParentId = parent.Id,
                    GrandParentId = parent.CategoryParentId
                });
                return View(products);   
            }          
        }


        // GET: Category/Create
        public ActionResult Create(int id)
        {
            if (id!=0)
            {
                var category = _categoryRepository.Get(id);
                ViewBag.parentname = category.CategoryName;
                ViewBag.parentid = category.Id;
            }
            else
            {
                ViewBag.parentname = "";
            }
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var entity = new Category()
            {
                CategoryName = model.CategoryName,
                Description = model.Description,
                CategoryParentId = model.ParentId
            };

            entity.CreatedById = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            if (model.Picture != null && model.Picture.Length > 0)
            {
                if (model.Picture.Length > 20000)
                {
                    // draw
                    TempData["Message"] = "Picture size can't exceed 20kb!";
                    return View(model);
                }

                using (var ms = new MemoryStream())
                {
                    model.Picture.CopyTo(ms);
                    entity.Picture = ms.ToArray();
                }
            }

            var res = _categoryRepository.Add(entity);
            if (res)
            {
                return RedirectToAction("List", new { @id = 0 });
            }

            TempData["Message"] = "Category create failed!";
            return View(model);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Categories = GetAllCategories();

            var category = _categoryRepository.Get(id);
            if (category == null)
            {
                TempData["Message"] = "Category do not exits!";
            }

            var cvm = new CategoryViewModel()
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                Description = category.Description,
                PictureStr = category.Picture != null ? Convert.ToBase64String(category.Picture):""
            };

            return View(cvm);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var entity = _categoryRepository.Get(expression: x => x.Id == model.Id);

            entity.Id = model.Id;
            entity.CategoryName = model.CategoryName;
            entity.Description = model.Description;
            entity.CategoryParentId = model.CategoryId;


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

            var res = _categoryRepository.Update(entity);
            if (res)
            {
                TempData["Message"] = "Category update is successful!";
                return RedirectToAction("List");
            }
            TempData["Message"] = "Category update failed!";
            return View(model);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            var res = _categoryRepository.Delete(id);
            TempData["Message"] = res ? "Delete is successful" : "Delete failed";
            return RedirectToAction("List");
        }

    }
}
