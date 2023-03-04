using L01_2020PM606_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020PM606_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class publicacionesController : ControllerBase
    {
        private readonly blogDBContext _blogDBContext;
        public publicacionesController(blogDBContext blogDBContext)
        {
            _blogDBContext = blogDBContext;
        }

        [HttpGet]
        [Route("GetAll")]

        //SELECT
        public IActionResult Get()
        {
            List<publicaciones> listadoPublicaciones = (from e in _blogDBContext.publicaciones
                                              select e).ToList();
            if (listadoPublicaciones.Count == 0) { return NotFound(); }

            return Ok(listadoPublicaciones);
        }

        [HttpPost] //Create o insert
        [Route("add")]

        public IActionResult crear([FromBody] publicaciones Publicacionnueva) //le ponemos Frombody para que lo busque en el código
        {
            try
            {
                _blogDBContext.publicaciones.Add(Publicacionnueva);
                _blogDBContext.SaveChanges();

                return Ok(Publicacionnueva); // tiene sentido regresar este objeto para corroborar el insert que se quedo en la d◘

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("update/{id}")]

        public IActionResult actualizar(int id, [FromBody] publicaciones publicacionActualizar)
        {
            try
            {
                publicaciones? publicacionExist = (from e in _blogDBContext.publicaciones
                                          where e.publicacionId == id
                                          select e).FirstOrDefault();

                if (publicacionExist == null) { return NotFound(); }

                publicacionExist.titulo = publicacionActualizar.titulo;
                publicacionExist.descripcion = publicacionActualizar.descripcion;
                publicacionExist.usuarioId = publicacionActualizar.usuarioId;


                _blogDBContext.Entry(publicacionExist).State = EntityState.Modified;
                _blogDBContext.SaveChanges();

                return Ok(publicacionActualizar);
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
                publicaciones? publicacionEliminar = (from e in _blogDBContext.publicaciones
                                             where e.publicacionId == id
                                             select e).FirstOrDefault();

                if (publicacionEliminar == null) { return NotFound(); }

                ////Esto se hace para eliminar los registros cosa que no se debe hacer

                _blogDBContext.publicaciones.Attach(publicacionEliminar); //para apuntar cual de todos vamos a eliminar
                _blogDBContext.publicaciones.Remove(publicacionEliminar);
                _blogDBContext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("findbyUsuarioId/{filtro}")] //nombre y apellido

        public IActionResult buscar_publicacion(int filtro)
        {
            List<publicaciones> publicacionList = (from e in _blogDBContext.publicaciones
                                            where e.usuarioId == filtro
                                            select e).ToList();

            if (publicacionList.Any()) { return Ok(publicacionList); }

            return NotFound();
        }
    }
}
