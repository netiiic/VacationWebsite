using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace AppZaOdmor.Models.Classes
{
    public class Aranzman
    {
        public Aranzman() { }

        public Aranzman(int id, string naziv, Enums.TipAranzmana tipAranzmana, Enums.TipPrevoza tipPrevoza, string lokacijaPutovanja, 
            string datumPocetka, string datumZavrsetka, Mesto_nalazenja mestoNalazenja, string vremeNalazenja, int maxBrPutnika, 
            string opisAranzmana, string programPutovanja, string slika, bool obrisan, string menadzer, Smestaj smestaj)
        {
            Id = id;
            Naziv = naziv;
            TipAranzmana = tipAranzmana;
            TipPrevoza = tipPrevoza;
            LokacijaPutovanja = lokacijaPutovanja;
            DatumPocetka = datumPocetka;
            DatumZavrsetka = datumZavrsetka;
            MestoNalazenja = mestoNalazenja;
            VremeNalazenja = vremeNalazenja;
            MaxBrPutnika = maxBrPutnika;
            OpisAranzmana = opisAranzmana;
            ProgramPutovanja = programPutovanja;
            Slika = slika;            
            Obrisan = obrisan;
            Menadzer = menadzer;
            Smestaj = smestaj;
        }



        //[Required]
        public int Id { get; set; } = 0;
        //[Required]
        public string Naziv { get; set; } = "";
        //[Required]
        public Enums.TipAranzmana TipAranzmana { get; set; } = Enums.TipAranzmana.Polupansion;
        //[Required]
        public Enums.TipPrevoza TipPrevoza { get; set; } = Enums.TipPrevoza.Ostalo;
        //[Required]
        public string LokacijaPutovanja { get; set; } = "";
        //[Required]
        public string DatumPocetka { get; set; }
        //[Required]
        public string DatumZavrsetka { get; set; }
        //[Required]
        public Mesto_nalazenja MestoNalazenja { get; set; }
        //[Required]
        public string VremeNalazenja { get; set; }
        //[Required]
        public int MaxBrPutnika { get; set; } = 0;
        //[Required]
        public string OpisAranzmana { get; set; } = "";
        //[Required]
        public string ProgramPutovanja { get; set; } = "";
        
        public string Slika { get; set; } = "";
        
        public Smestaj Smestaj { get; set; }
        public bool Obrisan { get; set; } = false;
        public string Menadzer { get; set; } = "";

        /*public static void NapraviAranzman(Aranzman aranzman)
        {
            string pt = HostingEnvironment.MapPath("~/App_Data/aranzmani.txt");
            FileStream st = new FileStream(pt, FileMode.Append);
            StreamWriter sw = new StreamWriter(st);

            sw.WriteLine($"{aranzman.Id}|{aranzman.Naziv}|{aranzman.TipAranzmana}|{aranzman.TipPrevoza}" +
                $"|{aranzman.LokacijaPutovanja}|{aranzman.DatumPocetka}|{aranzman.DatumZavrsetka}" +
                $"|{aranzman.MestoNalazenja.Adresa.UlicaIBroj}|{aranzman.MestoNalazenja.Adresa.Mesto}" +
                $"|{aranzman.MestoNalazenja.Adresa.PostanskiBroj}|{aranzman.MestoNalazenja.GeografskaSirina}" +
                $"|{aranzman.MestoNalazenja.GeografskaDuzina}|{aranzman.VremeNalazenja}" +
                $"|{aranzman.MaxBrPutnika}|{aranzman.OpisAranzmana}|{aranzman.ProgramPutovanja}" +
                $"|{aranzman.Slika}|{aranzman.Smestaj.TipSmestaja}|{aranzman.Smestaj.Naziv}" +
                $"|{aranzman.Smestaj.Id}|{aranzman.Smestaj.BrojZvezdica}|{aranzman.Smestaj.Bazen}" +
                $"|{aranzman.Smestaj.SpaCentar}|{aranzman.Smestaj.ZaInvalide}|{aranzman.Smestaj.Wifi}" +
                $"|{aranzman.Smestaj.SmestajnaJedinica.DozvoljenBrojGostiju}|{aranzman.Smestaj.SmestajnaJedinica.KucniLjubimci}" +
                $"|{aranzman.Smestaj.SmestajnaJedinica.Cena}|{aranzman.Obrisan}");

            sw.Close();
            st.Close();
        }*/

        

    }
}