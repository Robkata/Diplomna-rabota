using ExpressTaxi.Abstractions;
using ExpressTaxi.Data;
using ExpressTaxi.Domain;
using ExpressTaxi.Models.Taxi;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExpressTaxi.Services
{
    public class TaxiService : ITaxiService
    {
        private readonly ApplicationDbContext _context;

        public TaxiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(TaxiCreateVM model, string imagePath)
        {
            var extension = Path.GetExtension(model.Image.FileName).TrimStart('.');
            var taxi = new Taxi
            {
                BrandId = model.BrandId,
                Engine = model.Engine,
                Extras = model.Extras,
                Year = model.Year,
                DriverId = model.DriverId
            };

            var dbImage = new Image()
            {
                Taxi = taxi,
                Extension = extension
            };

            taxi.ImageId = dbImage.Id;

            Directory.CreateDirectory($"{imagePath}/images/");
            var physicalPath = $"{ imagePath}/images/{dbImage.Id}.{ extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await model.Image.CopyToAsync(fileStream);

            await this._context.Images.AddAsync(dbImage);
            await this._context.Taxies.AddAsync(taxi);
            await this._context.SaveChangesAsync();
        }

        public Taxi GetTaxiById(int taxiId)
        {
            return _context.Taxies.Find(taxiId);
        }

        public List<Taxi> GetTaxies(string searchStringExtras, string searchStringEngine)
        {
            List<Taxi> taxies = _context.Taxies.ToList();
            if (!String.IsNullOrEmpty(searchStringExtras) && !String.IsNullOrEmpty(searchStringEngine))
            {
                taxies = taxies.Where(d => d.Extras.Contains(searchStringExtras) && d.Engine.Contains(searchStringEngine)).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringExtras))
            {
                taxies = taxies.Where(d => d.Extras.Contains(searchStringExtras)).ToList();
            }
            else if (!string.IsNullOrEmpty(searchStringEngine))
            {
                taxies = taxies.Where(d => d.Engine.Contains(searchStringEngine)).ToList();
            }
            return taxies;
        }

        public bool RemoveById(int taxiId)
        {
            {
                var taxi = GetTaxiById(taxiId);
                if (taxi == default(Taxi))
                {
                    return false;
                }
                _context.Remove(taxi);
                return _context.SaveChanges() != 0;
            }
        }

        public bool UpdateTaxi(int taxiId, int brandId, string imageId, string engine, string extras, int driverId)
        {
            var taxi = GetTaxiById(taxiId);
            if (taxi == default(Taxi))
            {
                return false;
            }
            taxi.BrandId = brandId;
            taxi.ImageId = imageId;
            taxi.Engine = engine;
            taxi.Extras = extras;
            taxi.DriverId = driverId;
            _context.Update(taxi);
            return _context.SaveChanges() != 0;
        }

        public List<TaxiAllVM> GetTaxies()
        {
            List<TaxiAllVM> taxies = _context.Taxies
                .Select(d => new TaxiAllVM
                {
                    Id = d.Id,
                    BrandId = d.BrandId,
                    Brand = d.Brand.Name,
                    Engine = d.Engine,
                    Extras = d.Extras,
                    Year = d.Year.ToString("dd-mm-yyyy hh:mm", CultureInfo.InvariantCulture),
                    DriverId = d.DriverId,
                    Driver = d.Driver.FirstName,
                    ImageUrl = $"/images/{d.ImageId}.{d.Image.Extension}",
                }).ToList();

            return taxies;
        }
    }
}
