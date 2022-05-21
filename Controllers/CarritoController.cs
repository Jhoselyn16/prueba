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
using Rotativa.AspNetCore;

namespace hotel_santa_ursula_II.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CarritoController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
               // GET: Proforma
        public async Task<IActionResult> Index()
        {
            var userID = _userManager.GetUserName(User);
            var carrito = from o in _context.DataProforma select o;
            carrito = carrito.
                Include(p => p.habitacion).
                Where(s => s.UserID.Equals(userID));

                //conversion de fechas a numeros e integer
                var elements = await carrito.ToListAsync();
                int numer = Convert.ToInt32( elements.Sum(c => c.fechar.Day));
                //--------------------------------------------
            
            return View(await carrito.ToListAsync());
        }
       /*  public async Task<IActionResult> ContactPDF()
        {
            int a =10;
            var norma = _context.DataPago.ToList();
            foreach(var item in norma.ToList()){
                a=item.Id;
            }
            var userID = _userManager.GetUserName(User);
            var Impresion = from o in _context.DataDetallepedido select o;
            Impresion = Impresion.
                Include(p => p.Habitaciones  ).
                Include(y=>y.pedido).
                Include(y=>y.pedido.pago).
                Where(s => s.pedido.pago.Id.Equals(a));
           return new ViewAsPdf("ContactPDF", await Impresion.ToListAsync());
            {

            }
        }*/
        
        /****************************************************************************************************/

        // GET: Proforma/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var proforma = await _context.DataProforma.FindAsync(id);
            
            _context.DataProforma.Remove(proforma);
            await _context.SaveChangesAsync();
            
           
            return RedirectToAction(nameof(Index));
        }
        
       public IActionResult Editar(int id) {
            var region = _context.DataProforma.Find(id);
            return View(region);
        }

        [HttpPost]
        public IActionResult Editar(Carrito r) {
            if (ModelState.IsValid) {
                var region = _context.DataProforma.Find(r.Id);
                /*region.Quantity = r.Quantity;*/
                /*region.Precio=r.Precio;*/
                region.C_noches=r.C_noches;
                region.fechar=r.fechar;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(r);
        }

        public IActionResult EditarConfirmacion() {
            return View();
        }
         [HttpPost]
        public async Task<IActionResult> Delete(int id) {
          
           var userID = _userManager.GetUserName(User);
            var car = await _context.DataProforma.FindAsync(id);
            var itemsReserva = from o in _context.DataProforma select o;
            itemsReserva = itemsReserva.
                Include(p => p.habitacion).
                Where(s => s.Id.Equals(id));
             foreach (Carrito p in itemsReserva.ToList())
            {
                p.habitacion.Estado="Disponible";
            }
            _context.UpdateRange(itemsReserva);

            _context.SaveChanges();
            
            _context.Remove(car);
            _context.SaveChanges();
            

            return View("Carrito");
        }

        



    }
}
