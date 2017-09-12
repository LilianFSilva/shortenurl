using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShortenURL.Models
{
    public class Url
    {
        [Required]
        [JsonProperty("longUrl")]
        public string LongURL { get; set; }

        [JsonProperty("shortenUrl")]
        public string ShortenURL { get; set; }

        [JsonIgnore]
        public string CustomAlias { get; set; }
    }
}