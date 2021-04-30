using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Biblioteca.Database.Models
{
    public class HistorialMovimiento
    {
        public int Id { get; set; }

        public int IdLibro { get; set; }

        public Libro Libro { get; set; }

        public DateTime Date { get; set; }

        public TipoMovimiento TipoMovimiento { get; set; }
    }
}
