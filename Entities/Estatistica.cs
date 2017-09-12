using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("estatistica")]
    public class Estatistica
    {
        [Key]  
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("data")]
        public DateTime Data { get; set; }

        [Required]
        [Column("ip")]
        [StringLength(50)]
        public string Ip { get; set; }

        [Column("referencia")]
        [StringLength(500)]
        public string Referencia { get; set; }

        public ShortenUrl ShortenUrl { get; set; }
    }
}
