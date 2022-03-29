using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpressTaxi.Abstractions;

using ExpressTaxi.Domain;
using ExpressTaxi.Models.Brand;
using ExpressTaxi.Models.Taxi;
using ExpressTaxi.Models.Option;

namespace ExpressTaxi.Controllers

{
    public class TaxiController : Controller
    {
        private readonly ITaxiService _taxiService;
        private readonly IBrandService _brandService;
        private readonly IOptionService _optionService;
        private readonly IWebHostEnvironment _hostEnvironment;


        public TaxiController(ITaxiService taxiService, IBrandService brandService, IOptionService optionService, IWebHostEnvironment hostEnvironment)
        {
            this._taxiService = taxiService;
            this._brandService = brandService;
            this._hostEnvironment = hostEnvironment;
            this._optionService = optionService;

        }
        // GET: ProductsController
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            var product = new TaxiCreateVM();

            product.Brands = _brandService.GetBrands()
             .Select(c => new BrandChoiceVM()
             {
                 Name = c.Name,
                 Id = c.Id,
             })
        .ToList();
            product.Options = _optionService.GetOptions()
             .Select(c => new OptionPairVM()
             {
                 Name = c.Name,
                 Id = c.Id,
             })
        .ToList();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create([FromForm] TaxiCreateVM input)
        {
            var imagePath = $"{this._hostEnvironment.WebRootPath}";
            if (!ModelState.IsValid)
            {
                input.Brands = _brandService.GetBrands()
                    .Select(c => new BrandChoiceVM()
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .ToList();
                input.Options = _optionService.GetOptions()
                    .Select(c => new OptionPairVM()
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .ToList();
                return View(input);
            }
            await this._taxiService.Create(input, imagePath);
            return RedirectToAction(nameof(All));
        }

        public ActionResult Success()
        {
            return this.View();

        }

        public IActionResult Edit(int id)
        {
            Taxi item = _taxiService.GetTaxiById(id);
            if (item == null)
            {
                return NotFound();
            }

            TaxiCreateVM taxi = new TaxiCreateVM()
            {
                Id = item.Id,
                BrandId = item.BrandId,
                Engine = item.Engine,
                Extras = item.Extras,
                Year = item.Year,
                DriverId = item.DriverId
            };
            return View(taxi);
        }

        [HttpPost]
        public IActionResult Edit(int taxiId, TaxiEditVM bindingModel)
        {
            if (ModelState.IsValid)
            {
                var updated = _taxiService.UpdateTaxi(bindingModel.TaxiId, bindingModel.BrandId, bindingModel.ImageUrl, bindingModel.Engine, bindingModel.Extras, bindingModel.DriverId);
                if (updated)
                {
                    return this.RedirectToAction("All");
                }
            }
            return View(bindingModel);
        }

        public IActionResult Delete(int id)
        {

            Taxi item = _taxiService.GetTaxiById(id);
            if (item == null)
            {
                return NotFound();
            }
            TaxiCreateVM taxi = new TaxiCreateVM()

            {
                Id = item.Id,
                BrandId = item.BrandId,
                Engine = item.Engine,
                Extras = item.Extras,
                Year = item.Year,
                DriverId = item.DriverId
            };
            return View(taxi);
        }
        [HttpPost]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            var deleted = _taxiService.RemoveById(id);
            if (deleted)
            {
                return this.RedirectToAction("All");
            }
            else
            {
                return View();
            }
        }
        [AllowAnonymous]
        public ActionResult All(string searchStringExtras, string searchStringEngine)
        {
            //List<ProductAllVM> products = _productService.GetProducts(searchStringModel, searchStringDescription)
            //.Select(productFromDb => new ProductAllVM
            //{
            //    Id = productFromDb.Id,
            //    CategoryId = productFromDb.CategoryId,
            //    CategoryName = productFromDb.Category.Name,
            //    Model = productFromDb.Model,
            //    BrandId = productFromDb.BrandId,
            //    BrandName = productFromDb.Brand.Name,
            //    Description = productFromDb.Description,
            //    ImageUrl=productFromDb.ImageUrl,
            //    Price = productFromDb.Price,
            //    Quantity = productFromDb.Quantity,
            //    Discount = productFromDb.Discount
            //}).ToList();

            var taxies = _taxiService.GetTaxies();
            return this.View(taxies);
        }
    }
}




