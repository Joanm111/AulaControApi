using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificacionesController : Controller
    {
        [HttpGet]
        public IEnumerable<Notificacion> Get()
        {
            using (var context = new AulaControlContext())
            {
                return context.Notificacions.ToList();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Notificacion> Get(int id)
        {
            using (var context = new AulaControlContext())
            {
                var notificacion = context.Notificacions.FirstOrDefault(n => n.Id == id);
                if (notificacion == null)
                {
                    return NotFound();
                }
                return notificacion;
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (var context = new AulaControlContext())
            {
                var notificacion = context.Notificacions.FirstOrDefault(n => n.Id == id);
                if (notificacion == null)
                {
                    return NotFound();
                }
                context.Notificacions.Remove(notificacion);
                context.SaveChanges();
                return NoContent();
            }
        }

        [HttpPost]
        public ActionResult<Notificacion> Post(Notificacion notificacion)
        {
            notificacion.Fecha = DateOnly.FromDateTime(DateTime.UtcNow);
            using (var context = new AulaControlContext())
            {
                context.Notificacions.Add(notificacion);
                context.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = notificacion.Id }, notificacion);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Notificacion> Put(int id, Notificacion notificacion)
        {
            if (id != notificacion.Id)
            {
                return BadRequest();
            }

            using (var context = new AulaControlContext())
            {
                context.Entry(notificacion).State = EntityState.Modified;
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotificacionExists(id, context))
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

        private bool NotificacionExists(int id, AulaControlContext context)
        {
            return context.Notificacions.Any(e => e.Id == id);
        }
    }
}
