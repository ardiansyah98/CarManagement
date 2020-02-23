using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarManagement.Data;
using CarManagement.Models;
using CarManagement.Constants;
using Newtonsoft.Json;

namespace CarManagement.Controllers
{
    public class CarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Car.ToListAsync());
        }

        public async Task<IActionResult> Detail(string szCarId)
        {
            if (szCarId == null)
                return NotFound();

            var car = await _context.Car.FirstOrDefaultAsync(m => m.szCarId == szCarId);
            if (car == null)
                return NotFound();

            return View(car);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("szCarId,szCarName,szCarModel,dtmCreated,dtmUpdated")] Car car)
        {
            if (ModelState.IsValid)
            {
                DateTime dtmNow = DateTime.Now;
                car.dtmCreated = dtmNow;
                car.dtmUpdated = dtmNow;
                _context.Add(car);
                CreateLog(car, dtmNow, "Create Car");
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        public async Task<IActionResult> Edit(string szCarId)
        {
            if (szCarId == null)
                return NotFound();

            var car = await _context.Car.FindAsync(szCarId);
            if (car == null)
                return NotFound();

            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string szCarId, [Bind("szCarId,szCarName,szCarModel,dtmCreated,dtmUpdated")] Car car)
        {
            if (szCarId != car.szCarId)
                return NotFound();
            
            if (ModelState.IsValid)
            {
                try
                {
                    DateTime dtmNow = DateTime.Now;
                    var prev_car = await _context.Car.FirstOrDefaultAsync(m => m.szCarId == szCarId);
                    _context.Entry(prev_car).State = EntityState.Detached;
                    car.dtmCreated = prev_car.dtmCreated;
                    car.dtmUpdated = dtmNow; 
                    _context.Update(car); 
                    CreateLog(car, dtmNow, "Update Car");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Exists(car.szCarId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        private void CreateLog(Car car, DateTime dtmNow, string szAction)
        {
            Log log = new Log
            {
                gdHistoryId = Guid.NewGuid(),
                dtmCreated = dtmNow,
                szAction = szAction,
                szUri = CarConstants.URI,
                szEmail = "dummy@email.com",
                szData = JsonConvert.SerializeObject(car)
            };
            _context.Log.Add(log); 
        }

        public async Task<IActionResult> Delete(string szCarId)
        {
            if (szCarId == null)
                return NotFound();

            var car = await _context.Car
                .FirstOrDefaultAsync(m => m.szCarId == szCarId);
            if (car == null)
                return NotFound();

            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string szCarId)
        {
            var car = await _context.Car.FindAsync(szCarId);
            _context.Car.Remove(car);
            CreateLog(car, DateTime.Now, "Delete Car");
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Exists(string szCarId)
        {
            return _context.Car.Any(e => e.szCarId == szCarId);
        }
    }
}
