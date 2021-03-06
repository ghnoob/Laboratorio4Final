using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinalLaboratorio4.Models
{
    public class Marca
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [MaxLength(50)]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        public List<Producto> Productos { get; set; }
    }
}
