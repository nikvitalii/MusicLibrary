using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicLibrary
{
    public static class Library
    {
        
        public static string ConnectionStr = 
            $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
            {Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), 
            "data\\MusicLibrary.mdf")};Integrated Security = True; Connect Timeout = 30";
        public static List<Band> Bands;


        static public void LoadDatabase()
        {
            List<string[]> bands = new List<string[]>(ListStringArrayRead("SELECT * FROM BandName"));
            Bands = new List<Band>();
            foreach(var band in bands)
            {
                Bands.Add(new Band(band[0], band[1]));
                List<string[]> albums = new List<string[]>(ListStringArrayRead($"SELECT * FROM [{band[0]}]"));

                foreach(var album in albums)
                {
                    Bands[Bands.Count - 1].Albums.Add(new Album(album[0], album[1]));
                    List<string> songs = new List<string>(ListStringRead($"SELECT * FROM [{band[0]}_{album[0]}]"));

                    foreach(var song in songs)
                        Bands[Bands.Count - 1].Albums[Bands[Bands.Count - 1].Albums.Count - 1].Songs.Add(new Song(song)); 
                }
            }
        }
        static public List<Band> FullDatabase()
        {
            return Bands;
        }


        static public Band GetBandByName(string band, string genre)
        {
            var bands = Bands.Where(x => x.Name.ToUpper() == band.ToUpper());
            Band bnd;

            if (bands.Count() == 0)
            {
                ExecNonQuery($"INSERT INTO BandName VALUES(N'{band}',N'{genre}')");
                ExecNonQuery($"CREATE TABLE [{band}] ([Name] NVARCHAR(50) NOT NULL PRIMARY KEY , [Genre]NVARCHAR(50) NOT NULL)");
                bnd = new Band(band, genre);
                Bands.Add(bnd);
            }
            else
                bnd = bands.First();

            return bnd;
        }

        static public void AddItem(string song, string album, string band, string year, string genre)
        {
            var bnd = GetBandByName(band, genre);

            var alb = bnd.GetAlbumByName(album, year);

            if (alb.InsertSong(band, song))
                Added();
            else
                Exists();
        }

        static public List<string[]> Search(List<string> search)
        {
            List<string[]> result = new List<string[]>();
            foreach (var band in Bands)
                if ((band.Name.ToUpper().Contains(search[3].ToUpper()) || search[3] == "Введите текст" || search[3].Length == 0)
                    && (band.Genre.ToUpper().Contains(search[4].ToUpper()) || search[4] == "Введите текст" || search[4].Length == 0))
                    foreach (var album in band.Albums)
                        if ((album.Name.ToUpper().Contains(search[1].ToUpper()) || search[1] == "Введите текст" || search[1].Length == 0)
                            && (album.Year.ToUpper().Contains(search[2].ToUpper()) || search[2] == "Введите текст" || search[2].Length == 0))
                            foreach (var song in album.Songs)
                                if (song.Name.ToUpper().Contains(search[0].ToUpper()) || search[0] == "Введите текст" || search[0].Length == 0)
                                    result.Add(new string[] { song.Name, album.Name, album.Year, band.Name, band.Genre });

            return result;
        }


        #region DELETE
        static public void DeleteSong(List<string> list)
        {
            var band = Bands.Where(x => x.Name.ToUpper() == list[0].ToUpper()).First();
            var album = band.ReturnAlbum(list[1]);
            album.DeleteSong(list[0], list[2]);

        }

        static public void DeleteAlbum(string band, string album)
        {
            var bnd = Bands.Where(x => x.Name.ToUpper() == band.ToUpper()).First();
            if (bnd.DeleteAlbum(album) == 0)
                DeleteBand(band);
        }


        static public void DeleteBand(Band band)
        {
            foreach (var alb in band.Albums)
                ExecNonQuery($"DROP TABLE [{band.Name}_{alb.Name}]");
            Bands.Remove(band);
            ExecNonQuery($"DELETE FROM BandName WHERE Band=N'{band.Name}'");
            ExecNonQuery($"DROP TABLE [{band.Name}]");
        }
        
        static public void DeleteBand(string band)
        {
            var bnd = Bands.Where(x => x.Name.ToUpper() == band.ToUpper()).First();
            DeleteBand(bnd);
        }
        #endregion

        static public void ChangeSong(List<string> before, List<string> after)
        {
            DeleteSong(new List<string> { before[3], before[1], before[0] });
            AddItem(after[0], after[1], after[3], after[2], after[4]);
        }

        static public void ChangeAlbum(List<string> before, List<string> after)
        {
            Band bandBefore = null;
            Band bandAfter = null;

            bandBefore = GetBandByName(before[3], before[4]);
            bandAfter = GetBandByName(after[3], after[4]);
            if (before[3] == after[3])
            {
                bandBefore.ChangeAlbum(before, after);
            }
            else
            {
                Album alb = bandBefore.GetAlbumByName(before[1], before[2]);
                bandAfter.ChangeAlbum(before, after, alb);
            }
        }


        /*static public void ChangeAlbum(List<string> before, List<string> after)
        {
            string query = $"SELECT * FROM [{before[3]}_{before[1]}]";
            SqlCommand command = new SqlCommand(query, Connection);
            List<string> songs = new List<string>();
            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                        songs.Add(reader[0].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            if (before[3] != after[3])
            {
                query = $"IF NOT EXISTS(SELECT * FROM Bandname WHERE Band=N'{after[3]}') " +
                        $" BEGIN " +
                        $"CREATE TABLE [{after[3]}] ([Name] NVARCHAR(50) NOT NULL PRIMARY KEY , [Year]NVARCHAR(50) NOT NULL);" +
                        $"INSERT INTO BandName VALUES (N'{after[3]}', N'{after[4]}')" +
                        $" END";
                command = new SqlCommand(query, Connection);
                command.ExecuteNonQuery();

                query = $"IF NOT EXISTS(SELECT * FROM [{after[3]}] WHERE Name=N'{after[1]}')" +
                        $" BEGIN " +
                        $" EXEC sp_rename [{before[3]}_{before[1]}] ,[{after[3]}_{after[1]}];" +
                        $" INSERT INTO [{after[3]}] VALUES (N'{after[1]}', N'{after[2]}')" +
                        $" END";
                command = new SqlCommand(query, Connection);
                command.ExecuteNonQuery();

                query = $"IF NOT EXISTS(SELECT * FROM [{before[3]}] WHERE Name != N'{before[1]}')" +
                        $" BEGIN " +
                        $"DROP TABLE [{before[3]}];" +
                        $"DELETE FROM Bandname WHERE Band=N'{before[3]}'" +
                        $" END";
                command = new SqlCommand(query, Connection);
                command.ExecuteNonQuery();

                foreach (var song in songs)
                {
                    query = $"IF NOT EXISTS(SELECT * FROM [{after[3]}_{after[1]}] WHERE Name='{song}')" +
                            $"INSERT INTO [{after[3]}_{after[1]}] VALUES (N'{song}')";
                    command = new SqlCommand(query, Connection);
                    command.ExecuteNonQuery();
                }

                query = $"IF EXISTS(SELECT * FROM sysobjects WHERE name='{before[3]}_{before[1]}' and xtype='U') " +
                        $" BEGIN " +
                        $"DROP TABLE [{before[3]}_{before[1]}];" +
                        $" END";
                command = new SqlCommand(query, Connection);
                command.ExecuteNonQuery();
            }
            else if (before[1] != after[1])
            {
                query = $"IF NOT EXISTS(SELECT * FROM [{after[3]}] WHERE Name=N'{after[1]}')" +
                        $" BEGIN " +
                        $" EXEC sp_rename [{before[3]}_{before[1]}] ,[{after[3]}_{after[1]}];" +
                        $" INSERT INTO [{after[3]}] VALUES (N'{after[1]}', N'{after[2]}')" +
                        $" DELETE FROM [{before[3]}] WHERE Name = N'{before[1]}'" +
                        $" END";
                command = new SqlCommand(query, Connection);
                command.ExecuteNonQuery();

                foreach (var song in songs)
                {
                    query = $"IF NOT EXISTS(SELECT * FROM [{after[3]}_{after[1]}] WHERE Name='{song}')" +
                            $"INSERT INTO [{after[3]}_{after[1]}] VALUES (N'{song}')";
                    command = new SqlCommand(query, Connection);
                    command.ExecuteNonQuery();
                }
            }
            if (before[2] != after[2])
            {
                query = $"IF EXISTS(SELECT * FROM [{after[3]}] WHERE Name=N'{after[1]}')" +
                        $" BEGIN " +
                        $" DELETE FROM [{after[3]}] WHERE Name = N'{after[1]}'" +
                        $" INSERT INTO [{after[3]}] VALUES (N'{after[1]}', N'{after[2]}')" +
                        $" END";
                command = new SqlCommand(query, Connection);
                command.ExecuteNonQuery();
            }
        }*/
        /*static public void ChangeBand(List<string> before, List<string> after)
        {
            List<string[]> albums = new List<string[]>();
            string query = $"SELECT * FROM [{before[3]}]";
            SqlCommand command = new SqlCommand(query, Connection);
            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                        albums.Add(new string[] { reader[0].ToString(), reader[1].ToString() });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            if (before[3] != after[3])
            {
                query = $"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='{after[3]}') " +
                            $" BEGIN " +
                            $" CREATE TABLE '{after[3]}' ([Name] NVARCHAR(50) NOT NULL PRIMARY KEY , [Genre]NVARCHAR(50) NOT NULL)" +
                            $" DELETE FROM Bandname WHERE Band=N'{before[3]}'" +

                            $" INSERT INTO BandName VALUES (N'{after[3]}', N'{after[4]}')" +
                            $" END";
                new SqlCommand(query, Connection);

                foreach (var album in albums)
                {
                    query = $"SELECT * FROM [{before[3]}_{album[0]}]";
                    command = new SqlCommand(query, Connection);
                    List<string> songs = new List<string>();
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                            while (reader.Read())
                                songs.Add(reader[0].ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw;
                    }

                    query = $"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='{after[3]}_{album[0]}' and xtype='U') " +
                            $" BEGIN " +
                            $" DROP TABLE [{before[3]}_{album[0]}]" +
                            $" CREATE TABLE [{after[3]}_{album[0]}] ([Name] NVARCHAR(50) NOT NULL)" +
                            $" INSERT INTO [{after[3]}] VALUES (N'{album[0]}', N'{album[1]}')" +
                            $" END";
                    command = new SqlCommand(query, Connection);
                    command.ExecuteNonQuery();

                    foreach (var song in songs)
                    {
                        query = $"IF NOT EXISTS(SELECT * FROM [{after[3]}_{album[0]}] WHERE Name='{song}')" +
                                $"INSERT INTO [{after[3]}_{album[0]}] VALUES (N'{song}')";
                        command = new SqlCommand(query, Connection);
                        command.ExecuteNonQuery();
                    }
                    query = $"IF EXISTS(SELECT * FROM sysobjects WHERE name='{before[3]}_{album[0]}' and xtype='U')" +
                            $"DROP TABLE [{before[3]}_{album[0]}]";
                    command = new SqlCommand(query, Connection);
                    command.ExecuteNonQuery();
                }
                query = $"IF EXISTS(SELECT * FROM sysobjects WHERE name='{before[3]}' and xtype='U')" +
                        $"DROP TABLE [{before[3]}]";
                command = new SqlCommand(query, Connection);
                command.ExecuteNonQuery();

                query = $"IF EXISTS(SELECT * FROM Bandname WHERE Band={before[3]})" +
                        $"DELETE FROM Bandname WHERE Band = {before[3]}";
                command = new SqlCommand(query, Connection);
                command.ExecuteNonQuery();
            }

        }*/

        //messages
        static public void Added()
        {
            MessageBox.Show(
                "Песня добавлена в коллекцию",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        static public void Exists()
        {
            MessageBox.Show(
               "Песня уже есть в коллекции",
               "Ошибка",
               MessageBoxButtons.OK,
               MessageBoxIcon.Error
           );
        }

        //save
        static public void SaveToFile(List<Band> list, bool filtered = false, List<string> filters = null)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text file*|.txt";
            dialog.ShowDialog();
            if (dialog.FileName.Length > 1)
            {
                using (StreamWriter wr = new StreamWriter(dialog.FileName))
                {
                    if (filtered && filters != null)
                    {
                        wr.WriteLine("Фильтры:");
                        wr.WriteLine("{0, -15} {1, -15 }{2, -15} {3, -15} {4, -15}",
                            "Песня", "Альбом", "Год", "Автор", "Жанр");

                        wr.WriteLine("{0, -15} {1, -15 }{2, -15} {3, -15} {4, -15}\n\n",
                            filters[0], filters[1], filters[2], filters[3], filters[4]);
                    }


                    wr.WriteLine("{0, -15} {1, -15 }{2, -15} {3, -15} {4, -15}\n", "Песня", "Альбом", "Год", "Автор", "Жанр");
                    foreach(var band in list)
                        foreach(var album in band.Albums)
                            foreach (var song in album.Songs)
                                wr.WriteLine("{0, -15} {1, -15 }{2, -15} {3, -15} {4, -15}",
                                            song.Name, album.Name, album.Year, band.Name, band.Genre);
                }
                MessageBox.Show(
                   "Файл сохранен",
                   "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
            }

        }
        static public void SaveToFile(List<string[]> list, bool filtered = false, List<string> filters = null)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text file*|.txt";
            dialog.ShowDialog();
            if (dialog.FileName.Length > 1)
            {
                using (StreamWriter wr = new StreamWriter(dialog.FileName))
                {
                    if (filtered && filters != null)
                    {
                        wr.WriteLine("Фильтры:");
                        wr.WriteLine("{0, -15} {1, -15 }{2, -15} {3, -15} {4, -15}",
                            "Песня", "Альбом", "Год", "Автор", "Жанр");

                        wr.WriteLine("{0, -15} {1, -15 }{2, -15} {3, -15} {4, -15}\n\n",
                            filters[0], filters[1], filters[2], filters[3], filters[4]);
                    }


                    wr.WriteLine("{0, -15} {1, -15 }{2, -15} {3, -15} {4, -15}\n", "Песня", "Альбом", "Год", "Автор", "Жанр");
                    foreach (var str in list)
                        wr.WriteLine("{0, -15} {1, -15 }{2, -15} {3, -15} {4, -15}",
                                    str[0], str[1], str[2], str[3], str[4]);                
                }
                MessageBox.Show(
                   "Файл сохранен",
                   "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
            }

        }
        static public void ClearDatabase()
        {
            DialogResult result = MessageBox.Show(
                      $"Вы действительно хотите очистить базу данных?",
                      "Подтвердите действие",
                      MessageBoxButtons.YesNo,
                      MessageBoxIcon.Question
                      );
            if (result == DialogResult.Yes)
            {
                result = MessageBox.Show(
                        $"Вы уверены? Это действие необратимо",
                        "Подтвердите действие",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                        );
                if (result == DialogResult.Yes)
                {
                    List<string> tables = new List<string>();
                    List<string> bands = new List<string>(ListStringRead("SELECT * FROM Bandname"));
                    tables.AddRange(bands);
                    
                    foreach(var band in bands)
                    {
                        List<string> albums = new List<string>();
                        albums.AddRange(ListStringRead($"SELECT * FROM [{band}]"));

                        foreach (var album in albums)
                            tables.Add($"{band}_{album}");
                    }

                    foreach (var table in tables)
                        ExecNonQuery($"DROP TABLE [{table}]");

                    MessageBox.Show(
                       "База данных очищена",
                       "Сообщение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );

                    ExecNonQuery($"TRUNCATE TABLE Bandname");
                    Bands.Clear();
                }
            }
        }


        static public void ExecNonQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
                connection.Close();
            }
        }
        static List<string[]> ListStringArrayRead(string query)
        {
            List<string[]> list = new List<string[]>();
            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            list.Add(new string[] { reader[0].ToString(), reader[1].ToString() });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
                connection.Close();
            }
            return list;
        }
        static List<string> ListStringRead(string query)
        {
            List<string> list = new List<string>();
            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            list.Add(reader[0].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
                connection.Close();
            }
            return list;
        }
    }
}
