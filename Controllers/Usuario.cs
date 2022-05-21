using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using hotel_santa_ursula_II.Data;
using hotel_santa_ursula_II.Models;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace hotel_santa_ursula_II.Controllers
{
    public class Usuario : Controller
    {
        private readonly ILogger<Usuario> _logger;
        private readonly ApplicationDbContext _context;

        public Usuario(ILogger<Usuario> logger, ApplicationDbContext context)
        {
            _logger = logger;

            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    

        [HttpPost]
        public IActionResult Registrar(Models.Usuario objMuestra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(objMuestra);
                _context.SaveChanges();
                return RedirectToAction("ListarUsuario");

            }
            
           ViewData["Message"] = "El cliente ha sido registrado";
            return View("ListarUsuario");
        }

        public IActionResult ListarUsuario()
        {
             var lista = _context.listausuarios.ToList();
            return View(lista);
            // return View();
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Usu = await _context.usuario.FindAsync(id);
            if (Usu == null)
            {
                return NotFound();
            }
            return View(Usu);
        }

         [HttpPost]
        [ValidateAntiForgeryToken]
       
      
        public IActionResult Eliminar(int? id)
        {
            var idusu = _context.usuario.Find(id);
            _context.usuario.Remove(idusu);
            _context.SaveChanges();
            return RedirectToAction("ListarUsuario");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("id,nombres,apellidos,dni,direccion,telefono,correo")] Models.Usuario usua)
        {
            if (id != usua.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usua);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction("ListarUsuario");
            }
            return RedirectToAction("ListarUsuario");
        }

        public IActionResult ExportarExcel()
            {
                string excelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var usuarios = _context.usuario.AsNoTracking().ToList();
                using (var libro = new ExcelPackage())
                    {
                        var worksheet = libro.Workbook.Worksheets.Add("Usuarios");
                        worksheet.Cells["A1"].LoadFromCollection(usuarios, PrintHeaders: true);
                        for (var col = 1; col < usuarios.Count + 1; col++)
                            {
                                worksheet.Column(col).AutoFit();
                            }
        // Agregar formato de tabla
        var tabla = worksheet.Tables.Add(new ExcelAddressBase(fromRow: 1, fromCol: 1, toRow: usuarios.Count + 1, toColumn: 5), "Usuarios");
        tabla.ShowHeader = true;
        tabla.TableStyle = TableStyles.Light6;
        tabla.ShowTotal = true;

        return File(libro.GetAsByteArray(), excelContentType, "Usuarios.xlsx");
    }
    }
   }
}