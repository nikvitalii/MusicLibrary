using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary
{
    public class Album:Item
    {
        // Коллекция песен.
        public List<Song> Songs;

        // Год издания альбома.
        public string Year;
        
        public Album(string album, string year):base(album)
        {
            Songs = new List<Song>();
            Year = year;
        }

        // Добавление песни в альбом.
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

        // Удаление песни.
        public void DeleteSong(string band, string song)
        {
            var sng = Songs.Where(x => x.Name.ToUpper() == song.ToUpper()).First();
            Library.ExecNonQuery($"DELETE FROM [{band}_{Name}] WHERE Name=N'{song}'");
            Songs.Remove(sng);

            if (Songs.Count == 0)
                Library.DeleteAlbum(band, Name);
        }

        // Копирование из другого альбома
        public void CopyFrom(string band, Album alb)
        {
            foreach (var song in alb.Songs)
                InsertSong(band, song.Name);
        }
    }
}
