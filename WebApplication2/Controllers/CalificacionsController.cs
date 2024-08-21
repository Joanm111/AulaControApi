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
    public class CalificacionsController : Controller


    {
        [HttpGet]
        public IEnumerable<Calificacion> Get()
        {
            using (var context = new AulaControlContext())
            {
                return context.Calificacions.ToList();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Calificacion> Get(int id)
        {
            using (var context = new AulaControlContext())
            {
                var calificacion = context.Calificacions.FirstOrDefault(c => c.Id == id);
                if (calificacion == null)
                {
                    return NotFound();
                }
                return calificacion;
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (var context = new AulaControlContext())
            {
                var calificacion = context.Calificacions.FirstOrDefault(c => c.Id == id);
                if (calificacion == null)
                {
                    return NotFound();
                }
                context.Calificacions.Remove(calificacion);
                context.SaveChanges();
                return NoContent();
            }
        }

        [HttpPost]
        public ActionResult<Calificacion> Post(Calificacion calificacion)
        {
            using (var context = new AulaControlContext())
            {
                context.Calificacions.Add(calificacion);
                context.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = calificacion.Id }, calificacion);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Calificacion> Put(int id, Calificacion calificacion)
        {
            if (id != calificacion.Id)
            {
                return BadRequest();
            }

            using (var context = new AulaControlContext())
            {
                context.Entry(calificacion).State = EntityState.Modified;
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalificacionExists(id, context))
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

        private bool CalificacionExists(int id, AulaControlContext context)
        {
            return context.Calificacions.Any(e => e.Id == id);
        }
    }




}
