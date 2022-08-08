using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carvajal.Shared.Entidades
{
    public class Usuario
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int UsuID { get; set; }

        [Required(ErrorMessage = "El Nombre es Requerido")]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede superar 50 caracteres")]
        public string UsuNombre { get; set; }

        [Required(ErrorMessage = "La Contraseña es Requerida")]
        public string UsuPass { get; set; }

        [NotMapped]
        public string? UsuPassDecrypt { get; set; }
    }
}
