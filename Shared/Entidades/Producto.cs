using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carvajal.Shared.Entidades
{
    public class Producto
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int ProID { get; set; }

        [Required(ErrorMessage = "La Descripcion es Requerida")]
        [StringLength(50, ErrorMessage = "La descripcion del producto no puede superar los 50 caracteres")]
        public string ProDesc { get; set; }

        [Required]
        public decimal ProValor { get; set; }
    }
}
