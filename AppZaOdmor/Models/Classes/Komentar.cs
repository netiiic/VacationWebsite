using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppZaOdmor.Models.Classes
{
    public class Komentar
    {
        public Komentar(string turista, Aranzman aranzman, string tekst, int ocena)
        {
            Turista = turista;
            Aranzman = aranzman;
            Tekst = tekst;
            Ocena = ocena;
        }

        [Required]
        public string Turista { get; set; }
        [Required]
        public Aranzman Aranzman { get; set; }
        [Required]
        public string Tekst { get; set; }
        [Required]
        public int Ocena { get; set; }
    }
}