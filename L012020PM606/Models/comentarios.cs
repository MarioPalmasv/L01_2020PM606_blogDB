using System.ComponentModel.DataAnnotations;

namespace L012020PM606.Models
{
    public class comentarios
    {
        [Key]
        public int cometarioId { get; set; }

        public int publicacionId { get; set; }

        public string comentario { get; set; }

        public int usuarioId { get; set; }
    }
}
