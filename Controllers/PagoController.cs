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
using OfficeOpenXml;
using OfficeOpenXml.Table;
using Rotativa.AspNetCore;

namespace hotel_santa_ursula_II.Controllers
{
    public class PagoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PagoController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Listar()
        {
            var lista = _context.DataPago.ToList();
            return View(lista);
        }
        public IActionResult Create(int monto)
        {
            Pago pago = new Pago();
            pago.UserID = _userManager.GetUserName(User);
            pago.MontoTotal = monto;
            return View(pago);
        }

        [HttpPost]
        public IActionResult Pagar(Pago pago)
        {
            int a;

            pago.FechaPago = DateTime.Now;
            _context.Add(pago);

            var itemsReserva = from o in _context.DataProforma select o;
            itemsReserva = itemsReserva.
                Include(p => p.habitacion).
                Where(s => s.UserID.Equals(pago.UserID) && s.Status.Equals("PENDIENTE"));



            Pedido pedido = new Pedido();
            pedido.UserID = pago.UserID;
            pedido.Total = pago.MontoTotal;
            pedido.pago = pago;

            pedido.Estado = "PENDIENTE";
            _context.Add(pedido);


            List<Detallepedido> itemsPedido = new List<Detallepedido>();
            foreach (var item in itemsReserva.ToList())
            {
                Detallepedido detallePedido = new Detallepedido();
                detallePedido.pedido = pedido;
                detallePedido.Precio = Convert.ToInt32(item.Precio);
                detallePedido.Habitaciones = item.habitacion;
                a = item.habitacion.id;
                detallePedido.Cantidad = item.C_noches;
                itemsPedido.Add(detallePedido);
                var hab = from o in _context.habitaciones select o;
                hab = hab.Where(n => n.id.Equals(a));
                foreach (Models.Habitaciones j in hab.ToList())
                {

                }
                _context.UpdateRange(hab);
                _context.SaveChanges();
            }

            _context.AddRange(itemsPedido);

            foreach (Carrito p in itemsReserva.ToList())
            {
                p.Status = "PROCESADO";
            }
            _context.RemoveRange(itemsReserva);

            _context.SaveChanges();
            /*******/
            /*var resultado=_context.DataProforma.Where(x=>x.Status=="PROCESADO").ToList();
            _context.DataProforma.Remove(resultado);
            _context.SaveChanges();*/
            /*******/
            ViewData["Message"] = "El pago se ha registrado";
            return View("Metodo");
        }

        public IActionResult ExportarExcel()
        {
            string excelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var pagos = _context.DataPago.AsNoTracking().ToList();
            using (var libro = new ExcelPackage())
            {
                var worksheet = libro.Workbook.Worksheets.Add("Pagos");
                worksheet.Cells["A1"].LoadFromCollection(pagos, PrintHeaders: true);
                for (var col = 1; col < pagos.Count + 1; col++)
                {
                    worksheet.Column(col).AutoFit();
                }
                // Agregar formato de tabla
                var tabla = worksheet.Tables.Add(new ExcelAddressBase(fromRow: 1, fromCol: 1, toRow: pagos.Count + 1, toColumn: 2), "Pagos");
                tabla.ShowHeader = true;
                tabla.TableStyle = TableStyles.Light6;
                tabla.ShowTotal = true;

                return File(libro.GetAsByteArray(), excelContentType, "Pagos.xlsx");
            }
        }
         public async Task<IActionResult> Documento()
        {
           // return View(await _context.Documento.ToListAsync());
            var userID = _userManager.GetUserName(User);
            var items = from o in _context.DataProforma select o;
            items = items.
                Include(p => p.habitacion).
                Where(s => s.UserID.Equals(userID));
            
           return new ViewAsPdf("Documento",await items.ToListAsync());
        }


    }
}