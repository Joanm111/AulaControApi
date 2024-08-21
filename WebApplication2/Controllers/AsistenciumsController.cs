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
    public class AsistenciumsController : Controller
    {
        [HttpGet]
        public IEnumerable<Asistencium> Get()
        {
            using (var context = new AulaControlContext())
            {
                return context.Asistencia.ToList();
            }
        }

        [HttpPost]
        public ActionResult<Asistencium> Post([FromBody] Asistencium asistencia)
        {
            using (var context = new AulaControlContext())
            {
                asistencia.Fecha = DateOnly.FromDateTime(DateTime.UtcNow);
                context.Asistencia.Add(asistencia);
                context.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = asistencia.Id }, asistencia);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Asistencium> Put(int id, Asistencium asistencium)
        {
            if (id != asistencium.Id)
            {
                return BadRequest();
            }

            using (var context = new AulaControlContext())
            {
                context.Entry(asistencium).State = EntityState.Modified;
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsistenciaExists(id, context))
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
        private bool AsistenciaExists(int id, AulaControlContext context)
        {
            return context.Asistencia.Any(e => e.Id == id);
        }


    }
}