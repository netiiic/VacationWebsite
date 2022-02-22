using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace AppZaOdmor.Models.Classes
{
    public sealed class Data
    {

        public static Dictionary<string, Korisnik> UcitajKorisnika(string ptKorisnik)
        {
            Dictionary<string, Korisnik> korisnici = new Dictionary<string, Korisnik>();          
           
            ptKorisnik = System.Web.Hosting.HostingEnvironment.MapPath(ptKorisnik);
            FileStream fsk = new FileStream(ptKorisnik, FileMode.Open);
            StreamReader srk = new StreamReader(fsk);

            while (true)
            {
                string line = srk.ReadLine();

                if (line == null)
                    break;

                string[] s = line.Split('|');

                korisnici.Add(s[1], new Korisnik(int.Parse(s[0]), s[1], s[2], s[3], s[4],
                    (Enums.Pol)Enum.Parse(typeof(Enums.Pol), s[5]), s[6], s[7],
                    (Enums.Uloga)Enum.Parse(typeof(Enums.Uloga), s[8]), int.Parse(s[9]), bool.Parse(s[10])));
            }

            srk.Close();
            fsk.Close();
            
            return korisnici;
        }

        public static Dictionary<int, Aranzman> UcitajAranzmane(string ptAranzman)
        {
            Dictionary<int, Aranzman> aranzmani = new Dictionary<int, Aranzman>();
            ptAranzman = System.Web.Hosting.HostingEnvironment.MapPath(ptAranzman);
            FileStream fsa = new FileStream(ptAranzman, FileMode.Open);
            StreamReader sra = new StreamReader(fsa);

            while (true)
            {
                string line = sra.ReadLine();

                if (line == null)
                    break;

                string[] s = line.Split('|');

                aranzmani.Add(int.Parse(s[0]), new Aranzman(int.Parse(s[0]), s[1], (Enums.TipAranzmana)Enum.Parse(typeof(Enums.TipAranzmana), s[2]),
                    (Enums.TipPrevoza)Enum.Parse(typeof(Enums.TipPrevoza), s[3]), s[4], s[5], s[6],
                    new Mesto_nalazenja(new Adresa(s[7], s[8], int.Parse(s[9])),
                    double.Parse(s[10]), double.Parse(s[11])), s[12], int.Parse(s[13]), s[14], s[15], s[16], bool.Parse(s[17]), s[18],
                    new Smestaj(s[19], (Enums.TipSmestaja)Enum.Parse(typeof(Enums.TipSmestaja), s[20]),
                    int.Parse(s[21]), bool.Parse(s[22]), bool.Parse(s[23]), bool.Parse(s[24]), bool.Parse(s[25]))));
                
            }

            sra.Close();
            fsa.Close();
            
            return aranzmani;
        }

        public static void SacuvajAranzman(Aranzman aranzman)
        {
            Dictionary<int, Aranzman> aranzmani = (Dictionary<int, Aranzman>)HttpContext.Current.Application["aranzmani"];

            string ptAranzman = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/aranzmani.txt");
            FileStream fsa = new FileStream(ptAranzman, FileMode.Append);
            StreamWriter swa = new StreamWriter(fsa);

            swa.WriteLine( $"{aranzman.Id}|{aranzman.Naziv}|{aranzman.TipAranzmana}|{aranzman.TipPrevoza}" +
                $"|{aranzman.LokacijaPutovanja}|{aranzman.DatumPocetka}|{aranzman.DatumZavrsetka}" +
                $"|{aranzman.MestoNalazenja.Adresa.UlicaIBroj}|{aranzman.MestoNalazenja.Adresa.Mesto}" +
                $"|{aranzman.MestoNalazenja.Adresa.PostanskiBroj}|{aranzman.MestoNalazenja.GeografskaSirina}" +
                $"|{aranzman.MestoNalazenja.GeografskaDuzina}|{aranzman.VremeNalazenja}" +
                $"|{aranzman.MaxBrPutnika}|{aranzman.OpisAranzmana}|{aranzman.ProgramPutovanja}" +
                $"|{aranzman.Slika}|{aranzman.Obrisan}|{aranzman.Menadzer}|{aranzman.Smestaj.Naziv}|{aranzman.Smestaj.TipSmestaja}" +
                $"|{aranzman.Smestaj.BrojZvezdica}|{aranzman.Smestaj.Bazen}" +
                $"|{aranzman.Smestaj.SpaCentar}|{aranzman.Smestaj.ZaInvalide}|{aranzman.Smestaj.Wifi}");            

            swa.Close();
            fsa.Close();

            //Dictionary<string, Aranzman> aranzmani = (Dictionary<string, Aranzman>)HttpContext.Current.Application["aranzmani"];
            aranzmani.Add(aranzman.Id, aranzman);
            HttpContext.Current.Application["aranzmani"] = aranzmani;
        }

        public static void SacuvajKorisnika(Korisnik korisnik)
        {
            string pt = HostingEnvironment.MapPath("~/App_Data/korisnici.txt");
            FileStream st = new FileStream(pt, FileMode.Append);
            StreamWriter sw = new StreamWriter(st);

            sw.WriteLine($"{korisnik.Id}|{korisnik.KorisnickoIme}|{korisnik.Lozinka}" +
                $"|{korisnik.Ime}|{korisnik.Prezime}|{korisnik.Pol}|{korisnik.Email}" +
                $"|{korisnik.DatumRodjenja}|{korisnik.Uloga}|{korisnik.BrojOtkazivanja}|{korisnik.Obrisan}");

            sw.Close();
            st.Close();
        }

        public static Dictionary<int, Smestajna_jedinica> UcitajJedinice(string ptJedinica)
        {
            Dictionary<int, Smestajna_jedinica> jedinice = new Dictionary<int, Smestajna_jedinica>();
            ptJedinica = System.Web.Hosting.HostingEnvironment.MapPath(ptJedinica);
            FileStream fsa = new FileStream(ptJedinica, FileMode.Open);
            StreamReader sra = new StreamReader(fsa);

            while (true)
            {
                string line = sra.ReadLine();

                if (line == null)
                    break;

                string[] s = line.Split('|');

                jedinice.Add(int.Parse(s[0]), new Smestajna_jedinica(int.Parse(s[0]), int.Parse(s[1]), bool.Parse(s[2]), int.Parse(s[3]), int.Parse(s[4])));

            }

            sra.Close();
            fsa.Close();

            return jedinice;
        }

    }
}