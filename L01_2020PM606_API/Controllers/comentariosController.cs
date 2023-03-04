using L01_2020PM606_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020PM606_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase
    {
        private readonly blogDBContext _blogDBContext;
        public comentariosController(blogDBContext blogDBContext)
        {
            _blogDBContext = blogDBContext;
        }

        [HttpGet]
        [Route("GetAll")]

        //SELECT
        public IActionResult Get()
        {
            List<comentarios> listadoComentarios = (from e in _blogDBContext.comentarios
                                                        select e).ToList();
            if (listadoComentarios.Count == 0) { return NotFound(); }

            return Ok(listadoComentarios);
        }

        [HttpPost] //Create o insert
        [Route("add")]

        public IActionResult crear([FromBody] comentarios Comentarionuevo) //le ponemos Frombody para que lo busque en el código
        {
            try
            {
                _blogDBContext.comentarios.Add(Comentarionuevo);
                _blogDBContext.SaveChanges();

                return Ok(Comentarionuevo); // tiene sentido regresar este objeto para corroborar el insert que se quedo en la d◘

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("update/{id}")]

        public IActionResult actualizar(int id, [FromBody] comentarios comentarioActualizar)
        {
            try
            {
                comentarios? comentarioExist = (from e in _blogDBContext.comentarios
                                                   where e.comentarioId == id
                                                   select e).FirstOrDefault();

                if (comentarioExist == null) { return NotFound(); }

                comentarioExist.publicacionId = comentarioActualizar.publicacionId;
                comentarioExist.comentario = comentarioActualizar.comentario;
                comentarioExist.usuarioId = comentarioActualizar.usuarioId;


                _blogDBContext.Entry(comentarioExist).State = EntityState.Modified;
                _blogDBContext.SaveChanges();

                return Ok(comentarioActualizar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]

        public IActionResult borrar(int id)
        {
            try
            {
                comentarios? comentariosEliminar = (from e in _blogDBContext.comentarios
                                                      where e.comentarioId == id
                                                      select e).FirstOrDefault();

                if (comentariosEliminar == null) { return NotFound(); }

                ////Esto se hace para eliminar los registros cosa que no se debe hacer

                _blogDBContext.comentarios.Attach(comentariosEliminar); //para apuntar cual de todos vamos a eliminar
                _blogDBContext.comentarios.Remove(comentariosEliminar);
                _blogDBContext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("findbypublicacionId/{filtro}")]

        public IActionResult buscar_comentario(int filtro)
        {
            List<comentarios> comentarioList = (from e in _blogDBContext.comentarios
                                                   where e.publicacionId == filtro
                                                   select e).ToList();

            if (comentarioList.Any()) { return Ok(comentarioList); }

            return NotFound();
        }
    }
}
