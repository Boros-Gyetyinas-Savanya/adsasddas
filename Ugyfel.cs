using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jelmez2
{
    public class Ugyfel
    {
        public string nev;
        public string cim;
        public string adoszam;
        public string email;
        public Ugyfel(string nev, string cim, string adoszam, string email)
        {
            this.nev = nev;
            this.cim = cim;
            this.adoszam = adoszam;
            this.email = email;
        }
    }
}
