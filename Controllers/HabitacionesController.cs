using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using hotel_santa_ursula_II.Models;
using hotel_santa_ursula_II.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace hotel_santa_ursula_II.Controllers
{
    public class HabitacionesController : Controller
    {
         private readonly ILogger<HabitacionesController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public HabitacionesController(ILogger<HabitacionesController> logger,
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
       
        public async Task<IActionResult> Mostrar()
        {
            var productos = from o in _context.habitaciones select o;
            productos = productos.Where(s => s.Estado.Equals("Disponible"));
            return View(await productos.ToListAsync());
        }
        public async Task<IActionResult> Detalles(int? id)
        {
            Models.Habitaciones objProduct = await _context.habitaciones.FindAsync(id);
            if(objProduct == null){
                return NotFound();
            }
            return View(objProduct);
        }

        public async Task<IActionResult> Seleccionar(int? id)
        {
               var userID = _userManager.GetUserName(User);
            if(userID == null){
                ViewData["Message"] = "DEBE INICIAR SESIÓN PARA PODER REALIZAR SU RESERVA";
                List<Habitaciones> productos = new List<Habitaciones>();
                return  View("Mostrar",productos);
            }else{
                var producto = await _context.habitaciones.FindAsync(id);
                Carrito proforma = new Carrito();
                proforma.habitacion = producto;// revisar exactamente esto
                proforma.Precio = producto.precio;
                proforma.Quantity = 1;
                proforma.UserID = userID;
                proforma.habitacion.Estado="Ocupado";
                _context.Add(proforma);
                await _context.SaveChangesAsync();
                return  RedirectToAction(nameof(Mostrar));
            }
        }
         /*************************************** LISTAR HABITACION ******************************************/
        public IActionResult Listar()
        {
            var lista = _context.habitaciones.ToList();
            return View(lista);
           
            // return View();
        }
         /*************************************** EDITAR HABITACION ******************************************/
         public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtiphab = await _context.habitaciones.FindAsync(id);
            if (vtiphab == null)
            {
                return NotFound();
            }
            return View(vtiphab);
        }
         /*************************************** DETALLE HABITACION ******************************************/
         public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vtiphab = await _context.habitaciones.FindAsync(id);
            if (vtiphab == null)
            {
                return NotFound();
            }
            return  RedirectToAction("Seleccionar","Reservas", new {id});
        }

/*************************************** EDITAR HABITACION ******************************************/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("id,idtipo,numero,precio,descripcion,nivel,Estado,Imagen,tipoHabitacion")] Models.Habitaciones Hab)
        {
            if (id != Hab.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Hab);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction("Listar");
            }
            return RedirectToAction("Listar");
        }

/*************************************** REGISTRAR HABITACION ******************************************/
        [HttpPost]
        public IActionResult Registrar(Models.Habitaciones objMuestra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(objMuestra);
                _context.SaveChanges();
                return RedirectToAction("Listar");

            }
            return View("Index", objMuestra);
        }
/*************************************** MOSTRAR TUS HABITACIONES ****************************************/         
        [HttpGet]
        public async Task<IActionResult> Mostrar(String Empsearch){
            ViewData["Getemployeedetails"]=Empsearch;
            var empquery=from x in _context.habitaciones select x;
            if(!string.IsNullOrEmpty(Empsearch)){
                empquery=empquery.Where(x =>x.numero.Contains(Empsearch))  ;
            }
            return View(await empquery.AsNoTracking().ToListAsync());

        }

/***************************************** ELIMINAR UNA HABITACION ***************************************/
        public IActionResult Eliminar(int? id)
        {
            var loco = _context.habitaciones.Find(id);
            _context.habitaciones.Remove(loco);
            _context.SaveChanges();
            return RedirectToAction("ListarUsuario");
        }

}
}