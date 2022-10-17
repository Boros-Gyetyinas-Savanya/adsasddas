using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jelmez2
{
    public class Kolcsonzesek
    {
        public Ugyfel ugyfel;
        public Jelmez jelmez;
        public int db;
        public int ev;
        public int honap;
        public int nap;
        public int napokSzama;
        public int fizetendo;
        public int napHatra;
        public int evMost;
        public int honapMost;
        public int napMost;
        public int elteltIdo;

        public Kolcsonzesek(int db, int ev, int honap, int nap, int napokSzama, Jelmez jelmez, Ugyfel ugyfel)
        {
            this.jelmez = jelmez;
            this.ugyfel = ugyfel;
            this.db = db;
            this.ev = ev;
            this.honap = honap;
            this.nap = nap;
            this.napokSzama = napokSzama;
            this.fizetendo = napokSzama * jelmez.napiAr;
            
            this.evMost = DateTime.Now.Year;
            this.honapMost = DateTime.Now.Month;
            this.napMost = DateTime.Now.Day;
            this.elteltIdo = this.evMost * 365 - honap * 365 + this.honapMost * 30 - honap * 30 + this.napMost - nap;
            this.napHatra = this.napokSzama - this.elteltIdo;

        }
        

    }
}
