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
    public class MenadzerController : Controller
    {
        public ActionResult DodajAranzman()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DodajAranzman(Aranzman aranzman)
        {

            Dictionary<int, Aranzman> aranzmani = (Dictionary<int, Aranzman>)HttpContext.Application["aranzmani"];
            string[] pocetak = new string[5];
            pocetak = aranzman.DatumPocetka.Split('-', 'T', ':');
            aranzman.DatumPocetka = pocetak[2] + "/" + pocetak[1] + "/" + pocetak[0];

            string[] kraj = new string[5];
            kraj = aranzman.DatumZavrsetka.Split('-', 'T', ':');
            aranzman.DatumZavrsetka = kraj[2] + "/" + kraj[1] + "/" + kraj[0];

            aranzman.Id = aranzmani.Count + 1;
            aranzman.Obrisan = false;
            aranzman.Menadzer = (string)Session["korisnik"];
            //aranzmani.Add(aranzman.Id, aranzman);
            Data.SacuvajAranzman(aranzman);
            Session["aranzman"] = aranzman.Id;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult VasiAranzmani()
        {
            Dictionary<int, Aranzman> aranzmani = (Dictionary<int, Aranzman>)HttpContext.Application["aranzmani"];

            return View(aranzmani.Values);
        }

        public ActionResult IzmeniAranzman(int id)
        {
            Dictionary<int, Aranzman> aranzman = (Dictionary<int, Aranzman>)HttpContext.Application["aranzmani"];
            if(aranzman.ContainsKey(id))
            {
                TempData["Ar"] = aranzman[id];
            }
            return View();
        }

        [HttpPost]
        public ActionResult IzmeniAranzman(Aranzman aranzman)
        {
            Dictionary<int, Aranzman> aranzmani = (Dictionary<int, Aranzman>)HttpContext.Application["aranzmani"];
            if(aranzmani.ContainsKey(aranzman.Id))
            {
                aranzmani[aranzman.Id].Naziv = aranzman.Naziv;
                aranzmani[aranzman.Id].TipAranzmana = aranzman.TipAranzmana;
                aranzmani[aranzman.Id].TipPrevoza = aranzman.TipPrevoza;
                aranzmani[aranzman.Id].LokacijaPutovanja = aranzman.LokacijaPutovanja;
                string[] pocetak = new string[5];
                pocetak = aranzman.DatumPocetka.Split('-', 'T', ':');
                string dp = pocetak[2] + "/" + pocetak[1] + "/" + pocetak[0];
                aranzmani[aranzman.Id].DatumPocetka = dp;
                string[] kraj = new string[5];
                kraj = aranzman.DatumPocetka.Split('-', 'T', ':');
                string dk = kraj[2] + "/" + kraj[1] + "/" + kraj[0];
                aranzmani[aranzman.Id].DatumZavrsetka = dk;
                aranzmani[aranzman.Id].MestoNalazenja.Adresa.UlicaIBroj = aranzman.MestoNalazenja.Adresa.UlicaIBroj;
                aranzmani[aranzman.Id].MestoNalazenja.Adresa.Mesto = aranzman.MestoNalazenja.Adresa.Mesto;
                aranzmani[aranzman.Id].MestoNalazenja.Adresa.PostanskiBroj = aranzman.MestoNalazenja.Adresa.PostanskiBroj;
                aranzmani[aranzman.Id].MestoNalazenja.GeografskaSirina = aranzman.MestoNalazenja.GeografskaSirina;
                aranzmani[aranzman.Id].MestoNalazenja.GeografskaDuzina = aranzman.MestoNalazenja.GeografskaDuzina;
                aranzmani[aranzman.Id].VremeNalazenja = aranzman.VremeNalazenja;
                aranzmani[aranzman.Id].MaxBrPutnika = aranzman.MaxBrPutnika;
                aranzmani[aranzman.Id].OpisAranzmana = aranzman.OpisAranzmana;
                aranzmani[aranzman.Id].ProgramPutovanja = aranzman.ProgramPutovanja;
                aranzmani[aranzman.Id].Slika = aranzman.Slika;
            }

            string pt = HostingEnvironment.MapPath("~/App_Data/aranzmani.txt");
            FileStream st = new FileStream(pt, FileMode.Create);
            StreamWriter sw = new StreamWriter(st);

            foreach (int a in aranzmani.Keys)
            {
                sw.WriteLine($"{aranzmani[a].Id}|{aranzmani[a].Naziv}|{aranzmani[a].TipAranzmana}|{aranzmani[a].TipPrevoza}" +
                $"|{aranzmani[a].LokacijaPutovanja}|{aranzmani[a].DatumPocetka}|{aranzmani[a].DatumZavrsetka}" +
                $"|{aranzmani[a].MestoNalazenja.Adresa.UlicaIBroj}|{aranzmani[a].MestoNalazenja.Adresa.Mesto}" +
                $"|{aranzmani[a].MestoNalazenja.Adresa.PostanskiBroj}|{aranzmani[a].MestoNalazenja.GeografskaSirina}" +
                $"|{aranzmani[a].MestoNalazenja.GeografskaDuzina}|{aranzmani[a].VremeNalazenja}" +
                $"|{aranzmani[a].MaxBrPutnika}|{aranzmani[a].OpisAranzmana}|{aranzmani[a].ProgramPutovanja}" +
                $"|{aranzmani[a].Slika}|{aranzmani[a].Obrisan}|{aranzmani[a].Menadzer}|{aranzmani[a].Smestaj.Naziv}|{aranzmani[a].Smestaj.TipSmestaja}" +
                $"|{aranzmani[a].Smestaj.BrojZvezdica}|{aranzmani[a].Smestaj.Bazen}" +
                $"|{aranzmani[a].Smestaj.SpaCentar}|{aranzmani[a].Smestaj.ZaInvalide}|{aranzmani[a].Smestaj.Wifi}");
            }

            sw.Close();
            st.Close();

            return RedirectToAction("VasiAranzmani", "Menadzer");
        }

        [HttpPost]
        public ActionResult Obrisi(int id)
        {
            Dictionary<int, Aranzman> aranzman = (Dictionary<int, Aranzman>)HttpContext.Application["aranzmani"];

            if(aranzman.ContainsKey(id))
            {
                aranzman[id].Obrisan = true;
            }

            string pt = HostingEnvironment.MapPath("~/App_Data/aranzmani.txt");
            FileStream st = new FileStream(pt, FileMode.Create);
            StreamWriter sw = new StreamWriter(st);

            foreach(int a in aranzman.Keys)
            {
                sw.WriteLine($"{aranzman[a].Id}|{aranzman[a].Naziv}|{aranzman[a].TipAranzmana}|{aranzman[a].TipPrevoza}" +
                $"|{aranzman[a].LokacijaPutovanja}|{aranzman[a].DatumPocetka}|{aranzman[a].DatumZavrsetka}" +
                $"|{aranzman[a].MestoNalazenja.Adresa.UlicaIBroj}|{aranzman[a].MestoNalazenja.Adresa.Mesto}" +
                $"|{aranzman[a].MestoNalazenja.Adresa.PostanskiBroj}|{aranzman[a].MestoNalazenja.GeografskaSirina}" +
                $"|{aranzman[a].MestoNalazenja.GeografskaDuzina}|{aranzman[a].VremeNalazenja}" +
                $"|{aranzman[a].MaxBrPutnika}|{aranzman[a].OpisAranzmana}|{aranzman[a].ProgramPutovanja}" +
                $"|{aranzman[a].Slika}|{aranzman[a].Obrisan}|{aranzman[a].Menadzer}|{aranzman[a].Smestaj.Naziv}|{aranzman[a].Smestaj.TipSmestaja}" +
                $"|{aranzman[a].Smestaj.BrojZvezdica}|{aranzman[a].Smestaj.Bazen}" +
                $"|{aranzman[a].Smestaj.SpaCentar}|{aranzman[a].Smestaj.ZaInvalide}|{aranzman[a].Smestaj.Wifi}");
            }

            sw.Close();
            st.Close();

            return RedirectToAction("VasiAranzmani", "Menadzer");
        }

    }
}