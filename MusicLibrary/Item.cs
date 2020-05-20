using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary
{
    abstract class Item
    {
        public string Name;

        public Item(string n)
        {
            Name = n;
        }
    }
}
