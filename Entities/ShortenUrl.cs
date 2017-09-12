using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("shorten_urls")]
    public class ShortenUrl
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("long_url")]  
        [StringLength(1000)]
        public string LongUrl { get; set; }

        [Required]
        [Column("alias")]
        [StringLength(20)]
        public string Alias { get; set; }

        [Required]
        [Column("adicionado")]
        public DateTime Adicionado { get; set; }

        [Required]
        [Column("ip")]
        [StringLength(50)]
        public string Ip { get; set; }

        [Required]
        [Column("numero_clicks")]
        public int NumeroDeClicks { get; set; }

        public Estatistica[] Estatistica { get; set; }
    }
}
