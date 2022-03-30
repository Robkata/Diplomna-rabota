using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExpressTaxi.Domain;

namespace ExpressTaxi.Abstractions
{
    public interface IBrandService
    {
        List<Brand> GetBrands();
        Brand GetBrandById(int brandId);
        List<Taxi> GetTaxiesByBrand(int brandId);
    }
}
