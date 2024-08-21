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
    public class ReportesController : Controller
    {
        [HttpGet]
        public IEnumerable<Reporte> Get()
        {
            using (var context = new AulaControlContext())
            {
                return context.Reportes.ToList();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Reporte> Get(int id)
        {
            using (var context = new AulaControlContext())
            {
                var reporte = context.Reportes.FirstOrDefault(r => r.Id == id);
                if (reporte == null)
                {
                    return NotFound();
                }
                return reporte;
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (var context = new AulaControlContext())
            {
                var reporte = context.Reportes.FirstOrDefault(r => r.Id == id);
                if (reporte == null)
                {
                    return NotFound();
                }
                context.Reportes.Remove(reporte);
                context.SaveChanges();
                return NoContent();
            }
        }

        [HttpPost]
        public ActionResult<Reporte> Post(Reporte reporte)
        {
            using (var context = new AulaControlContext())
            {
                context.Reportes.Add(reporte);
                context.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = reporte.Id }, reporte);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Reporte> Put(int id, Reporte reporte)
        {
            if (id != reporte.Id)
            {
                return BadRequest();
            }

            using (var context = new AulaControlContext())
            {
                context.Entry(reporte).State = EntityState.Modified;
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReporteExists(id, context))
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

        private bool ReporteExists(int id, AulaControlContext context)
        {
            return context.Reportes.Any(e => e.Id == id);
        }
    }
}
