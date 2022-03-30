using ExpressTaxi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpressTaxi.Abstractions
{
    public interface IDriverService
    {
        List<Driver> GetDrivers();
        Driver GetDriverById(int driverId);
        List<Taxi> GetTaxiesByDriver(int driverId);
    }
}
