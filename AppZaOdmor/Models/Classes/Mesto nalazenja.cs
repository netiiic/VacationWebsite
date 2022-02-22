using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppZaOdmor.Models.Classes
{
    public class Mesto_nalazenja
    {
        public Mesto_nalazenja() { }
        public Mesto_nalazenja(Adresa adresa, double geografskaDuzina, double geografskaSirina)
        {
            Adresa = adresa;
            GeografskaDuzina = geografskaDuzina;
            GeografskaSirina = geografskaSirina;
        }

        public Adresa Adresa { get; set; }
        public double GeografskaDuzina { get; set; } = 0;
        public double GeografskaSirina { get; set; } = 0;
    }
}