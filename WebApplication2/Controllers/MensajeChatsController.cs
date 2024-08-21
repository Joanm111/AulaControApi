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
    public class MensajeChatsController : Controller
    {
        [HttpGet]
        public IEnumerable<MensajeChat> Get()
        {
            using (var context = new AulaControlContext())
            {
                return context.MensajeChats.ToList();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<MensajeChat> Get(int id)
        {
            using (var context = new AulaControlContext())
            {
                var mensajeChat = context.MensajeChats.FirstOrDefault(m => m.Id == id);
                if (mensajeChat == null)
                {
                    return NotFound();
                }
                return mensajeChat;
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (var context = new AulaControlContext())
            {
                var mensajeChat = context.MensajeChats.FirstOrDefault(m => m.Id == id);
                if (mensajeChat == null)
                {
                    return NotFound();
                }
                context.MensajeChats.Remove(mensajeChat);
                context.SaveChanges();
                return NoContent();
            }
        }

        [HttpPost]
        public ActionResult<MensajeChat> Post(MensajeChat mensajeChat)
        {
            using (var context = new AulaControlContext())
            {
                context.MensajeChats.Add(mensajeChat);
                context.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = mensajeChat.Id }, mensajeChat);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<MensajeChat> Put(int id, MensajeChat mensajeChat)
        {
            if (id != mensajeChat.Id)
            {
                return BadRequest();
            }

            using (var context = new AulaControlContext())
            {
                context.Entry(mensajeChat).State = EntityState.Modified;
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MensajeChatExists(id, context))
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

        private bool MensajeChatExists(int id, AulaControlContext context)
        {
            return context.MensajeChats.Any(e => e.Id == id);
        }
    }
}
