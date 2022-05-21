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
    public class ReclamacionesController : Controller
    {
        private readonly ILogger<ReclamacionesController> _logger;
        private readonly ApplicationDbContext _context;

        public ReclamacionesController(ILogger<ReclamacionesController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;

        }

        public IActionResult Index()
        {
            var lista = _context.reclamo.ToList();
            return View(lista);
            // return View();
        }
        public IActionResult Crear()
        {
            // var lista = _context.Tipo_hab.ToList();
            return View();
        }

/********************************REGISTRAR RECLAMO*******************************/
        [HttpPost]
        public IActionResult Registrar(Reclamaciones objreclamacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(objreclamacion);
                _context.SaveChanges();
                return View("Crear");

            }
            return View("Crear", objreclamacion);
        }
/********************************CONFIRMAR RECLAMO*******************************/
        public IActionResult ConfirmacionUpdate()
        {
            return View();
        }
/********************************EDITAR RECLAMO*******************************/
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vreclamacion = await _context.reclamo.FindAsync(id);
            if (vreclamacion == null)
            {
                return NotFound();
            }
            return View(vreclamacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("id,Dni,Nombres,Apellido,N_celular,Correo,Reclamo")] Reclamaciones objreclamacion)
        {
            if (id != objreclamacion.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(objreclamacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(objreclamacion);
        }
/********************************ELIMINAR RECLAMO*******************************/
        public IActionResult Eliminar(int? id)
        {
            var Ereclamacion = _context.reclamo.Find(id);
            _context.reclamo.Remove(Ereclamacion);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
}