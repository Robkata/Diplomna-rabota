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
using System.Globalization;
using ExpressTaxi.Models.Driver;
using ExpressTaxi.Data;

namespace ExpressTaxi.Controllers

{
    public class TaxiController : Controller
    {
        private readonly ITaxiService _taxiService;
        private readonly IBrandService _brandService;
        private readonly IDriverService _driverService;
        private readonly IOptionService _optionService;
        private readonly IWebHostEnvironment _hostEnvironment;


        public TaxiController(ITaxiService taxiService, IBrandService brandService, IDriverService driverService, IOptionService optionService, IWebHostEnvironment hostEnvironment)
        {
            this._taxiService = taxiService;
            this._brandService = brandService;
            this._driverService = driverService;
            this._hostEnvironment = hostEnvironment;
            this._optionService = optionService;

        }

        public ActionResult Details(int id)
        {
            Taxi item = _taxiService.GetTaxiById(id);
            if (item == null)
            {
                return NotFound();
            }
            TaxiDetailsVM taxi = new TaxiDetailsVM()
            {
                Id = item.Id,
                Brand = item.Brand.Name,
                Engine = item.Engine,
                Extras = item.Extras,
                Year = item.Year,
                Driver = item.Driver.Name
            };
            return View(taxi);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            var taxi = new TaxiCreateVM();

            taxi.Brands = _brandService.GetBrands()
             .Select(c => new BrandChoiceVM()
             {
                 Name = c.Name,
                 Id = c.Id
             })
        .ToList();
            taxi.Drivers = _driverService.GetDrivers()
             .Select(d => new DriverPairVM()
             {
                 Name = d.Name,
                 Id = d.Id
             })
        .ToList();
            return View(taxi);
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
                input.Drivers = _driverService.GetDrivers()
                    .Select(d => new DriverPairVM()
                    {
                        Id = d.Id,
                        Name = d.Name
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

        /*
        public ActionResult Edit()
        {
            var taxi = new TaxiEditVM();

            taxi.Brands = _brandService.GetBrands()
             .Select(c => new BrandChoiceVM()
             {
                 Name = c.Name,
                 Id = c.Id
             })
        .ToList();
            taxi.Drivers = _driverService.GetDrivers()
             .Select(d => new DriverPairVM()
             {
                 Name = d.Name,
                 Id = d.Id
             })
        .ToList();
            return View(taxi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Edit([FromForm] TaxiEditVM input)
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
                input.Drivers = _driverService.GetDrivers()
                    .Select(d => new DriverPairVM()
                    {
                        Id = d.Id,
                        Name = d.Name
                    })
                    .ToList();
                return View(input);
            }
            await this._taxiService.UpdateTaxi(input, imagePath);
            return RedirectToAction(nameof(All));
        }
        */

        
        public IActionResult Edit(int id)
        {
            Taxi item = _taxiService.GetTaxiById(id);
            if (item == null)
            {
                return NotFound();
            }

            TaxiEditVM taxi = new TaxiEditVM()
            {
                Id = item.Id,
                BrandId = item.BrandId,
                Engine = item.Engine,
                Extras = item.Extras,
                Year = item.Year,
                DriverId = item.DriverId
            };
            taxi.Brands =_brandService.GetBrands()
               .Select(c => new BrandChoiceVM()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
            taxi.Drivers = _driverService.GetDrivers()
                .Select(d => new DriverPairVM()
                {
                    Id = d.Id,
                    Name = d.Name
                })
                .ToList();
            return View(taxi);
        }

        [HttpPost]
        public IActionResult Edit(int id, TaxiEditVM bindingModel)
        {
            if (ModelState.IsValid)
            {
                var updated = _taxiService.UpdateTaxi(id, bindingModel.BrandId, bindingModel.Engine, bindingModel.Extras, bindingModel.DriverId);
                _brandService.GetBrands()
                   .Select(c => new BrandChoiceVM()
                   {
                       Id = c.Id,
                       Name = c.Name
                   })
                   .ToList();
                _driverService.GetDrivers()
                   .Select(d => new DriverPairVM()
                   {
                       Id = d.Id,
                       Name = d.Name
                   })
                   .ToList();
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
            TaxiDeleteVM taxi = new TaxiDeleteVM()
            {
                Id = item.Id,
                Brand = item.Brand.Name,
                Engine = item.Engine,
                Extras = item.Extras,
                Year = item.Year,
                Driver = item.Driver.Name
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
            var taxies = _taxiService.GetTaxies();
            return this.View(taxies);
        }
    }
}




