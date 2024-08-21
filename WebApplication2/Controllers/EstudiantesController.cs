using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstudiantesController : ControllerBase
    {
    
        [HttpGet]
        public IEnumerable<Estudiante> Get()
        {
            using (var DB = new AulaControlContext())
            {
                var lst = from u in DB.Estudiantes
                          select new Estudiante
                          {
                              Id = u.Id,
                              Nombre = u.Nombre,
                              Apellido = u.Apellido,
                              FechaNacimiento = u.FechaNacimiento,
                              PadreId = u.PadreId,
                              GradoId = u.GradoId,
                              Calificacions = u.Calificacions,
                              Asistencia = u.Asistencia,
                              Conducta = u.Conducta,
                              Reportes = u.Reportes,
                              Grado = u.Grado,
                              Padre = u.Padre
                          };

                return lst.ToList();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Estudiante> Get(int id)
        {
            using (var DB = new AulaControlContext())
            {
                var estudiante = DB.Estudiantes
                    .Include(e => e.Calificacions)
                    .Include(e => e.Asistencia)
                    .Include(e => e.Conducta)
                    .Include(e => e.Reportes)
                    .Include(e => e.Grado)
                    .Include(e => e.Padre)
                    .FirstOrDefault(e => e.Id == id);

                if (estudiante == null)
                {
                    return NotFound();
                }

                return estudiante;
            }
        }

        [HttpPost]
        public ActionResult<Estudiante> Post(Estudiante estudiante)
        {
            using (var DB = new AulaControlContext())
            {
                DB.Estudiantes.Add(estudiante);
                DB.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = estudiante.Id }, estudiante);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Estudiante> Put(int id, Estudiante estudiante)
        {
            if (id != estudiante.Id)
            {
                return BadRequest();
            }

            using (var DB = new AulaControlContext())
            {
                DB.Entry(estudiante).State = EntityState.Modified;

                try
                {
                    DB.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteExists(id, DB))
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

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (var DB = new AulaControlContext())
            {
                var estudiante = DB.Estudiantes.Find(id);
                if (estudiante == null)
                {
                    return NotFound();
                }

                DB.Estudiantes.Remove(estudiante);
                DB.SaveChanges();
                return NoContent();
            }
        }

        private bool EstudianteExists(int id, AulaControlContext DB)
        {
            return DB.Estudiantes.Any(e => e.Id == id);
        }
    }

}
       


