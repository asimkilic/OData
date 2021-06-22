using ODataDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;

namespace ODataDemo
{
    public static class WebApiConfig
    {
       
        public static void Register(HttpConfiguration config)
        {

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Employee>("Employees");
            // Employees controllerdan Employee ile ilgili sorgulamaları yapabileceğimi belirtiyorum
            // Employees nesnesine OData için Employee 'i kullanabilir hale getirmiş oluyoruz.
            builder.EntitySet<Department>("Departments");

            config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
            // ilk o data isim ikinci odata ise nasıl ulaşacağımız /odata diyerek ulaşacağız


            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
