using Negocio;
using ShortenURL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Entities;

namespace ShortenURL.Controllers
{
    public class UrlController : Controller
    {
        private IRepositorioShorten _urlRepositorio;

        [HttpGet]
        public ActionResult Index()
        {
            Url url = new Url();
            return View();
        }

        public UrlController(IRepositorioShorten urlRepositorio)
        {
            this._urlRepositorio = urlRepositorio;
        }

        public async Task<ActionResult> Index(Url url)
        {
            if (ModelState.IsValid)
            {
                ShortenUrl shortenUrl = await this._urlRepositorio.ShortenUrl(url.LongURL, Request.UserHostAddress, url.CustomAlias);
                url.ShortenURL = string.Format("{0}://{1}{2}{3}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"), shortenUrl.Alias);
            }
            return View(url);
        }

        public async Task<ActionResult> Click(string segment)
        {
            string referer = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : string.Empty;
            Estatistica estatistica = await this._urlRepositorio.Estatisticas(segment, referer, Request.UserHostAddress);
            return this.RedirectPermanent(estatistica.ShortenUrl.LongUrl);
        }
    }
}