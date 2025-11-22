using Capa_de_datos.Modelos;
using Capa_de_datos.Repository;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioRepository _repo;

    public UsuarioController(UsuarioRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public IActionResult Consultar(int? id)
    {

        var usuarios = _repo.Consultar(id);

        if (id.HasValue)
        {
            var usuario = usuarios.FirstOrDefault();
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        return Ok(usuarios);
    }

    [HttpPost]
    public IActionResult Agregar([FromBody] Usuario usuario)
    {
        _repo.Agregar(usuario);
        return Ok("Usuario agregado correctamente.");
    }

    [HttpPut]
    public IActionResult Modificar([FromBody] Usuario user)
    {
        _repo.Modificar(user);
        return Ok("Usuario modificado correctamente.");
    }

    [HttpDelete("{id}")]
    public IActionResult Eliminar(int id)
    {
        _repo.Eliminar(id);
        return Ok("Usuario eliminado correctamente.");
    }
}
