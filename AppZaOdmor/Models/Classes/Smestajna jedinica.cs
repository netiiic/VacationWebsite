using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppZaOdmor.Models.Classes
{
    public class Smestajna_jedinica
    {
        public Smestajna_jedinica()
        {
        }

        public Smestajna_jedinica(int id, int dozvoljenBrojGostiju, bool kucniLjubimci, double cena, int idS)
        {
            Id = id;
            DozvoljenBrojGostiju = dozvoljenBrojGostiju;
            KucniLjubimci = kucniLjubimci;
            Cena = cena;
            IdSmestaj = idS;
        }
        public int Id { get; set; }
        public int DozvoljenBrojGostiju { get; set; } = 0;
        public bool KucniLjubimci { get; set; } = false;
        public double Cena { get; set; } = 0;
        public int IdSmestaj { get; set; }

        public override string ToString()
        {
            return $"{Id}|{DozvoljenBrojGostiju}|{KucniLjubimci}|{Cena}";
        }
    }
}