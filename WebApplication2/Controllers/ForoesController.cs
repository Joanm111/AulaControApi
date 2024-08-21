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
    public class ForoesController : Controller
    {
        [HttpGet]
        public IEnumerable<Foro> Get()
        {
            using (var context = new AulaControlContext())
            {
                return context.Foros.ToList();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Foro> Get(int id)
        {
            using (var context = new AulaControlContext())
            {
                var foro = context.Foros.FirstOrDefault(f => f.Id == id);
                if (foro == null)
                {
                    return NotFound();
                }
                return foro;
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (var context = new AulaControlContext())
            {
                var foro = context.Foros.FirstOrDefault(f => f.Id == id);
                if (foro == null)
                {
                    return NotFound();
                }
                context.Foros.Remove(foro);
                context.SaveChanges();
                return NoContent();
            }
        }

        [HttpPost]
        public ActionResult<Foro> Post(Foro foro)
        {
            using (var context = new AulaControlContext())
            {
                context.Foros.Add(foro);
                context.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = foro.Id }, foro);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Foro> Put(int id, Foro foro)
        {
            if (id != foro.Id)
            {
                return BadRequest();
            }

            using (var context = new AulaControlContext())
            {
                context.Entry(foro).State = EntityState.Modified;
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForoExists(id, context))
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

        private bool ForoExists(int id, AulaControlContext context)
        {
            return context.Foros.Any(e => e.Id == id);
        }
    }
}
