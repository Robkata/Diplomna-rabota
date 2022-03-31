using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpressTaxi.Data;
using ExpressTaxi.Domain;

namespace ExpressTaxi.Controllers
{
    public class Taxi2Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public Taxi2Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Taxi2
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Taxies.Include(t => t.Brand).Include(t => t.Driver).Include(t => t.Image);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Taxi2/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxi = await _context.Taxies
                .Include(t => t.Brand)
                .Include(t => t.Driver)
                .Include(t => t.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taxi == null)
            {
                return NotFound();
            }

            return View(taxi);
        }

        // GET: Taxi2/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id");
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Id");
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id");
            return View();
        }

        // POST: Taxi2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TaxiId,BrandId,ImageId,Engine,Extras,Year,DriverId")] Taxi taxi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taxi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", taxi.BrandId);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Id", taxi.DriverId);
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id", taxi.ImageId);
            return View(taxi);
        }

        // GET: Taxi2/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxi = await _context.Taxies.FindAsync(id);
            if (taxi == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", taxi.BrandId);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Id", taxi.DriverId);
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id", taxi.ImageId);
            return View(taxi);
        }

        // POST: Taxi2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TaxiId,BrandId,ImageId,Engine,Extras,Year,DriverId")] Taxi taxi)
        {
            if (id != taxi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taxi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaxiExists(taxi.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", taxi.BrandId);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Id", taxi.DriverId);
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id", taxi.ImageId);
            return View(taxi);
        }

        // GET: Taxi2/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxi = await _context.Taxies
                .Include(t => t.Brand)
                .Include(t => t.Driver)
                .Include(t => t.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taxi == null)
            {
                return NotFound();
            }

            return View(taxi);
        }

        // POST: Taxi2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taxi = await _context.Taxies.FindAsync(id);
            _context.Taxies.Remove(taxi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaxiExists(int id)
        {
            return _context.Taxies.Any(e => e.Id == id);
        }
    }
}
