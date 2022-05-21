using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using hotel_santa_ursula_II.Models;

namespace hotel_santa_ursula_II.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<hotel_santa_ursula_II.Models.TipoHabitacion> Tipo_hab { get; set; }
        public DbSet<hotel_santa_ursula_II.Models.Habitaciones> habitaciones { get; set; }
        public DbSet<hotel_santa_ursula_II.Models.Usuario> usuario { get; set; }
          public DbSet<hotel_santa_ursula_II.Models.Reservas> DataReservas { get; set; }
        public DbSet<hotel_santa_ursula_II.Models.Pago> DataPago { get; set; }
        public DbSet<hotel_santa_ursula_II.Models.Pedido> DataPedido { get; set; }
        public DbSet<hotel_santa_ursula_II.Models.Detallepedido> DataDetallepedido { get; set; }

        public DbSet<hotel_santa_ursula_II.Models.Reclamaciones> reclamo { get; set; }
        public DbSet<hotel_santa_ursula_II.Models.Carrito> DataProforma { get; set; }
        public DbSet<hotel_santa_ursula_II.Models.Usuario> listausuarios { get; set; }
        public DbSet<hotel_santa_ursula_II.Models.Devolucion> devol { get; set; }


    }
}
