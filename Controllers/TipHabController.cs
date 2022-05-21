using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using hotel_santa_ursula_II.Data;
using hotel_santa_ursula_II.Models;

namespace hotel_santa_ursula_II.Controllers
{
    public class TipHabController : Controller
    {
        private readonly ILogger<TipHabController> _logger;
        private readonly ApplicationDbContext _context;

        public TipHabController(ILogger<TipHabController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.Tipo_hab.ToList();
            return View(lista);
            // return View();
        }
        public IActionResult Crear()
        {
            // var lista = _context.Tipo_hab.ToList();
            return View();
        }


        [HttpPost]
        public IActionResult Registrar(TipoHabitacion objTipHab)
        {
            if (ModelState.IsValid)
            {
                _context.Add(objTipHab);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View("Crear", objTipHab);
        }

        public IActionResult ConfirmacionUpdate()
        {
            return View();
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtiphab = await _context.Tipo_hab.FindAsync(id);
            if (vtiphab == null)
            {
                return NotFound();
            }
            return View(vtiphab);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("id,nomtiphabitacion,desctiphab")] TipoHabitacion objTipHab)
        {
            if (id != objTipHab.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(objTipHab);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(objTipHab);
        }

        public IActionResult Eliminar(int? id)
        {
            var etiphab = _context.Tipo_hab.Find(id);
            _context.Tipo_hab.Remove(etiphab);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
}