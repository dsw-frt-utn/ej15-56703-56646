using Dsw2026Ej15.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor que recibe las opciones (como la cadena de conexión)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Estas van a ser tus tablas en la base de datos SQL Server
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
    }
}
