using Api_Biblioteca.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Biblioteca.Database.Context
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options)
            : base(options)
        {
        }

        public DbSet<Autor> Autors { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<HistorialMovimiento> HistorialMovimientos { get; set; }
    }
}
