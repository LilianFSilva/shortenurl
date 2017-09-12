using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Data;
using System.Net;
using System.Text.RegularExpressions;
using System.Configuration;
using Exceptions;

namespace Negocio.Implementacoes
{
    public class ShortenUrlManager : IRepositorioShorten
    {
        public Task<Estatistica> Estatisticas(string alias, string referencia, string ip)
        {
            return Task.Run(() =>
            {
                using (var urlContext = new ShortenContext())
                {
                    ShortenUrl url = urlContext.ShortenUrl.Where(u => u.Alias == alias).FirstOrDefault();
                    if (url == null)
                    {
                        throw new ShortenNotFoundException();
                    }

                    url.NumeroDeClicks = url.NumeroDeClicks + 1;

                    Estatistica estatistica = new Estatistica()
                    {
                        Data = DateTime.Now,
                        Ip = ip,
                        Referencia = referencia,
                        ShortenUrl = url
                    };

                    urlContext.Estatistica.Add(estatistica);

                    urlContext.SaveChanges();

                    return estatistica;
                }
            });
        }

        public Task<ShortenUrl> ShortenUrl(string longUrl, string ip, string alias = "")
        {
            return Task.Run(() =>
            {
                using (var urlContext = new ShortenContext())
                {
                    ShortenUrl url;

                    url = urlContext.ShortenUrl.Where(u => u.LongUrl == longUrl).FirstOrDefault();
                    if (url != null)
                    {
                        return url;
                    }

                    if (!longUrl.StartsWith("http://") && !longUrl.StartsWith("https://"))
                    {
                        throw new ArgumentException("URL com formato inválido!");
                    }
                    Uri urlCheck = new Uri(longUrl);
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlCheck);
                    request.Timeout = 10000;
                    try
                    {
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    }
                    catch (Exception)
                    {
                        throw new ShortenNotFoundException();
                    }

                    int cap = 0;
                    string capString = ConfigurationManager.AppSettings["MaxNumberShortUrlsPerHour"];
                    int.TryParse(capString, out cap);
                    DateTime dataCriacao = DateTime.Now.Subtract(new TimeSpan(1, 0, 0));
                    int count = urlContext.ShortenUrl.Where(u => u.Ip == ip && u.Adicionado >= dataCriacao).Count();
                    if (cap != 0 && count > cap)
                    {
                        throw new ArgumentException("Você excedeu o limite diário.");
                    }

                    if (!string.IsNullOrEmpty(alias))
                    {
                        if (urlContext.ShortenUrl.Where(u => u.Alias == alias).Any())
                        {
                            throw new ShortenConflitoException();
                        }
                        if (alias.Length > 20 || !Regex.IsMatch(alias, @"^[A-Za-z\d_-]+$"))
                        {
                            throw new ArgumentException("Alias com formato errado ou muito longo.");
                        }
                    }
                    else
                    {
                        alias = this.NovoAlias();
                    }

                    if (string.IsNullOrEmpty(alias))
                    {
                        throw new ArgumentException("Alias vazio.");
                    }

                    url = new ShortenUrl()
                    {
                        Adicionado = DateTime.Now,
                        Ip = ip,
                        LongUrl = longUrl,
                        NumeroDeClicks = 0,
                        Alias = alias
                    };

                    urlContext.ShortenUrl.Add(url);

                    urlContext.SaveChanges();

                    return url;
                }
            });
        }

        private string NovoAlias()
        {
            using (var urlContext = new ShortenContext())
            {
                int i = 0;
                while (true)
                {
                    string alias = Guid.NewGuid().ToString().Substring(0, 6);
                    if (!urlContext.ShortenUrl.Where(u => u.Alias == alias).Any())
                        return alias;

                    if (i > 30)
                        break;

                    i++;
                }
                return string.Empty;
            }
        }
    }
}
