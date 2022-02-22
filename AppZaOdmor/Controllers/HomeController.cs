using AppZaOdmor.Models.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace AppZaOdmor.Controllers
{
    public class HomeController : Controller
    {
        public Dictionary<int, Smestajna_jedinica> trenutniJedinica;
        public Dictionary<int, Aranzman> sortirani;
        public Dictionary<int, Aranzman> trenutniAranzman;
        public Dictionary<int, Aranzman> prpAranzman;
        public bool adNaziv;
        public bool adBrGostiju;
        public bool adCena;
        public ActionResult Index()
        {
            Dictionary<int, Aranzman> aranzmani = (Dictionary<int, Aranzman>)HttpContext.Application["aranzmani"];            
            var sorted = from entry in aranzmani orderby entry.Value.DatumPocetka ascending select entry;
            sortirani = sorted.ToDictionary(x => x.Key, x => x.Value);
            ViewBag.Buduci = sortirani;
            adNaziv = true;
            adBrGostiju = true;
            adCena = true;
            Dictionary<int, Smestajna_jedinica> jedinica = (Dictionary<int, Smestajna_jedinica>)HttpContext.Application["jedinica"];
            trenutniJedinica = jedinica.ToDictionary(x => x.Key, x => x.Value);
            
            trenutniAranzman = aranzmani.ToDictionary(x => x.Key, x => x.Value);
            return View();
            
        }

        public ActionResult Prosli()
        {
            Dictionary<int, Aranzman> aranzmani = (Dictionary<int, Aranzman>)HttpContext.Application["aranzmani"];

            var sorted = from entry in aranzmani orderby entry.Value.DatumPocetka descending select entry;
            sortirani = sorted.ToDictionary(x => x.Key, x => x.Value);
            ViewBag.Prosli = sortirani;
            adNaziv = true;
            return View();

        }

        public ActionResult Detaljno()
        {
            Dictionary<int, Aranzman> aranzmani = (Dictionary<int, Aranzman>)HttpContext.Application["aranzmani"];

            var sorted = from entry in aranzmani orderby entry.Value.DatumPocetka ascending select entry;
            sortirani = sorted.ToDictionary(x => x.Key, x => x.Value);
            return View(sortirani.Values);

        }

        public ActionResult ProsliDetaljno()
        {
            Dictionary<int, Aranzman> aranzmani = (Dictionary<int, Aranzman>)HttpContext.Application["aranzmani"];

            var sorted = from entry in aranzmani orderby entry.Value.DatumPocetka descending select entry;
            sortirani = sorted.ToDictionary(x => x.Key, x => x.Value);
            return View(sortirani.Values);

        }

        public ActionResult Logout()
        {
            Session["korisnik"] = null;
            Session["admin"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string kIme, string lozinka)
        {
            Dictionary<string, Korisnik> korisnik = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];

            if(korisnik[kIme].Obrisan == true)
            {
                ViewBag.Message = $"Korisnik ne postoji u sistemu!";
                return View();
            }

            if (korisnik.ContainsKey(kIme))
            {
                if (korisnik[kIme].Lozinka != lozinka)
                {
                    ViewBag.Message = $"Pogresna loznika!";
                    return View();
                }

                if(korisnik[kIme].Uloga == Enums.Uloga.ADMINISTRATOR)
                {
                    Session["admin"] = korisnik[kIme].KorisnickoIme;
                }

                if (korisnik[kIme].Uloga == Enums.Uloga.MENADZER)
                {
                    Session["menadzer"] = korisnik[kIme].KorisnickoIme;
                }

                Session["korisnik"] = korisnik[kIme].KorisnickoIme;
                return RedirectToAction("Index");
            }

            ViewBag.Message = $"Korisnik ne postoji u sistemu!";
            return View();
        }

        public ActionResult Registracija()
        {
            Korisnik korisnik = new Korisnik();
            Session["korisnik"] = korisnik.KorisnickoIme;
            //Session["admin"] = true;
            return View(korisnik);
        }

        [HttpPost]
        public ActionResult Registracija(Korisnik korisnik)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = $"Popunite sva polja!";
                return View();
            }

            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];

            if (korisnici.ContainsKey(korisnik.KorisnickoIme))
            {
                ViewBag.Message = $"Korisnik sa korisnickim imenom vec postoji!";
                return View();
            }

            string[] datum = new string[3];
            datum = korisnik.DatumRodjenja.Split('-');
            string d = datum[2] + "/" + datum[1] + "/" + datum[0];
            korisnik.DatumRodjenja = d;
            korisnik.Id = korisnici.Count + 1;
            //korisnik.Uloga = Enums.Uloga.TURISTA;
            korisnik.BrojOtkazivanja = 0;
            korisnik.Obrisan = false;

            korisnici.Add(korisnik.KorisnickoIme, korisnik);
            Data.SacuvajKorisnika(korisnik);
            Session["korisnik"] = korisnik.KorisnickoIme;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult PregledProfila()
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            ViewBag.CurrentUser = korisnici[(string)Session["korisnik"]];

            return View();
        }
        [HttpPost]
        public ActionResult PregledProfila(Korisnik korisnik)
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            ViewBag.CurrentUser = korisnici[(string)Session["korisnik"]];
            if (korisnici.ContainsKey(korisnik.KorisnickoIme))
            {
                korisnici[korisnik.KorisnickoIme].Lozinka = korisnik.Lozinka;
                korisnici[korisnik.KorisnickoIme].Ime = korisnik.Ime;
                korisnici[korisnik.KorisnickoIme].Prezime = korisnik.Prezime;
                korisnici[korisnik.KorisnickoIme].Pol = korisnik.Pol;
                korisnici[korisnik.KorisnickoIme].Email = korisnik.Email;
                string[] datum = new string[3];
                datum = korisnik.DatumRodjenja.Split('-');
                string d = datum[2] + "/" + datum[1] + "/" + datum[0];
                korisnici[korisnik.KorisnickoIme].DatumRodjenja = d;
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
           
            sw.Close();
            st.Close();

            return View();
        }

        public ActionResult BuduciSortNaziv()
        {
            Dictionary<int, Aranzman> aranzmani = (Dictionary<int, Aranzman>)HttpContext.Application["aranzmani"];
            //aranzmani = trenutniAranzman.ToDictionary(x => x.Key, x => x.Value);
            if (adNaziv)
            {
                var sorted = from entry in aranzmani orderby entry.Value.Naziv ascending select entry;
                aranzmani = sorted.ToDictionary(x => x.Key, x => x.Value);
                adNaziv = false;
            }   
            else
            {
                var sorted = from entry in aranzmani orderby entry.Value.Naziv descending select entry;
                aranzmani = sorted.ToDictionary(x => x.Key, x => x.Value);
                adNaziv = true;
            }
            ViewBag.Buduci = aranzmani;
            trenutniAranzman = aranzmani.ToDictionary(x => x.Key, x => x.Value);
            return View("Index");
        }

        public ActionResult ProsliSortNaziv()
        {
            Dictionary<int, Aranzman> aranzmani = (Dictionary<int, Aranzman>)HttpContext.Application["aranzmani"];
            //aranzmani = trenutniAranzman.ToDictionary(x => x.Key, x => x.Value);
            if (adNaziv)
            {
                var sorted = from entry in aranzmani orderby entry.Value.Naziv ascending select entry;
                aranzmani = sorted.ToDictionary(x => x.Key, x => x.Value);
                adNaziv = false;
            }
            else
            {
                var sorted = from entry in aranzmani orderby entry.Value.Naziv descending select entry;
                aranzmani = sorted.ToDictionary(x => x.Key, x => x.Value);
                adNaziv = true;
            }
            ViewBag.Prosli = aranzmani;
            trenutniAranzman = aranzmani.ToDictionary(x => x.Key, x => x.Value);
            return View("Prosli");
        }

        public ActionResult BuduciSortDatumPocetak()
        {
            Dictionary<int, Aranzman> aranzmani = (Dictionary<int, Aranzman>)HttpContext.Application["aranzmani"];
            //aranzmani = trenutniAranzman.ToDictionary(x => x.Key, x => x.Value);
            if (adNaziv)
            {
                var sorted = from entry in aranzmani orderby entry.Value.DatumPocetka ascending select entry;
                aranzmani = sorted.ToDictionary(x => x.Key, x => x.Value);
                adNaziv = false;
            }
            else
            {
                var sorted = from entry in aranzmani orderby entry.Value.DatumPocetka descending select entry;
                aranzmani = sorted.ToDictionary(x => x.Key, x => x.Value);
                adNaziv = true;
            }
            ViewBag.Buduci = aranzmani;
            trenutniAranzman = aranzmani.ToDictionary(x => x.Key, x => x.Value);
            return View("Index");
        }

        public ActionResult BuduciSortDatumKraj()
        {
            Dictionary<int, Aranzman> aranzmani = (Dictionary<int, Aranzman>)HttpContext.Application["aranzmani"];
            //aranzmani = trenutniAranzman.ToDictionary(x => x.Key, x => x.Value);
            if (adNaziv)
            {
                var sorted = from entry in aranzmani orderby entry.Value.DatumZavrsetka ascending select entry;
                aranzmani = sorted.ToDictionary(x => x.Key, x => x.Value);
                adNaziv = false;
            }
            else
            {
                var sorted = from entry in aranzmani orderby entry.Value.DatumZavrsetka descending select entry;
                aranzmani = sorted.ToDictionary(x => x.Key, x => x.Value);
                adNaziv = true;
            }
            ViewBag.Buduci = aranzmani;
            trenutniAranzman = aranzmani.ToDictionary(x => x.Key, x => x.Value);
            return View("Index");
        }

        public ActionResult ProsliSortDatumPocetak()
        {
            Dictionary<int, Aranzman> aranzmani = (Dictionary<int, Aranzman>)HttpContext.Application["aranzmani"];
            //aranzmani = trenutniAranzman.ToDictionary(x => x.Key, x => x.Value);
            if (adNaziv)
            {
                var sorted = from entry in aranzmani orderby entry.Value.DatumPocetka ascending select entry;
                aranzmani = sorted.ToDictionary(x => x.Key, x => x.Value);
                adNaziv = false;
            }
            else
            {
                var sorted = from entry in aranzmani orderby entry.Value.DatumPocetka descending select entry;
                aranzmani = sorted.ToDictionary(x => x.Key, x => x.Value);
                adNaziv = true;
            }
            ViewBag.Prosli = aranzmani;
            trenutniAranzman = aranzmani.ToDictionary(x => x.Key, x => x.Value);
            return View("Prosli");
        }

        public ActionResult ProsliSortDatumKraj()
        {
            Dictionary<int, Aranzman> aranzmani = (Dictionary<int, Aranzman>)HttpContext.Application["aranzmani"];
            //aranzmani = trenutniAranzman.ToDictionary(x => x.Key, x => x.Value);
            if (adNaziv)
            {
                var sorted = from entry in aranzmani orderby entry.Value.DatumZavrsetka ascending select entry;
                aranzmani = sorted.ToDictionary(x => x.Key, x => x.Value);
                adNaziv = false;
            }
            else
            {
                var sorted = from entry in aranzmani orderby entry.Value.DatumZavrsetka descending select entry;
                aranzmani = sorted.ToDictionary(x => x.Key, x => x.Value);
                adNaziv = true;
            }
            ViewBag.Prosli = aranzmani;
            trenutniAranzman = aranzmani.ToDictionary(x => x.Key, x => x.Value);
            return View("Prosli");
        }

        public ActionResult Smestaj(int id)
        {
            Dictionary<int, Aranzman> aranzmani = (Dictionary<int, Aranzman>)HttpContext.Application["aranzmani"];
            if(aranzmani.ContainsKey(id))
            {
                ViewBag.Smestaj = aranzmani[id].Id;
            }
            return View(aranzmani.Values);
        }
        [HttpGet]
        public ActionResult Pretraga(string tipPrevoza, string tipAranzmana, string naziv)
        {
            Dictionary<int, Aranzman> aranzmani = (Dictionary<int, Aranzman>)HttpContext.Application["aranzmani"];

            var tmp = aranzmani.Where(
                x => /*(DateTime.ParseExact(x.Value.DatumPocetka, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)) <= donjapocetak &&
                    (DateTime.ParseExact(x.Value.DatumPocetka, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)) >= gornjapocetak &&
                    (DateTime.ParseExact(x.Value.DatumZavrsetka, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)) <= donjakraj &&
                    (DateTime.ParseExact(x.Value.DatumZavrsetka, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)) >= gornjakraj &&*/
                    x.Value.TipPrevoza.ToString().ToUpper().Contains(tipPrevoza.ToUpper()) &&
                    x.Value.TipAranzmana.ToString().ToUpper().Contains(tipAranzmana.ToUpper()) &&
                    x.Value.Naziv.ToUpper().Contains(naziv.ToUpper()));

            aranzmani = tmp.ToDictionary(x => x.Key, x => x.Value);
            ViewBag.Buduci = aranzmani;
            trenutniAranzman = aranzmani.ToDictionary(x => x.Key, x => x.Value);
            prpAranzman = aranzmani.ToDictionary(x => x.Key, x => x.Value);
            return View("Index");
        }

        public ActionResult Jedinica(int id)
        {
            Dictionary<int, Aranzman> aranzmani = (Dictionary<int, Aranzman>)HttpContext.Application["aranzmani"];
            if (aranzmani.ContainsKey(id))
            {
                ViewBag.Jedinica = aranzmani[id].Id;
            }
            Dictionary<int, Smestajna_jedinica> jedinice = (Dictionary<int, Smestajna_jedinica>)HttpContext.Application["jedinica"];
            ViewBag.SJedinice = jedinice;
            return View();
        }

        public ActionResult SortBrGostiju()
        {
            Dictionary<int, Smestajna_jedinica> jedinica = (Dictionary<int, Smestajna_jedinica>)HttpContext.Application["jedinica"];
            jedinica = trenutniJedinica.ToDictionary(x => x.Key, x => x.Value);
            if (adBrGostiju)
            {
                var sorted = from entry in jedinica orderby entry.Value.DozvoljenBrojGostiju ascending select entry;
                jedinica = sorted.ToDictionary(x => x.Key, x => x.Value);
                adBrGostiju = false;
            }
            else
            {
                var sorted = from entry in jedinica orderby entry.Value.DozvoljenBrojGostiju descending select entry;
                jedinica = sorted.ToDictionary(x => x.Key, x => x.Value);
                adBrGostiju = true;
            }
            ViewBag.SJedinice = jedinica;
            trenutniJedinica = jedinica.ToDictionary(x => x.Key, x => x.Value);
            return View("Jedinica");
        }

        public ActionResult SortCena()
        {
            Dictionary<int, Smestajna_jedinica> jedinica = (Dictionary<int, Smestajna_jedinica>)HttpContext.Application["jedinica"];
            jedinica = trenutniJedinica.ToDictionary(x => x.Key, x => x.Value);
            if (adBrGostiju)
            {
                var sorted = from entry in jedinica orderby entry.Value.Cena ascending select entry;
                jedinica = sorted.ToDictionary(x => x.Key, x => x.Value);
                adBrGostiju = false;
            }
            else
            {
                var sorted = from entry in jedinica orderby entry.Value.Cena descending select entry;
                jedinica = sorted.ToDictionary(x => x.Key, x => x.Value);
                adBrGostiju = true;
            }
            ViewBag.SJedinice = jedinica;
            trenutniJedinica = jedinica.ToDictionary(x => x.Key, x => x.Value);
            return View("Jedinica");
        }
    }
}