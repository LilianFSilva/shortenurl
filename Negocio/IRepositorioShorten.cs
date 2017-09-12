using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public interface IRepositorioShorten
    {
        Task<ShortenUrl> ShortenUrl(string longUrl, string ip, string alias = "");
        Task<Estatistica> Estatisticas(string alias, string referencia, string ip);
    }
}
