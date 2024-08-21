using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : Controller
    {

        private readonly string secrekey;

           public UsuariosController (IConfiguration config)
        {
            secrekey = config.GetSection("settings").GetSection("secretkey").ToString();
        }

        [HttpPost("login")]

        public IActionResult Login(Usuario user)
        {
            try
            {
                using (var DB = new AulaControlContext())
                {
                   
                    var usuario = DB.Usuarios.FirstOrDefault(u => u.Correo == user.Correo && u.Contrasena == user.Contrasena);

                    if (usuario != null)
                    {
                      
                        var usuarioResponse = new
                        {
                            Id = usuario.Id,
                            Correo = usuario.Correo,
                            RolId = usuario.RolId,
                            
                            
                        };

                        return Ok(usuarioResponse); 
                    }
                    else
                    {
                        return NotFound("Correo o contraseña incorrectos"); 
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }



        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            using (var DB = new Models.AulaControlContext())
            {
                var lst = from u in DB.Usuarios
                          select new Usuario
                          {
                              Id = u.Id,
                              Rol = u.Rol,
                              Correo = u.Correo,
                              RolId = u.RolId,
                          };

               
                int totalUsuarios = DB.Usuarios.Count();

                
                Response.Headers.Add("X-Total-Count", totalUsuarios.ToString());

                return lst.ToList();
            }
        }


        [HttpGet("{id}")]
        public ActionResult<Usuario> Get(int id)
        {
            using (var context = new AulaControlContext())
            {
                var usuario = context.Usuarios.FirstOrDefault(u => u.Id == id);
                if (usuario == null)
                {
                    return NotFound();
                }
                return usuario;
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (var context = new AulaControlContext())
            {
                var usuario = context.Usuarios.FirstOrDefault(u => u.Id == id);
                if (usuario == null)
                {
                    return NotFound();
                }
                context.Usuarios.Remove(usuario);
                context.SaveChanges();
                return NoContent();
            }
        }

        [HttpPost]
        public ActionResult<Usuario> Post(Usuario usuario)
        {
            using (var context = new AulaControlContext())
            {
                context.Usuarios.Add(usuario);
                context.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = usuario.Id }, usuario);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Usuario> Put(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            using (var context = new AulaControlContext())
            {
                context.Entry(usuario).State = EntityState.Modified;
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(id, context))
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

        private bool UsuarioExists(int id, AulaControlContext context)
        {
            return context.Usuarios.Any(e => e.Id == id);
        }
    }

    

    }



