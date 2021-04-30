using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Biblioteca.Models
{
    public class LibroDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public int Cantidad { get; set; }

        public int IdAutor { get; set; }
    }
}
