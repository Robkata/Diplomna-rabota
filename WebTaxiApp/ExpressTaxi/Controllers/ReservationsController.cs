using ExpressTaxi.Abstractions;
using ExpressTaxi.Data;
using ExpressTaxi.Domain;
using ExpressTaxi.Models.Option;
using ExpressTaxi.Models.Reservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExpressTaxi.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly IOptionService _optionService;


        public ReservationsController(IOptionService optionService)
        {
            this._optionService = optionService;
        }

        private readonly ApplicationDbContext context;

        public ReservationsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult Create(ReservationCreateBindingModel bindingModel)
        {

            if (this.ModelState.IsValid)
            {
                string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var user = this.context.Users.SingleOrDefault(u => u.Id == currentUserId);
                var ev = this.context.Taxies.SingleOrDefault(e => e.Id == bindingModel.TaxiId);
                if (user == null || ev == null /*|| ev.TotalTickets < bindingModel.TicketsCount*/)
                {
                    // ако потребителят не съществува или събитието не съществува или няма достатъчно билети
                    return this.RedirectToAction("All", "Events");
                }

                Reservation reservationForDb = new Reservation //създаваме нова поръчка
                {
                    TaxiId = bindingModel.TaxiId,
                    Destination = bindingModel.Destination,
                    Start = DateTime.UtcNow,
                    End = bindingModel.End,
                    Passengers = bindingModel.Passengers,
                    OptionId = bindingModel.OptionId,
                    Status = bindingModel.Status
                };

                reservationForDb.Status = "Reserved";
                bindingModel.Options = _optionService.GetOptions()
                  .Select(c => new OptionPairVM()
                  {
                      Id = c.Id,
                      Name = c.Name
                  })
                  .ToList();
                //ev.TotalTickets -= bindingModel.TicketsCount; //намаляваме броя на билетите

                this.context.Taxies.Update(ev);
                this.context.Reservations.Add(reservationForDb);
                this.context.SaveChanges();
            }
            return this.RedirectToAction("My", "Reservations");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            List<ReservationListingViewModel> reservations = this.context.Reservations
               .Select(o => new ReservationListingViewModel
               {
                    //   Id = o.Id,
                    //   EventId = o.EventId,
                    //EventName = o.Event.Name,
                    //  EventStart = o.Event.Start.ToString("dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                    //  EventEnd = o.Event.End.ToString("dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                    //  EventPlace = o.Event.Place,
                    //  CustomerId = o.CustomerId,
                   CustomerUsername = o.TaxiUser.UserName,
                   //Option = o.Option.Name,
                   Start = o.Start.ToString("dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                    //да се преработи да извежда по нашата часова зона
                })
               .ToList();
            return this.View(reservations);
        }
    }
}
