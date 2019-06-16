using Application.Commands.CategoryCommands;
using Application.Commands.ManufacturerCommands;
using Application.Commands.ProductCommands;
using Application.DataTransfer.ProductDto;
using Application.Exceptions;
using Application.Helpers;
using Application.Searches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Linq;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.Controllers {
    public class ProductsController : Controller {

        private readonly ICreateProductCommand _create;
        private readonly ISearchProductsCommand _search;
        private readonly IGetProductCommand _get;
        private readonly IDeleteProductCommand _delete;
        private readonly IEditProductCommand _edit;

        private readonly IGetCategoriesCommand _get_categories;
        private readonly IGetManufacturersCommand _get_manufacturers;

        public ProductsController(ICreateProductCommand create, ISearchProductsCommand search, IGetProductCommand get, IDeleteProductCommand delete, IEditProductCommand edit, IGetCategoriesCommand get_categories, IGetManufacturersCommand get_manufacturers) {
            _create = create;
            _search = search;
            _get = get;
            _delete = delete;
            _edit = edit;
            _get_categories = get_categories;
            _get_manufacturers = get_manufacturers;
        }



        // GET: Products
        public ActionResult Index([FromQuery] ProductSearch search) {

            var model = new CatalogViewModel {
                Response = _search.Execute(search)
            };

            model.Products = model.Response.Data;

            ViewBag.PerPage = search.PerPage;

            return View(model);
        }

        // GET: Products/Details/5
        public ActionResult Details(int id) {

            try {

                var p = _get.Execute(id);

                return View(p);

            } catch(EntityNotFoundException) {

                return RedirectToAction(nameof(Index));
            } catch(Exception) {

                return RedirectToAction(nameof(Index));
            }


        }

        // GET: Products/Create
        public ActionResult Create() {
            ViewBag.Categories = _get_categories.Execute();
            ViewBag.Manufacturers = _get_manufacturers.Execute();


            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] InsertProduct p) {

            var extension = Path.GetExtension(p.Image.FileName);

            if(!FileUpload.AllowedExtensions.Contains(extension)) {
                TempData["error"] = "Image extension is not allowed.";
                RedirectToAction(nameof(Create));
            }

            try {

                var new_file_name = Guid.NewGuid().ToString() + "_" + p.Image.FileName;

                var file_path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", new_file_name);

                var dto = new CreateProductDto {
                    Name = p.Name,
                    Image = new_file_name,
                    Description = p.Description,
                    ManufacturerId = p.ManufacturerId,
                    CategoryId = p.CategoryId,
                    Price = p.Price
                };

                _create.Execute(dto);

                p.Image.CopyTo(new FileStream(file_path, FileMode.Create));

                return RedirectToAction(nameof(Index));

            } catch(EntityNotFoundException e) {

                TempData["error"] = e.Message;
                return RedirectToAction(nameof(Create));

            } catch(EntityAlreadyExistsException e) {

                TempData["error"] = e.Message;
                return RedirectToAction(nameof(Create));

            } catch(Exception) {

                TempData["error"] = "Something went wrong.";
                return RedirectToAction(nameof(Create));
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id) {
            try {

                var product = _get.Execute(id);

                ViewBag.Categories = _get_categories.Execute();
                ViewBag.Manufacturers = _get_manufacturers.Execute();

                return View(product);

            } catch(EntityAlreadyExistsException) {

                return RedirectToAction(nameof(Index));

            } catch(EntityNotFoundException) {

                return RedirectToAction(nameof(Index));

            } catch(Exception) {

                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [FromForm] EditProductDto dto) {

            return View();
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id) {
            try {

                _delete.Execute(id);

                return RedirectToAction(nameof(Index));

            } catch(EntityNotFoundException) {

                return RedirectToAction(nameof(Index));
            } catch(Exception) {

                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection) {
            try {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }
    }
}