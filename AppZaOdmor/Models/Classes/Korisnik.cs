using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace AppZaOdmor.Models.Classes
{
    public class Korisnik
    {
        public Dictionary<string, Korisnik> korisnici { get; set; }

        public Korisnik()
        {
        }

        public Korisnik(int id, string korisnickoIme, string lozinka, string ime, string prezime,
            Enums.Pol pol, string email, string datumRodjenja, Enums.Uloga uloga,
            int brojOtkazivanja, bool obrisan)
        {
            Id = id;
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
            Ime = ime;
            Prezime = prezime;
            Pol = pol;
            Email = email;
            DatumRodjenja = datumRodjenja;
            Uloga = uloga;
            BrojOtkazivanja = brojOtkazivanja;
            Obrisan = obrisan;
        }

        /* public Korisnik(int id, string korisnickoIme, string lozinka, string ime, string prezime, 
             Enums.Pol pol, string email, DateTime datumRodjenja, Enums.Uloga uloga, 
             List<Rezervacija> rezervacije, List<Aranzman> aranzmani, int brojOtkazivanja)
         {
             Id = id;
             KorisnickoIme = korisnickoIme;
             Lozinka = lozinka;
             Ime = ime;
             Prezime = prezime;
             Pol = pol;
             Email = email;
             DatumRodjenja = datumRodjenja;
             Uloga = uloga;
             Rezervacije = rezervacije;
             Aranzmani = aranzmani;
             BrojOtkazivanja = brojOtkazivanja;
         }*/


        public int Id { get; set; }
        [Required]
        public string KorisnickoIme { get; set; }
        [Required]
        public string Lozinka { get; set; }
        [Required]
        public string Ime { get; set; }
        [Required]
        public string Prezime { get; set; }
        [Required]
        public Enums.Pol Pol { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string DatumRodjenja { get; set; }

        public Enums.Uloga Uloga { get; set; }

        public List<Rezervacija> Rezervacije { get; set; }

        public List<Aranzman> Aranzmani { get; set; }
        public int BrojOtkazivanja { get; set; }
        public bool Obrisan { get; set; }

        /*public static void SacuvajKorisnika(Korisnik korisnik)
        {
            string pt = HostingEnvironment.MapPath("~/App_Data/korisnici.txt");
            FileStream st = new FileStream(pt, FileMode.Append);
            StreamWriter sw = new StreamWriter(st);

            sw.WriteLine($"{korisnik.Id}|{korisnik.KorisnickoIme}|{korisnik.Lozinka}" +
                $"|{korisnik.Ime}|{korisnik.Prezime}|{korisnik.Pol}|{korisnik.Email}" +
                $"|{korisnik.DatumRodjenja}|{korisnik.Uloga}|{korisnik.BrojOtkazivanja}");

            sw.Close();
            st.Close();
        }

        public static List<Korisnik> UcitajKorisnike(string pt)
        {
            List<Korisnik> korisnici = new List<Korisnik>();
            pt = HostingEnvironment.MapPath(pt);
            FileStream fs = new FileStream(pt, FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                string[] s = line.Split('|');
                Korisnik k = new Korisnik(int.Parse(s[0]), s[1], s[2], s[3], s[4], 
                    (Enums.Pol)Enum.Parse(typeof(Enums.Pol), s[5]), s[6], DateTime.Parse(s[7]),
                    (Enums.Uloga)Enum.Parse(typeof(Enums.Uloga), s[8]), int.Parse(s[9]));

                korisnici.Add(k);
            }

            sr.Close();
            fs.Close();
            return korisnici;
        }*/
    }
}