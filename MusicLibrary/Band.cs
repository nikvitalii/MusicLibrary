using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary
{
    public class Band:Item
    {
        public List<Album> Albums;
        public string Genre;
        public Band(string band, string genre, string album, string year, string song):base(band)
        {
            Genre = genre;
            Albums = new List<Album> { new Album(album, year, song) };
        }
        public Band(string band, string genre):base(band)
        {
            Genre = genre;
            Albums = new List<Album>();
        }

        public Album GetAlbumByName(string album, string year)
        {
            Album alb;
            var albums = Albums.Where(x => x.Name.ToUpper() == album.ToUpper());

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

        public Album ReturnAlbum(string alb)
        {
            var album = Albums.Where(x => x.Name.ToUpper() == alb.ToUpper()).First();
            return album;
        }

        public int DeleteAlbum(string alb)
        {
            var album = Albums.Where(x => x.Name.ToUpper() == alb.ToUpper()).First();
            Albums.Remove(album);
            Library.ExecNonQuery($"DELETE FROM [{Name}] WHERE Name=N'{alb}'");
            Library.ExecNonQuery($"DROP TABLE [{Name}_{alb}]");

            return Albums.Count;
        }

        public void ChangeAlbum(List<string> before, List<string> after)
        {
            if(before[1] != after[1])
            {
                //GetAlbumByName()
            }
        }


    }
}
