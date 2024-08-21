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
    public class ConductumsController : Controller
    {
        [HttpGet]
        public IEnumerable<Conductum> Get()
        {
            using (var context = new AulaControlContext())
            {
                return context.Conducta.ToList();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Conductum> Get(int id)
        {
            using (var context = new AulaControlContext())
            {
                var conductum = context.Conducta.FirstOrDefault(c => c.Id == id);
                if (conductum == null)
                {
                    return NotFound();
                }
                return conductum;
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (var context = new AulaControlContext())
            {
                var conductum = context.Conducta.FirstOrDefault(c => c.Id == id);
                if (conductum == null)
                {
                    return NotFound();
                }
                context.Conducta.Remove(conductum);
                context.SaveChanges();
                return NoContent();
            }
        }

        [HttpPost]
        public ActionResult<Conductum> Post(Conductum conductum)
        {
            using (var context = new AulaControlContext())
            {
                context.Conducta.Add(conductum);
                context.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = conductum.Id }, conductum);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Conductum> Put(int id, Conductum conductum)
        {
            if (id != conductum.Id)
            {
                return BadRequest();
            }

            using (var context = new AulaControlContext())
            {
                context.Entry(conductum).State = EntityState.Modified;
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConductumExists(id, context))
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

        private bool ConductumExists(int id, AulaControlContext context)
        {
            return context.Conducta.Any(e => e.Id == id);
        }
    }
}
