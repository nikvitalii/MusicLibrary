using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary
{
    class Band:Item
    {
        public List<Album> Albums;
        public string Genre;
        public Band(string band, string genre, string album, int year, string song):base(band)
        {
            Genre = genre;
            Albums = new List<Album> { new Album(album, year, song) };
        }
    }
}
