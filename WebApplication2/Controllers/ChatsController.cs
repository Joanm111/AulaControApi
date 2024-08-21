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
    public class ChatsController : Controller
    {
        [HttpGet]
        public IEnumerable<Chat> Get()
        {
            using (var context = new AulaControlContext())
            {
                return context.Chats.ToList();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Chat> Get(int id)
        {
            using (var context = new AulaControlContext())
            {
                var chat = context.Chats.FirstOrDefault(c => c.Id == id);
                if (chat == null)
                {
                    return NotFound();
                }
                return chat;
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (var context = new AulaControlContext())
            {
                var chat = context.Chats.FirstOrDefault(c => c.Id == id);
                if (chat == null)
                {
                    return NotFound();
                }
                context.Chats.Remove(chat);
                context.SaveChanges();
                return NoContent();
            }
        }

        [HttpPost]
        public ActionResult<Chat> Post(Chat chat)
        {
            using (var context = new AulaControlContext())
            {
                context.Chats.Add(chat);
                context.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = chat.Id }, chat);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Chat> Put(int id, Chat chat)
        {
            if (id != chat.Id)
            {
                return BadRequest();
            }

            using (var context = new AulaControlContext())
            {
                context.Entry(chat).State = EntityState.Modified;
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChatExists(id, context))
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

        private bool ChatExists(int id, AulaControlContext context)
        {
            return context.Chats.Any(e => e.Id == id);
        }
    }
}
