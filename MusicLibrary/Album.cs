using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary
{
    class Album:Item
    {
        public List<Song> Songs;
        public int Year;

        public Album(string album, int year, string song) : base(album)
        {
            Songs = new List<Song> { new Song(song)};
            Year = year;
        }
    }
}
