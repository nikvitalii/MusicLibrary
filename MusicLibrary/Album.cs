using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary
{
    public class Album:Item
    {
        public List<Song> Songs;
        public string Year;

        public Album(string album, string year, string song) : base(album)
        {
            Songs = new List<Song> { new Song(song)};
            Year = year;
        }
        
        public Album(string album, string year):base(album)
        {
            Songs = new List<Song>();
            Year = year;
        }

        public bool InsertSong(string band, string song)
        {
            var songs = Songs.Where(x => x.Name.ToUpper() == song.ToUpper());

            if (songs.Count() == 0)
            {
                Library.ExecNonQuery($"INSERT INTO [{band}_{Name}] VALUES(N'{song}')");
                Songs.Add(new Song(song));
                return true;
            }

            return false;
        }

        public int DeleteSong(string band, string song)
        {
            var sng = Songs.Where(x => x.Name.ToUpper() == song.ToUpper()).First();
            Songs.Remove(sng);
            Library.ExecNonQuery($"DELETE FROM [{band}_{Name}] WHERE Name=N'{song}'");

            return Songs.Count;
        }
    }
}
