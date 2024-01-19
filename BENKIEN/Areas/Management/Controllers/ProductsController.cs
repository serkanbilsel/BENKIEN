using BENKIEN.Data;
using BENKIEN.Entities;
using BENKIEN.Models;
using BENKIEN.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BENKIEN.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly DatabaseContext _context;

        public ProductsController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Products.Include(p => p.Category).Include(p => p.Brand).Include(p => p.ProductImages).ToList());

        }

        public IActionResult Details(int id)
        {
            var product = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var productViewModel = new ProductViewModel
            {
                Product = product,
                Categories = _context.Categories.ToList(),
                Brands = _context.Brands.ToList(),
            };

            return View(productViewModel);
        }

        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            ViewBag.Brands = new SelectList(_context.Brands.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(ProductViewModel viewModel, IEnumerable<IFormFile> ProductImages, IFormFile? Image)
        {
            try
            {
                var product = viewModel.Product;

                if (ProductImages != null)
                {
                    List<ProductImage> newImages = await FileHelperMulti.FileLoaderAsync(ProductImages);

                    newImages.ForEach(image =>
                    {
                        image.Product = product;
                        image.ImageUrl = "/Products/" + image.ImageName;
                    });

                    product.ProductImages = newImages;
                }



                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                TempData["Message"] = "<div class='alert alert-success'>Ürün başarıyla eklendi</div>";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ürün eklenirken bir hata oluştu. Lütfen tekrar deneyin.");

                var categories = _context.Categories.ToList();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");

                var brands = _context.Brands.ToList();
                ViewBag.Brands = new SelectList(brands, "Id", "Name");

                return View(nameof(Create));
            }
        }

        public ActionResult Edit(int id)
        {
            var existingProduct = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .FirstOrDefault(p => p.Id == id);

            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            ViewBag.Brands = new SelectList(_context.Brands.ToList(), "Id", "Name");

            // Modeli ProductViewModel olarak düzenle
            var productViewModel = new ProductViewModel
            {
                Product = existingProduct,
                ProductImages = existingProduct.ProductImages.ToList() // ProductImages'ı burada oluştur
            };

            return View(productViewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ProductViewModel viewModel, IEnumerable<IFormFile> ProductImages, bool? RemoveImage, List<int> imageIds)
        {
            try
            {
                var Products = await _context.Products
                    .Include(p => p.ProductImages)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (Products == null)
                {
                    return NotFound();
                }

                Products.Name = viewModel.Product.Name;
                Products.Description = viewModel.Product.Description;
                Products.Price = viewModel.Product.Price;
                Products.ProductCode = viewModel.Product.ProductCode;
                Products.CategoryId = viewModel.Product.CategoryId;
                Products.BrandId = viewModel.Product.BrandId;
                Products.Stock = viewModel.Product.Stock;
                Products.IsAvailable = viewModel.Product.IsAvailable;
                Products.IsActive = viewModel.Product.IsActive;
                Products.IsHome = viewModel.Product.IsHome;
                Products.UpdatedAt = viewModel.Product.UpdatedAt;

                if (ProductImages != null && ProductImages.Any())
                {
                    List<ProductImage> newImages = await FileHelperMulti.FileLoaderAsync(ProductImages);

                    newImages.ForEach(image =>
                    {
                        image.Product = Products;
                        image.ImageUrl = "/Products/" + image.ImageName;
                    });

                    Products.ProductImages.AddRange(newImages);
                }

                // Resim silme işlemi
                if (imageIds != null && imageIds.Any())
                {
                    foreach (var imageId in imageIds)
                    {
                        var image = Products.ProductImages.FirstOrDefault(i => i.Id == imageId);

                        if (image != null)
                        {
                            // Resmi dosya sisteminden ve veritabanından kaldır
                            FileHelperMulti.FileRemover(new List<string> { image.ImageName });
                            Products.ProductImages.Remove(image);
                        }
                    }
                }

                // Save changes to the database
                await _context.SaveChangesAsync();

                TempData["Message"] = "<div class='alert alert-success'>Ürün başarıyla güncellendi</div>";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ürün güncellenirken bir hata oluştu. Lütfen tekrar deneyin.");

                // Kategori ve Marka listelerini ViewBag'e ekleyerek Edit view'ını döndür
                ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
                ViewBag.Brands = new SelectList(_context.Brands.ToList(), "Id", "Name");

                return View(nameof(Edit), viewModel);
            }
        }









        public ActionResult Delete(int id)
        {
            return View(_context.Products.Include(c => c.Category).FirstOrDefault(p => p.Id == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Product collection)
        {
            try
            {
                _context.Products.Remove(collection);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
