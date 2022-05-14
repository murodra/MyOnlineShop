using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyOnlineShop.Data.Entities;
using MyOnlineShop.Services.Interfaces;
using MyOnlineShop.Ui.Models;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;

namespace MyOnlineShop.Ui.Controllers
{
    [Authorize]
    public class StoreController : Controller
    {
        private readonly IRepository<Store> _storeRepository;

        public StoreController(IRepository<Store> storeRepository)
        {
            _storeRepository = storeRepository;
        }

        // GET: Category
        public ActionResult Index(int id)
        {
            var store = _storeRepository.Get(id);
            if (store == null)
            {
                TempData["Message"] = "Store do not exits!";
            }

            var svm = new StoreViewModel()
            {
                Id = store.Id,
                StoreName = store.StoreName,
                Address = store.Address,
                City = store.City,
                PictureStr = Convert.ToBase64String(store.StoreLogo)
            };
            return View(svm);
        }
        public ActionResult List()
        {
            var stores = _storeRepository.GetAll().Select(x =>
            new StoreViewModel()
            {
                Id = x.Id,
                StoreName = x.StoreName,
                Address = x.Address,
                City = x.City,
                PictureStr = x.StoreLogo != null ? Convert.ToBase64String(x.StoreLogo) : "",
            });

            return View(stores);
        }


        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StoreViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var entity = new Store()
            {
                StoreName = model.StoreName,
                Address = model.Address,
                City = model.City
            };



            if (model.StoreLogo != null && model.StoreLogo.Length > 0)
            {
                if (model.StoreLogo.Length > 20000)
                {
                    // draw
                    TempData["Message"] = "StoreLogo size can't exceed 20kb!";
                    return View(model);
                }
                //if (model.StoreLogo==null)
                //{
                //    model.StoreLogo =
                //}

                using (var ms = new MemoryStream())
                {
                    model.StoreLogo.CopyTo(ms);
                    entity.StoreLogo = ms.ToArray();
                }
            }

            var res = _storeRepository.Add(entity);
            if (res)
            {
                return RedirectToAction(nameof(List));
            }

            TempData["Message"] = "Store create failed!";
            return View(model);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            var store = _storeRepository.Get(id);
            if (store == null)
            {
                TempData["Message"] = "Store do not exits!";
            }

            var cvm = new StoreViewModel()
            {
                Id = store.Id,
                StoreName = store.StoreName,
                Address = store.Address,
                City = store.City,
                PictureStr = Convert.ToBase64String(store.StoreLogo)
            };

            return View(cvm);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StoreViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var entity = _storeRepository.Get(expression: x => x.Id == model.Id);

            entity.Id = model.Id;
            entity.StoreName = model.StoreName;
            entity.Address = model.Address;

            entity.UpdatedById = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            if (model.StoreLogo != null && model.StoreLogo.Length > 0)
            {
                if (model.StoreLogo.Length > 20000)
                {
                    TempData["Message"] = "StoreLogo size can not exceed 20kb!";
                    return View(model);
                }

                using (var ms = new MemoryStream())
                {
                    model.StoreLogo.CopyTo(ms);
                    entity.StoreLogo = ms.ToArray();
                }
            }

            var res = _storeRepository.Update(entity);
            if (res)
            {
                TempData["Message"] = "Store update is successful!";
                return RedirectToAction("List");
            }
            TempData["Message"] = "Store update failed!";
            return View(model);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            var res = _storeRepository.Delete(id);
            TempData["Message"] = res ? "Delete is successful" : "Delete failed";
            return RedirectToAction("List");
        }

    }
}
