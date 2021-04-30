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
    public class AutorsController : ControllerBase
    {
        private readonly BibliotecaContext bibliotecaContext;

        public AutorsController(BibliotecaContext bibliotecaContext)
        {
            this.bibliotecaContext = bibliotecaContext;
        }


        [HttpPost()]
        public async Task<IActionResult> AddNewAsync(AutorDTO autorDTO)
        {
            try
            {
                var exist = this.bibliotecaContext.Autors.FirstOrDefault(a => a.Nombre == autorDTO.Nombre);
                if (exist != null)
                {
                    return this.BadRequest(new { error = $"Ya existe un autor con el nombre {autorDTO.Nombre}"});
                }
                await this.bibliotecaContext.Autors.AddAsync(new Autor
                {
                    Edad = autorDTO.Edad,
                    Nombre = autorDTO.Nombre,
                });

                await this.bibliotecaContext.SaveChangesAsync();
                return this.Ok();
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }
    }
}
