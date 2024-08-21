using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MateriumController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Materium> Get()
        {
            using (var context = new AulaControlContext())
            {
                return context.Materia.ToList();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Materium> Get(int id)
        {
            using (var context = new AulaControlContext())
            {
                var Materia = context.Materia.FirstOrDefault(c => c.Id == id);
                if (Materia == null)
                {
                    return NotFound();
                }
                return Materia;
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (var context = new AulaControlContext())
            {
                var Materia = context.Materia.FirstOrDefault(c => c.Id == id);
                if (Materia == null)
                {
                    return NotFound();
                }
                context.Materia.Remove(Materia);
                context.SaveChanges();
                return NoContent();
            }
        }

        [HttpPost]
        public ActionResult<Materium> Post(Materium materium)
        {
            using (var context = new AulaControlContext())
            {
                context.Materia.Add(materium);
                context.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = materium.Id }, materium);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Materium> Put(int id, Materium materium)
        {
            if (id != materium.Id)
            {
                return BadRequest();
            }

            using (var context = new AulaControlContext())
            {
                context.Entry(materium).State = EntityState.Modified;
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MateriaExists(id, context))
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

        private bool MateriaExists(int id, AulaControlContext context)
        {
            return context.Materia.Any(e => e.Id == id);
        }
    }
}
