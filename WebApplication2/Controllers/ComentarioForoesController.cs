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
    public class ComentarioForoesController : Controller
    {
        [HttpGet]
        public IEnumerable<ComentarioForo> Get(int foroId)
        {
            using (var context = new AulaControlContext())
            {
                return context.ComentarioForos.Where(c => c.ForoId == foroId).ToList();
            }
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (var context = new AulaControlContext())
            {
                var comentarioForo = context.ComentarioForos.FirstOrDefault(c => c.Id == id);
                if (comentarioForo == null)
                {
                    return NotFound();
                }
                context.ComentarioForos.Remove(comentarioForo);
                context.SaveChanges();
                return NoContent();
            }
        }

        [HttpPost]
        public ActionResult<ComentarioForo> Post(ComentarioForo comentarioForo)
        {
            using (var context = new AulaControlContext())
            {
                context.ComentarioForos.Add(comentarioForo);
                context.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = comentarioForo.Id }, comentarioForo);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<ComentarioForo> Put(int id, ComentarioForo comentarioForo)
        {
            if (id != comentarioForo.Id)
            {
                return BadRequest();
            }

            using (var context = new AulaControlContext())
            {
                context.Entry(comentarioForo).State = EntityState.Modified;
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComentarioForoExists(id, context))
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

        private bool ComentarioForoExists(int id, AulaControlContext context)
        {
            return context.ComentarioForos.Any(e => e.Id == id);
        }
    }
}
