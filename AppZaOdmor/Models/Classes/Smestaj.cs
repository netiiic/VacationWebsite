using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppZaOdmor.Models.Classes
{
    public class Smestaj
    {
        public Smestaj() { }

        public Smestaj(string naziv, Enums.TipSmestaja tipSmestaja, int brojZvezdica, bool bazen, bool spaCentar, bool zaInvalide, bool wifi)
        {
            Naziv = naziv;
            TipSmestaja = tipSmestaja;
            BrojZvezdica = brojZvezdica;
            Bazen = bazen;
            SpaCentar = spaCentar;
            ZaInvalide = zaInvalide;
            Wifi = wifi;
        }

        public string Naziv { get; set; } = "";
        public Enums.TipSmestaja TipSmestaja { get; set; } = Enums.TipSmestaja.Hotel;
        public int BrojZvezdica { get; set; } = 0;
        public bool Bazen { get; set; } = false;
        public bool SpaCentar { get; set; } = false;
        public bool ZaInvalide { get; set; } = false;
        public bool Wifi { get; set; } = false;
        public List<Smestajna_jedinica> Smestajna_Jedinica { get; set; } = new List<Smestajna_jedinica>();
        public Smestajna_jedinica SmestajnaJedinica { get; set; }

    }
}