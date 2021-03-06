using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinalLaboratorio4.Models
{
    public class Proveedor
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        [MaxLength(50)]
        [Phone]
        [DisplayName("Teléfono")]
        public string Telefono { get; set; }

        [Required]
        [StringLength(50)]
        [MaxLength(50)]
        public string Domicilio { get; set; }

        [Required]
        [StringLength(50)]
        [MaxLength(50)]
        public string Localidad { get; set; }

        [Required]
        [StringLength(50)]
        [MaxLength(50)]
        public string Provincia { get; set; }

        public ICollection<Producto> Productos { get; set; }
    }
}
