using CleanArchMVC.Application.DTOs;
using CleanArchMVC.Application.Interfaces;
using CleanArchMVC.Application.Services;
using CleanArchMVC.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMVC.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        private IWebHostEnvironment _webHostEnvironment;
        public ProductsController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProducts();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO product)
        {
            var productCategory = await _categoryService.GetById(product.CategoryId);
            product.Category = productCategory;
            if (ModelState.IsValid)
            {
                await _productService.Add(product);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.CategoryId =
                            new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            }
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var productDto = await _productService.GetById(id);
            if (productDto == null) return NotFound();
            ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name", productDto.CategoryId);

            return View(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.Update(product);
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var productDto = await _productService.GetById(id);
            if (productDto == null) return NotFound();

            return View(productDto);
        }

        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null) return NotFound();
            try
            {
                await _productService.Remove(id);
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var productDto = await _productService.GetById(id);
            if (productDto == null) return NotFound();

            var wwwroot = _webHostEnvironment.WebRootPath;
            var image = Path.Combine(wwwroot, "images\\" + productDto.Image);
            var exists = System.IO.File.Exists(image);
            ViewBag.ImageExist = exists;
            return View(productDto);
        }
    }
}
