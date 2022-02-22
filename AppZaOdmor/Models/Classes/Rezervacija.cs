using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppZaOdmor.Models.Classes
{
    public class Rezervacija
    {
        public Rezervacija(int id, string idRezervacije, string turista,
            Enums.StatusRezervacija statusRezervacije, Smestajna_jedinica smestajnaJedinica,
            Aranzman aranzman)
        {
            Id = id;
            IdRezervacije = idRezervacije;
            Turista = turista;
            StatusRezervacije = statusRezervacije;
            SmestajnaJedinica = smestajnaJedinica;
            Aranzman = aranzman;
        }

        public int Id { get; set; }
        public string IdRezervacije { get; set; }
        public string Turista { get; set; }
        public Enums.StatusRezervacija StatusRezervacije { get; set; }
        public Smestajna_jedinica SmestajnaJedinica { get; set; }
        public Aranzman Aranzman { get; set; }
    }
}