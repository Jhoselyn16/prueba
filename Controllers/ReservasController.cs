using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hotel_santa_ursula_II.Data;
using hotel_santa_ursula_II.Models;
using Microsoft.AspNetCore.Identity;
using System.Dynamic;

namespace hotel_santa_ursula_II.Controllers
{
    public class ReservasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ReservasController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
               // GET: Proforma
        public async Task<IActionResult> Index()
        {
            var userID = _userManager.GetUserName(User);
            var items = from o in _context.DataReservas select o;
            items = items.
                Include(r => r.Habitaciones).
                Where(s => s.UserID.Equals(userID) && s.Estado.Equals("PENDIENTE"));

            var elements = await items.ToListAsync();
            //conversion de fechas a numeros e integer
            int numer = Convert.ToInt32( elements.Sum(c => c.DiaSalida.Day - c.DiaEntrada.Day ));
            var total = elements.Sum(c => numer * c.Precio );
            
            dynamic model = new ExpandoObject();
            model.montoTotal = total;
            model.reservas = elements;
            

            return View(model);
        }

        // GET: Proforma/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.DataReservas.FindAsync(id);
            _context.DataReservas.Remove(reserva);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        
        // GET: Proforma/Edit/5
        public async Task<IActionResult> Seleccionar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.habitaciones.FindAsync(id);
            if (reserva== null)
            {
                return NotFound();
            }
            return View(reserva);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Seleccionar(int id, [Bind("Id,CantHuespedes,DiaEntrada,DiaSalida,Precio,UserID")] Reservas reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Id))
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
            return View(reserva);
        }
        private bool ReservaExists(int id)
        {
            return _context.DataReservas.Any(e => e.Id == id);
        }

    }
}