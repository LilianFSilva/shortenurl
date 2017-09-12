using Entities;
using Negocio;
using ShortenURL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ShortenURL.ApiControllers
{
    [RoutePrefix("api/url")]
    public class UrlController : ApiController
    {
        private IRepositorioShorten _urlRepositorio;

        public UrlController(IRepositorioShorten urlRepositorio)
        {
            this._urlRepositorio = urlRepositorio;
        }

        [Route("shorten")]
        [HttpGet]
        public async Task<Url> Shorten([FromUri]string url, [FromUri]string segment = "")
        {
            ShortenUrl shortenUrl = await this._urlRepositorio.ShortenUrl(HttpUtility.UrlDecode(url), HttpContext.Current.Request.UserHostAddress, segment);
            Url urlModel = new Url()
            {
                LongURL = url,
                ShortenURL = string.Format("{0}://{1}/{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority, shortenUrl.Alias)
            };
            return urlModel;
        }
    }
}
