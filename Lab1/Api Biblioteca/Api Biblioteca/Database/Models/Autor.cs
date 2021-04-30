using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Biblioteca.Database.Models
{
    public class Autor
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int Edad { get; set; }

        public ICollection<Libro> Libros { get; set; } = new HashSet<Libro>();
    }
}
