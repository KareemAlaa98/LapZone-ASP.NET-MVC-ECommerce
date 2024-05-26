using E_Commerce_GP.Data;
using E_Commerce_GP.IRepository;
using E_Commerce_GP.Models;
using E_Commerce_GP.Repository;
using E_Commerce_GP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace E_Commerce_GP.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        IProductRepository productRepository;
        IProductImageRepository productImageRepository;
        IBrandRepository brandRepository;
        IDiscountRepository discountRepository;
        IReviewRepository reviewRepository;

        public ProductController(
            IWebHostEnvironment hostingEnvironment,
            IProductRepository productRepository,
            IBrandRepository brandRepository,
            IDiscountRepository discountRepository,
            IProductImageRepository productImageRepository,
            IReviewRepository reviewRepository)
        {
            this.productRepository = productRepository;
            this.brandRepository = brandRepository;
            this.discountRepository = discountRepository;
            _hostingEnvironment = hostingEnvironment;
            this.productImageRepository = productImageRepository;
            this.reviewRepository = reviewRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            var allProducts = productRepository.ReadAll();
            return View(allProducts);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            ProductViewModel productVM = new ProductViewModel();

            ViewData["listOfBrands"] = brandRepository.ReadAllBrand();
            ViewData["listOfDiscounts"] = discountRepository.ReadAll();
            return View(productVM);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNew(ProductViewModel productVM, List<IFormFile> productImages)
        {
            if (ModelState.IsValid)
            {
                var newProduct = productRepository.Create(productVM);

                if (newProduct != null)
                {
                    // Create a folder for the new product
                    string productFolder = "product-" + newProduct.Id.ToString();
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images", "products", productFolder);
                    Directory.CreateDirectory(uploadsFolder);

                    foreach (var image in productImages)
                    {
                        if (image != null && image.Length > 0)
                        {
                            // Generate a unique file name for each image
                            var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                            // Save the image to the file system
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                image.CopyTo(fileStream);
                            }

                            // Save the image path to the database using ProductImageRepository
                            productImageRepository.Create(newProduct.Id, uniqueFileName);
                        }
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle case where newProduct is null
                    // For example, return an error view or redirect to another action
                    return RedirectToAction("Error");
                }
            }

            ViewData["listOfBrands"] = brandRepository.ReadAllBrand();
            ViewData["listOfDiscounts"] = discountRepository.ReadAll();
            return View("create", productVM);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = productRepository.ReadById(id);
            if (product == null) { RedirectToAction("Index"); }

            // Fetch existing product images for the product ID
            var productImages = productImageRepository.GetProductImagesByProductId(id);

            ProductViewModel productVM = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                QuantityInStock = product.QuantityInStock,
                BrandId = product.BrandId,
                DiscountId = product.DiscountId,
                ProductImages = (List<ProductImage>)productImages // Add existing product images to the view model
            };

            // Pass productId to the view
            ViewData["ProductId"] = id;

            ViewData["listOfBrands"] = brandRepository.ReadAllBrand();
            ViewData["listOfDiscounts"] = discountRepository.ReadAll();
            ViewData["listOfImages"] = productImageRepository.GetProductImagesByProductId(productVM.Id);
            return View(productVM);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdit(ProductViewModel productVM, List<IFormFile> productImages)
        {
            if (ModelState.IsValid)
            {
                // Update product details
                productRepository.Update(productVM);

                // Check if there are new images to upload
                if (productImages != null && productImages.Any())
                {
                    // Get product ID
                    int productId = productVM.Id;

                    // Create folder for the product images if it doesn't exist
                    string productFolder = "product-" + productId.ToString();
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images", "products", productFolder);
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    // Process each uploaded image
                    foreach (var image in productImages)
                    {
                        if (image != null && image.Length > 0)
                        {
                            // Generate a unique file name for each image
                            var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                            // Save the image to the file system
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                image.CopyTo(fileStream);
                            }

                            // Save the image path to the database using ProductImageRepository
                            productImageRepository.Create(productId, uniqueFileName);
                        }
                    }
                }

                return RedirectToAction("Index");
            }

            ViewData["listOfBrands"] = brandRepository.ReadAllBrand();
            ViewData["listOfDiscounts"] = discountRepository.ReadAll();
            ViewData["listOfImages"] = productImageRepository.GetProductImagesByProductId(productVM.Id);
            return View("update", productVM);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult DeleteImage(int imageId)
        {
            // Get the product image to be deleted
            var imageToBeDeleted = productImageRepository.Get(imageId);

            // Check if the image exists
            if (imageToBeDeleted != null)
            {
                // Delete the image file from the file system
                // Check if the ImageUrl property of the product image is not null or empty
                if (!string.IsNullOrEmpty(imageToBeDeleted.ImageUrl))
                {
                    // Combine the web root path with the image URL to get the full file path on the server
                    var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, imageToBeDeleted.ImageUrl.TrimStart('\\'));

                    // Check if the image file exists at the specified file path
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        // If the image file exists, delete it from the file system
                        System.IO.File.Delete(oldImagePath);
                    }
                }


                // Remove the image from the database
                productImageRepository.Delete(imageId);

                TempData["success"] = "Deleted successfully";
            }

            // Redirect to the Update action of the product controller with the product ID
            return RedirectToAction(nameof(Update), new { id = imageToBeDeleted?.ProductId });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            productRepository.Delete(id);
            return RedirectToAction("Index");
        }


        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var productDetails = productRepository.ReadById(id);
            if (productDetails == null)
            {
                return View("NotFound");
            }
            ViewData["listOfBrands"] = brandRepository.ReadAllBrand();
            ViewData["listOfDiscounts"] = discountRepository.ReadAll();
            return View("Details", productDetails);
        }


        [HttpPost]
        [AllowAnonymous]
        public IActionResult Filter(string? brandName = null, decimal? minPrice = null, decimal? maxPrice = null, int? rating = null)
        {
            var filteredResults = productRepository.Filter(brandName, minPrice, maxPrice, rating);
            return View(filteredResults);
        }


        [Authorize]
        public IActionResult AddComment(int productId)
        {
            var reviewVM = new ReviewViewModel
            {
                ProductId = productId
            };

            return View(reviewVM);
        }

        // POST: Product/AddComment
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult AddComment(ReviewViewModel reviewVM)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var review = new Review
                {
                    ProductId = reviewVM.ProductId,
                    ApplicationUserId = userId,
                    Rating = reviewVM.Rating,
                    Comment = reviewVM.Comment,
                    CreatedAt = DateTime.Now,
                };

                reviewRepository.Create(review);
                productRepository.RecalculateAverageRating(reviewVM.ProductId);

                return RedirectToAction("Details", new { id = reviewVM.ProductId });
            }
            return View(reviewVM);
        }

        
    }

}
