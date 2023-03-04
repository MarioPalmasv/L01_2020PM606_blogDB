using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace L012020PM606.Models
{
    public class usuarios
    {
        [Key]
        public int usuarioId { get; set; }

        public int RolId { get; set; }
        public string nombreUsuario { get; set; }

        public string clave { get; set; }

        public string nombre { get; set; }

        public string apellido { get; set; }

        
    }
}
