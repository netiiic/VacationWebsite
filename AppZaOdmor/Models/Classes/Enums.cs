using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppZaOdmor.Models.Classes
{
    public class Enums
    {
        public enum Pol { Musko, Zensko };
        public enum Uloga { ADMINISTRATOR, MENADZER, TURISTA };
        public enum TipAranzmana { NocenjeSaDoruckom, Polupansion, PunPanison, AllInclusive, NajamApartmana };
        public enum TipPrevoza { Autobus, Avion, AutobusAvion, Individualan, Ostalo };
        public enum TipSmestaja { Hotel, Motel, Vila };
        public enum StatusRezervacija { Aktivna, Otkazana };
    }

}