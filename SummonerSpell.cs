using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLSSTracker
{
    public class SummonerSpell
    {
        public int cooldown { get; set; }
        public string imageName { get; set; }

        public string imageURL { get; set; }

        public string ToString()
        {
            return imageURL + " : " + cooldown;
        }
    }
}
