﻿using ShortenURL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ShortenURL
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Filters.Add(new ShortenApiErrorFilter());
        }
    }
}
