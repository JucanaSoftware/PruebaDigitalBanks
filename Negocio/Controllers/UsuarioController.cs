using Datos.Models;
using Datos.Models.Dto;
using Datos.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Negocio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepository _repository;

        public UsuarioController(UsuarioRepository repository)
        {
            _repository = repository;
        }
        // GET api/usuario
        [HttpGet]
        public IActionResult ConsultarTodos()
        {
            var usuarios = _repository.Consultar();
            return Ok(usuarios);
        }

        // GET api/usuario/5
        [HttpGet("{id}")]
        public IActionResult Consultar(int id)
        {
            var usuario = _repository.Consultar(id);
            if (usuario == null || usuario.Count == 0)
                return NotFound();
            return Ok(usuario[0]);
        }

        // POST api/usuario
        [HttpPost]
        public IActionResult Agregar([FromBody] UsuarioDto usuarioDto)
        {
            var usuario = new Usuario
            {
                Nombre = usuarioDto.Nombre,
                FechaNacimiento = usuarioDto.FechaNacimiento,
                Sexo = usuarioDto.Sexo
            };
            var newId = _repository.Agregar(usuario);
            return Ok(new { Guardado = true, id=newId });
        }

        // PUT api/usuario/5
        [HttpPut]
        public IActionResult Modificar([FromBody] Usuario usuario)
        {
            var filas = _repository.Modificar(usuario);
            return Ok(new { Actualizado = true, id = usuario.Id });
        }

        // DELETE api/usuario/5
        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            var filas = _repository.Eliminar(id);
            return Ok(new { Borrado = true, id = id });
        }

    }
}
