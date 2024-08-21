using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PadresController : Controller
    {
        [HttpGet]
        public IEnumerable<Padre> Get()
        {
            using (var context = new AulaControlContext())
            {
                return context.Padres.ToList();
            }
        }









        [HttpGet("{id}")]
        public ActionResult<Padre> Get(int id)
        {
            using (var context = new AulaControlContext())
            {
                var padre = context.Padres.FirstOrDefault(p => p.Id == id);
                if (padre == null)
                {
                    return NotFound();
                }
                return padre;
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                using (var context = new AulaControlContext())
                {
                    
                    var padre = context.Padres
                        .Include(p => p.Estudiantes)
                            .ThenInclude(e => e.Asistencia)
                        .Include(p => p.Estudiantes)
                            .ThenInclude(e => e.Calificacions)
                        .Include(p => p.Estudiantes)
                            .ThenInclude(e => e.Conducta)
                        .Include(p => p.Estudiantes)
                            .ThenInclude(e => e.Reportes)
                        .Include(p => p.Usuario)
                        .FirstOrDefault(p => p.Id == id);

                    if (padre == null)
                    {
                        return NotFound();
                    }

                    
                    foreach (var estudiante in padre.Estudiantes.ToList())
                    {
                        
                        foreach (var calificacion in estudiante.Calificacions.ToList())
                        {
                            context.Calificacions.Remove(calificacion);
                        }

                        
                        foreach (var asistencia in estudiante.Asistencia.ToList())
                        {
                            context.Asistencia.Remove(asistencia);
                        }

                        
                        foreach (var conducta in estudiante.Conducta.ToList())
                        {
                            context.Conducta.Remove(conducta);
                        }

                        foreach (var reporte in estudiante.Reportes.ToList())
                        {
                            context.Reportes.Remove(reporte);
                        }

                        estudiante.GradoId = null;

                        
                        context.Estudiantes.Remove(estudiante);
                    }

                   
                    var usuarioPadre = padre.Usuario;
                    if (usuarioPadre != null)
                    {
                        
                        padre.UsuarioId = null;

                       
                        context.Usuarios.Remove(usuarioPadre);
                    }

                  
                    context.Padres.Remove(padre);

                    context.SaveChanges();

                    return NoContent();
                }
            }
            catch (Exception ex)
            {
              
                Console.WriteLine($"Error en el método Delete: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }




        [HttpPost]
        public ActionResult<Padre> Post(Padre padre)
        {
            using (var context = new AulaControlContext())
            {
                context.Padres.Add(padre);
                context.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = padre.Id }, padre);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Padre> Put(int id, Padre padre)
        {
            if (id != padre.Id)
            {
                return BadRequest();
            }

            using (var context = new AulaControlContext())
            {
                context.Entry(padre).State = EntityState.Modified;
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PadreExists(id, context))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
        }

        private bool PadreExists(int id, AulaControlContext context)
        {
            return context.Padres.Any(e => e.Id == id);
        }
    }
}
