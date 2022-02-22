using AppZaOdmor.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AppZaOdmor
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Dictionary<string, Korisnik> korisnici = Data.UcitajKorisnika("~/App_Data/korisnici.txt");
            HttpContext.Current.Application["korisnici"] = korisnici;

            Dictionary<int, Aranzman> aranzmani = Data.UcitajAranzmane("~/App_Data/aranzmani.txt");
            HttpContext.Current.Application["aranzmani"] = aranzmani;

            Dictionary<int, Smestajna_jedinica> jedinica = Data.UcitajJedinice("~/App_Data/smestajnajedinica.txt");
            HttpContext.Current.Application["jedinica"] = jedinica;


        }

        /*protected void Session_Start()
        {
            try
            {
                Data.Instance.Ucitaj();
            }
            catch
            {
                HttpContext.Current.Application["korisnici"] = null;
                //HttpContext.Current.Application["aranzmani"] = null;

            }
        }*/
    }
}
