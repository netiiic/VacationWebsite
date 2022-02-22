using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppZaOdmor.Models.Classes
{
    public class Adresa
    {
        public Adresa() { }
        public Adresa(string ulicaIBroj, string mesto, int postanskiBroj)
        {
            UlicaIBroj = ulicaIBroj;
            Mesto = mesto;
            PostanskiBroj = postanskiBroj;
        }

        public string UlicaIBroj { get; set; } = "";
        public string Mesto { get; set; } = "";
        public int PostanskiBroj { get; set; } = 0;
    }
}