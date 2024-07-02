using Microsoft.AspNetCore.Mvc;
using ProductSaleSystem.Models;
using ProductSaleSystem.Repositories;

namespace ProductSaleSystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(ProductDTO productDTO)
        {
            if (productDTO.FormFile != null && productDTO.FormFile.Length > 0)
            {
                DirectoryInfo fileType = new DirectoryInfo(productDTO.FormFile.FileName);
                string fileName = Guid.NewGuid().ToString() + fileType.Extension;

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    productDTO.FormFile.CopyToAsync(stream);
                }
                productDTO.ImagePath = "/img/" + fileName;
            }
            else
            {
                ViewBag.Message = "Lütfen bir dosya seçin.";
            }
            _productRepository.AddProduct(productDTO);
            return RedirectToAction("ProductList");

        }
        public IActionResult ProductList()
        {
            
            var product = _productRepository.GetAllProducts();
            return View(product);
        }
        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            ViewBag.Categories = _productRepository.GetAllProducts();
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult UpdateProduct(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = _productRepository.GetProductById(productDTO.Id);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                if (productDTO.FormFile != null && productDTO.FormFile.Length > 0)
                {
                    // Eski dosyayı silme
                    if (!string.IsNullOrEmpty(existingProduct.ImagePath))
                    {
                        var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingProduct.ImagePath.TrimStart('/'));
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }

                    // Yeni dosyayı kaydetme
                    var fileExtension = Path.GetExtension(productDTO.FormFile.FileName);
                    string fileName = Guid.NewGuid().ToString() + fileExtension;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        productDTO.FormFile.CopyTo(stream); // .Wait() yerine doğrudan senkron CopyTo kullanıyoruz
                    }
                    productDTO.ImagePath = "/img/" + fileName;
                }
                else
                {
                    productDTO.ImagePath = existingProduct.ImagePath; // Mevcut resmi koruyoruz
                }



                _productRepository.UpdateProduct(productDTO);
                return RedirectToAction("ProductList");
            }
            return View(productDTO);
        }
        [HttpGet]
        public IActionResult DeleteProduct(int  id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            _productRepository.DeleteProduct(product);
            return RedirectToAction("ProductList");

        }
    }
}
