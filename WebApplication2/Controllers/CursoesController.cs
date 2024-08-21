using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GradoController : Controller
    {
        [HttpGet]
        public IEnumerable<Grado> Get()
        {
            using (var context = new AulaControlContext())
            {
                return context.Grados.ToList();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Grado> Get(int id)
        {
            using (var context = new AulaControlContext())
            {
                var curso = context.Grados.FirstOrDefault(c => c.Id == id);
                if (curso == null)
                {
                    return NotFound();
                }
                return curso;
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (var context = new AulaControlContext())
            {
                var curso = context.Grados.FirstOrDefault(c => c.Id == id);
                if (curso == null)
                {
                    return NotFound();
                }
                context.Grados.Remove(curso);
                context.SaveChanges();
                return NoContent();
            }
        }

        [HttpPost]
        public ActionResult<Grado> Post(Grado curso)
        {
            using (var context = new AulaControlContext())
            {
                context.Grados.Add(curso);
                context.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = curso.Id  }, curso);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Grado> Put(int id, Grado curso)
        {
            if (id != curso.Id)
            {
                return BadRequest();
            }

            using (var context = new AulaControlContext())
            {
                context.Entry(curso).State = EntityState.Modified;
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoExists(id, context))
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

        private bool CursoExists(int id, AulaControlContext context)
        {
            return context.Grados.Any(e => e.Id == id);
        }
    }
}
