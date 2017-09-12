using Microsoft.Practices.Unity;
using Negocio;
using Negocio.Implementacoes;
using System;
using System.Web.Http;
using Unity.WebApi;

namespace ShortenURL
{
    public static class UnityConfig
    {
        
           private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
           {
               var container = new UnityContainer();
               RegisterTypes(container);
               return container;
           });
    
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IRepositorioShorten, ShortenUrlManager>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}