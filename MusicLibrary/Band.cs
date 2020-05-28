using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary
{
    public class Band:Item
    {
        // Коллекция альбомов.
        public List<Album> Albums;

        // Жанр группы.
        public string Genre;

        public Band(string band, string genre):base(band)
        {
            Genre = genre;
            Albums = new List<Album>();
        }

        // Возвращение экземлпяра альбома. 
        // Если такого нет, создается новый.
        public Album ReturnAlbum(string album, string year)
        {
            Album alb;
            var albums = Albums.Where(x => (x.Name.ToUpper() == album.ToUpper() && x.Year == year));

            if (albums.Count() == 0)
            {
                Library.ExecNonQuery($"INSERT INTO [{Name}] VALUES(N'{album}',N'{year}')");
                Library.ExecNonQuery($"CREATE TABLE [{Name}_{album}] ([Name] NVARCHAR(50) NOT NULL)");
                alb = new Album(album, year);
                Albums.Add(alb);
            }
            else
                alb = albums.First();

            return alb;
        }

        // Возвращение экземпляра альбома без создания нового.
        public Album GetAlbumByName(string alb)
        {
            var album = Albums.Where(x => x.Name.ToUpper() == alb.ToUpper()).First();
            return album;
        }

        // Удаление альбома по названию.
        public void DeleteAlbum(string alb)
        {
            var album = GetAlbumByName(alb);
            DeleteAlbum(album);
        }

        // Удаление альбома по экземпляру класса.
        public void DeleteAlbum(Album alb)
        {
            Library.ExecNonQuery($"DELETE FROM [{Name}] WHERE (Name=N'{alb.Name}' and Year=N'{alb.Year}')");
            Library.ExecNonQuery($"DROP TABLE [{Name}_{alb.Name}]");
            Albums.Remove(alb);
            if (Albums.Count == 0)
                Library.DeleteBand(this);
        }

        // Изменение параметров альбома.
        public void ChangeAlbum(List<string> before, List<string> after, Album albumBefore = null, Band bandBefore = null)
        {
            if(albumBefore == null)
                albumBefore = ReturnAlbum(before[1], before[2]);

            var albumAfter = ReturnAlbum(after[1], after[2]);

            albumAfter.CopyFrom(Name, albumBefore);

            if (bandBefore != null)
                bandBefore.DeleteAlbum(albumBefore);
            else
                DeleteAlbum(albumBefore);
        }
    }
}
