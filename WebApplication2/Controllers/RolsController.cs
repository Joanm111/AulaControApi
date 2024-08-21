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
    public class RolsController : Controller
    {

        
    [HttpGet]
        public IEnumerable<Rol> Get()
        {
            using (var context = new AulaControlContext())
            {
                return context.Rols.ToList();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Rol> Get(int id)
        {
            using (var context = new AulaControlContext())
            {
                var rol = context.Rols.FirstOrDefault(r => r.Id == id);
                if (rol == null)
                {
                    return NotFound();
                }
                return rol;
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (var context = new AulaControlContext())
            {
                var rol = context.Rols.FirstOrDefault(r => r.Id == id);
                if (rol == null)
                {
                    return NotFound();
                }
                context.Rols.Remove(rol);
                context.SaveChanges();
                return NoContent();
            }
        }

        [HttpPost]
        public ActionResult<Rol> Post(Rol rol)
        {
            using (var context = new AulaControlContext())
            {
                context.Rols.Add(rol);
                context.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = rol.Id }, rol);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Rol> Put(int id, Rol rol)
        {
            if (id != rol.Id)
            {
                return BadRequest();
            }

            using (var context = new AulaControlContext())
            {
                context.Entry(rol).State = EntityState.Modified;
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolExists(id, context))
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

        private bool RolExists(int id, AulaControlContext context)
        {
            return context.Rols.Any(e => e.Id == id);
        }
    }
}

