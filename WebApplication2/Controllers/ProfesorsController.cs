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
    public class ProfesorsController : Controller

    {
        [HttpGet]
        public IEnumerable<Profesor> Get()
        {
            using (var context = new AulaControlContext())
            {
                return context.Profesors.ToList();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Profesor> Get(int id)
        {
            using (var context = new AulaControlContext())
            {
                var profesor = context.Profesors.FirstOrDefault(p => p.Id == id);
                if (profesor == null)
                {
                    return NotFound();
                }
                return profesor;
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (var context = new AulaControlContext())
            {
                var profesor = context.Profesors.FirstOrDefault(p => p.Id == id);
                if (profesor == null)
                {
                    return NotFound();
                }
                context.Profesors.Remove(profesor);
                context.SaveChanges();
                return NoContent();
            }
        }

        [HttpPost]
        public ActionResult<Profesor> Post(Profesor profesor)
        {
            using (var context = new AulaControlContext())
            {
                context.Profesors.Add(profesor);
                context.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = profesor.Id }, profesor);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Profesor> Put(int id, Profesor profesor)
        {
            if (id != profesor.Id)
            {
                return BadRequest();
            }

            using (var context = new AulaControlContext())
            {
                context.Entry(profesor).State = EntityState.Modified;
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesorExists(id, context))
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

        private bool ProfesorExists(int id, AulaControlContext context)
        {
            return context.Profesors.Any(e => e.Id == id);
        }
    }
}
