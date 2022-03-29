using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExpressTaxi.Domain;

namespace ExpressTaxi.Abstractions
{
    public interface IOptionService
    {
        List<Brand> GetOptions();
        Brand GetOptionById(int optionId);
        List<Taxi> GetReservationByOption(int optionId);
    }
}