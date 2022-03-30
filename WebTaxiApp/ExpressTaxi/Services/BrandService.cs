using ExpressTaxi.Abstractions;
using ExpressTaxi.Data;
using ExpressTaxi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpressTaxi.Services
{
    public class BrandService : IBrandService
    {
        private readonly ApplicationDbContext _context;

        public BrandService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Taxi> GetTaxiesByBrand(int brandId)
        {
            return _context.Taxies
                  .Where(x => x.BrandId ==
                  brandId)
              .ToList();
        }

        Brand IBrandService.GetBrandById(int brandId)
        {
            return _context.Brands.Find(brandId);
        }

        List<Brand> IBrandService.GetBrands()
        {
            List<Brand> brands = _context.Brands.ToList();
            return brands;
        }
    }
}
