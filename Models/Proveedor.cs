using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinalLaboratorio4.Models
{
    public class Proveedor
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [MaxLength(20)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(20)]
        [MaxLength(20)]
        [Phone]
        [DisplayName("Teléfono")]
        public string Telefono { get; set; }

        [Required]
        [StringLength(20)]
        [MaxLength(20)]
        public string Domicilio { get; set; }

        [Required]
        [StringLength(20)]
        [MaxLength(20)]
        public string Localidad { get; set; }

        [Required]
        [StringLength(20)]
        [MaxLength(20)]
        public string Provincia { get; set; }

        public ICollection<Producto> Productos { get; set; }
    }
}
