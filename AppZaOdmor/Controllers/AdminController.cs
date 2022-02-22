using AppZaOdmor.Models.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace AppZaOdmor.Controllers
{
    public class AdminController : Controller
    {
        public static bool adIme;
        public static bool adPrezime;
        public static bool adUloga;
        public static Dictionary<string, Korisnik> trenutniKorisnik;
        public static Dictionary<string, Korisnik> prpKorisnik;

        public ActionResult Korisnici()
        {
            Session["admin"] = true;
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            ViewBag.Korisnici = korisnici;

            adIme = true;
            adPrezime = true;
            adUloga = true;
            trenutniKorisnik = korisnici.ToDictionary(x => x.Key, x => x.Value);
            prpKorisnik = korisnici.ToDictionary(x => x.Key, x => x.Value);
            return View();

        }

        public ActionResult SumnjiviKorisnici()
        {
            Session["admin"] = true;
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            ViewBag.Korisnici = korisnici;
            return View(korisnici.Values);
        }
        [HttpPost]
        public ActionResult SumnjiviKorisnici(string kIme)
        {
            Session["admin"] = true;
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];

            foreach (string k in korisnici.Keys)
            {
                if (korisnici.ContainsKey(kIme))
                {
                    korisnici[kIme].Obrisan = true;
                }
            }


            string pt = HostingEnvironment.MapPath("~/App_Data/korisnici.txt");
            FileStream st = new FileStream(pt, FileMode.Create);
            StreamWriter sw = new StreamWriter(st);

            foreach (string k in korisnici.Keys)
            {
                sw.WriteLine($"{korisnici[k].Id}|{korisnici[k].KorisnickoIme}|{korisnici[k].Lozinka}" +
                $"|{korisnici[k].Ime}|{korisnici[k].Prezime}|{korisnici[k].Pol}|{korisnici[k].Email}" +
                $"|{korisnici[k].DatumRodjenja}|{korisnici[k].Uloga}|{korisnici[k].BrojOtkazivanja}|{korisnici[k].Obrisan}");
            }
            /*if (korisnici.ContainsKey(kIme))
            {
                sw.WriteLine($"{korisnici[kIme].Id}|{korisnici[kIme].KorisnickoIme}|{korisnici[kIme].Lozinka}" +
                $"|{korisnici[kIme].Ime}|{korisnici[kIme].Prezime}|{korisnici[kIme].Pol}|{korisnici[kIme].Email}" +
                $"|{korisnici[kIme].DatumRodjenja}|{korisnici[kIme].Uloga}|{korisnici[kIme].BrojOtkazivanja}|{korisnici[kIme].Obrisan}\n");
            }*/


            sw.Close();
            st.Close();

            return View(korisnici.Values);
        }

        public ActionResult SrIme()
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            korisnici = trenutniKorisnik.ToDictionary(x => x.Key, x => x.Value);
            if (adIme)
            {
                var sorted = from entry in korisnici orderby entry.Value.Ime ascending select entry;
                korisnici = sorted.ToDictionary(x => x.Key, x => x.Value);
                adIme = false;
            }
            else
            {
                var sorted = from entry in korisnici orderby entry.Value.Ime descending select entry;
                korisnici = sorted.ToDictionary(x => x.Key, x => x.Value);
                adIme = true;
            }
            ViewBag.Korisnici = korisnici;
            trenutniKorisnik = korisnici.ToDictionary(x => x.Key, x => x.Value);

            return View("Korisnici");
        }

        public ActionResult SPrezime()
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            korisnici = trenutniKorisnik.ToDictionary(x => x.Key, x => x.Value);
            if (adPrezime)
            {
                var sorted = from entry in korisnici orderby entry.Value.Prezime ascending select entry;
                korisnici = sorted.ToDictionary(x => x.Key, x => x.Value);
                adPrezime = false;
            }
            else
            {
                var sorted = from entry in korisnici orderby entry.Value.Prezime descending select entry;
                korisnici = sorted.ToDictionary(x => x.Key, x => x.Value);
                adPrezime = true;
            }
            ViewBag.Korisnici = korisnici;
            trenutniKorisnik = korisnici.ToDictionary(x => x.Key, x => x.Value);
            return View("Korisnici");
        }

        public ActionResult SUloga()
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            korisnici = trenutniKorisnik.ToDictionary(x => x.Key, x => x.Value);
            if (adUloga)
            {
                var sorted = from entry in korisnici orderby entry.Value.Uloga ascending select entry;
                korisnici = sorted.ToDictionary(x => x.Key, x => x.Value);
                adUloga = false;
            }
            else
            {
                var sorted = from entry in korisnici orderby entry.Value.Uloga descending select entry;
                korisnici = sorted.ToDictionary(x => x.Key, x => x.Value);
                adUloga = true;
            }
            ViewBag.Korisnici = korisnici;
            trenutniKorisnik = korisnici.ToDictionary(x => x.Key, x => x.Value);
            return View("Korisnici");
        }
        [HttpGet]
        public ActionResult Pretraga(string ime, string prezime, string uloga)
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];

            var tmp = korisnici.Where
                (x => x.Value.Ime.ToUpper().Contains(ime.ToUpper()) && x.Value.Prezime.ToUpper().Contains(prezime.ToUpper()) && x.Value.Uloga.ToString().ToUpper().Contains(uloga.ToUpper()));


            korisnici = tmp.ToDictionary(x => x.Key, x => x.Value);
            ViewBag.Korisnici = korisnici;

            trenutniKorisnik = korisnici.ToDictionary(x => x.Key, x => x.Value);
            prpKorisnik = korisnici.ToDictionary(x => x.Key, x => x.Value);
            return View("Korisnici");
        }
    }
}