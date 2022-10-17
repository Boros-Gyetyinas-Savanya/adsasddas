using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jelmez2
{
    public class Jelmez
    {
        public string jelmezNev;
        public int minMeret;
        public int maxMeret;
        public int napiAr;
        public bool foglalt;

        public Jelmez(string nev, int ar, int min, int max, bool foglalt)
        {
            this.jelmezNev = nev;
            this.minMeret = min;
            this.maxMeret = max;
            this.napiAr = ar;
            this.foglalt = foglalt;
        }
    }
}
