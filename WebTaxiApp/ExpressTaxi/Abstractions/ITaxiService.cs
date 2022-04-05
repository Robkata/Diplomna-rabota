using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExpressTaxi.Domain;
using ExpressTaxi.Models.Taxi;

namespace ExpressTaxi.Abstractions
{
    public interface ITaxiService
    {
        Task Create(TaxiCreateVM model, string imagePath);
        // bool Create( int categoryId, string model, int brandId, string description, string image, decimal price, decimal quantity, decimal discount);

        //Task UpdateTaxi(TaxiEditVM model, string imagePath);
        bool UpdateTaxi(int taxiId, int brandId, string engine, string extras, int driverId);

        List<TaxiAllVM> GetTaxies();
        //List<ProductAllVM> GetAccessories();

        Taxi GetTaxiById(int taxiId);

        bool RemoveById(int taxiId);

        List<Taxi> GetTaxies(string searchStringExtras, string searchStringEngine);
        //List<Product> GetAccessories(string searchStringModel, string searchStringDescription);

    }
}
