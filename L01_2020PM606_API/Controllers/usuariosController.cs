using L01_2020PM606_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020PM606_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly blogDBContext _blogDBContext;

        public usuariosController(blogDBContext blogDBContext)
        {
            _blogDBContext = blogDBContext;
        }

        [HttpGet]
        [Route("GetAll")]

        //SELECT
        public IActionResult Get()
        {
            List<usuarios> listadoUsuarios = (from e in _blogDBContext.usuarios
                                                 select e).ToList();
            if (listadoUsuarios.Count == 0) { return NotFound(); }

            return Ok(listadoUsuarios);
        }

        [HttpPost] //Create o insert
        [Route("add")]

        public IActionResult crear([FromBody] usuarios Usuarionuevo) //le ponemos Frombody para que lo busque en el código
        {
            try
            {
                _blogDBContext.usuarios.Add(Usuarionuevo);
                _blogDBContext.SaveChanges();

                return Ok(Usuarionuevo); // tiene sentido regresar este objeto para corroborar el insert que se quedo en la d◘

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut]
        [Route("update/(id)")]

        public IActionResult actualizar(int id, [FromBody] usuarios usuarioActualizar)
        {
            try
            {
                usuarios? usuarioExist = (from e in _blogDBContext.usuarios
                                             where e.usuarioId == id
                                             select e).FirstOrDefault();

                if (usuarioExist == null) { return NotFound(); }

                usuarioExist.RolId = usuarioActualizar.RolId;
                usuarioExist.nombreUsuario = usuarioActualizar.nombreUsuario;
                usuarioExist.clave = usuarioActualizar.clave;
                usuarioExist.nombre = usuarioActualizar.nombre;
                usuarioExist.apellido = usuarioActualizar.apellido;


                _blogDBContext.Entry(usuarioExist).State = EntityState.Modified;
                _blogDBContext.SaveChanges();

                return Ok(usuarioActualizar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete/(id)")]

        public IActionResult borrar(int id)
        {
            try
            {
                usuarios? usuarioEliminar = (from e in _blogDBContext.usuarios
                                                where e.usuarioId == id
                                                select e).FirstOrDefault();

                if (usuarioEliminar == null) { return NotFound(); }

                ////Esto se hace para eliminar los registros cosa que no se debe hacer

                _blogDBContext.usuarios.Attach(usuarioEliminar); //para apuntar cual de todos vamos a eliminar
                _blogDBContext.usuarios.Remove(usuarioEliminar);
                _blogDBContext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("findbynombreApellido/{filtro}")] //nombre y apellido

        public IActionResult buscar_nombreApellido(string filtro)
        {
            List<usuarios> categoriaList = (from e in _blogDBContext.usuarios
                                             where e.nombre.Contains(filtro) || e.apellido.Contains(filtro)
                                             select e).ToList();

            if (categoriaList.Any()) { return Ok(categoriaList); }

            return NotFound();
        }

        [HttpGet]
        [Route("findbyRolId/{filtro}")] //rol

        public IActionResult buscarRol(int filtro)
        {
            List<usuarios> categoriaList = (from e in _blogDBContext.usuarios
                                            where e.RolId == filtro
                                            select e).ToList();

            if (categoriaList.Any()) { return Ok(categoriaList); }

            return NotFound();
        }
    }
}
