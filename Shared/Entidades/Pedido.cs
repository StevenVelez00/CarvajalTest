using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carvajal.Shared.Entidades
{
    public class Pedido
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int PedId { get; set; }

        [Required(ErrorMessage = "La Cantidad es Requerida")]
        public int PedCant { get; set; }

        [Required(ErrorMessage = "El Valor Unitario es Requerido")]
        public Decimal pedVrUnit { get; set; }

        [Required(ErrorMessage = "El IVA es Requerido")]
        public Decimal PedIVA { get; set; }

        public Decimal PedSubtot { get; set; }
        public Decimal PedTotal { get; set; }

        public Usuario? Usuario { get; set; }
        public Producto? Producto { get; set; }

        [Required(ErrorMessage = "El Producto es Requerido")]
        public int ProductoProID { get; set; }

        [Required(ErrorMessage = "El Usuario es Requerido")]
        public int UsuarioUsuID { get; set; }
    }
}
