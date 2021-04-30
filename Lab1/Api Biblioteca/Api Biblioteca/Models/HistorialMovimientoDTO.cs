using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Biblioteca.Models
{
    public class HistorialMovimientoDTO
    {
        public int Id { get; set; }

        public string NombreLibro { get; set; }

        public DateTime Date { get; set; }

        public string TipoMovimiento { get; set; }
    }
}
