using ExpressTaxi.Abstractions;
using ExpressTaxi.Data;
using ExpressTaxi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpressTaxi.Services
{
    public class DriverService : IDriverService
    {
        private readonly ApplicationDbContext _context;

        public DriverService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Driver GetDriverById(int driverId)
        {
            return _context.Drivers.Find(driverId);
        }

        public List<Driver> GetDrivers()
        {
            List<Driver> drivers = _context.Drivers.ToList();
            return drivers;
        }

        public List<Taxi> GetTaxiesByDriver(int driverId)
        {
            return _context.Taxies
                  .Where(x => x.DriverId ==
                  driverId)
              .ToList();
        }
    }
}
