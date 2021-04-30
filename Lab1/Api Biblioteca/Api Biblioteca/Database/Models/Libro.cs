using System;
using System.Collections.Generic;

namespace Api_Biblioteca.Database.Models
{
    public class Libro
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public int Cantidad { get; set; }

        public int IdAutor { get; set; }

        public Autor Autor { get; set; }

        public ICollection<HistorialMovimiento> Movimientos { get; set; } = new HashSet<HistorialMovimiento>();
    }
}