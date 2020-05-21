﻿using System;
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
            var album = ReturnAlbum(alb);
            DeleteAlbum(album);
            return Albums.Count;
        }

        public void DeleteAlbum(Album alb)
        {
            Library.ExecNonQuery($"DELETE FROM [{Name}] WHERE Name=N'{alb.Name}'");
            Library.ExecNonQuery($"DROP TABLE [{Name}_{alb.Name}]");
            Albums.Remove(alb);
            if (Albums.Count == 0)
                Library.DeleteBand(this);
        }



        public void ChangeAlbum(List<string> before, List<string> after, Album albumBefore = null)
        {
            if(albumBefore == null)
                albumBefore = GetAlbumByName(before[1], before[2]);

            var albumAfter = GetAlbumByName(after[1], after[2]);

            albumAfter.CopyFrom(Name, albumBefore);
            DeleteAlbum(albumBefore);
        }


    }
}
