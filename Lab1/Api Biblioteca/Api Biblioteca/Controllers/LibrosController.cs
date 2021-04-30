using Api_Biblioteca.Database.Context;
using Api_Biblioteca.Database.Models;
using Api_Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Biblioteca.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibrosController : ControllerBase
    {
        private readonly BibliotecaContext bibliotecaContext;

        public LibrosController(BibliotecaContext bibliotecaContext)
        {
            this.bibliotecaContext = bibliotecaContext;
        }


        [HttpGet("HistorialMovimientos")]
        public async Task<IActionResult> HistorialMovimientos()
        {
            return this.Ok(this.bibliotecaContext.HistorialMovimientos);
        }


        [HttpPost()]
        public async Task<IActionResult> AddNewAsync(LibroDTO libroDTO)
        {
            try
            {

                var exist = this.bibliotecaContext.Libros.FirstOrDefault(a => a.Nombre == libroDTO.Nombre && a.IdAutor == libroDTO.IdAutor);
                if (exist != null)
                {
                    return this.BadRequest(new { error = $"Ya existe un libro con el nombre {libroDTO.Nombre}" });
                }
                var autor = this.bibliotecaContext.Autors.FirstOrDefault(p => p.Id == libroDTO.IdAutor);
                if (autor == null)
                {
                    return this.BadRequest(new { Error = $"No se encontro un autor con el id [{libroDTO.IdAutor}]" });
                }

                await this.bibliotecaContext.Libros.AddAsync(new Libro
                {
                    IdAutor = libroDTO.IdAutor,
                    Cantidad = libroDTO.Cantidad,
                    FechaPublicacion = libroDTO.FechaPublicacion,
                    Nombre = libroDTO.Nombre,
                });

                await this.bibliotecaContext.SaveChangesAsync();
                return this.Ok();
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }

        [HttpGet("Prestar/{idLibro}")]
        public async Task<IActionResult> Prestar(int idLibro)
        {
            var libro = this.bibliotecaContext.Libros.FirstOrDefault(l => l.Id == idLibro);
            if (libro == null)
            {
                return this.BadRequest(new { Error = $"No se encontro un libro con el id [{idLibro}]" });
            }

            if (libro.Cantidad <= 3)
            {
                return this.NotFound(new { Error = $"No hay suficientes copias del libro {libro.Nombre}" });
            }

            libro.Cantidad -= 1;
            this.bibliotecaContext.Libros.Update(libro);

            this.bibliotecaContext.HistorialMovimientos.Add(new HistorialMovimiento
            {
                IdLibro = libro.Id,
                Date = DateTime.Now,
                TipoMovimiento = TipoMovimiento.Prestar,
            });

            await this.bibliotecaContext.SaveChangesAsync();
            return this.Ok();
        }

        [HttpGet("Retornar/{idLibro}")]
        public async Task<IActionResult> Retornar(int idLibro)
        {
            var libro = this.bibliotecaContext.Libros.FirstOrDefault(l => l.Id == idLibro);
            if (libro == null)
            {
                return this.BadRequest(new { Error = $"No se encontro un libro con el id [{idLibro}]" });
            }

            libro.Cantidad += 1;
            this.bibliotecaContext.Libros.Update(libro);

            this.bibliotecaContext.HistorialMovimientos.Add(new HistorialMovimiento
            {
                IdLibro = libro.Id,
                Date = DateTime.Now,
                TipoMovimiento = TipoMovimiento.Retornar,
            });
            await this.bibliotecaContext.SaveChangesAsync();
            return this.Ok();
        }

        [HttpGet("GetByAutor/{idAutor}")]
        public async Task<IActionResult> GetByAutor(int idAutor)
        {
            try
            {
                var libros = this.bibliotecaContext.Libros.Where(c => c.IdAutor == idAutor);
                return this.Ok(libros.Select(c => new LibroDTO
                {
                    Id = c.Id,
                    IdAutor = c.IdAutor,
                    Cantidad = c.Cantidad,
                    FechaPublicacion = c.FechaPublicacion,
                    Nombre = c.Nombre,
                }));
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }
    }
}
