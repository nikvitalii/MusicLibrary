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
        ////////////connection
        public static string ConnectionStr = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "data\\MusicLibrary.mdf")};Integrated Security = True; Connect Timeout = 30";
        public static SqlConnection Connection = new SqlConnection(ConnectionStr);
        private static List<Band> Bands;

        static public void Open()
        {
            Bands = new List<Band>();
            Connection.Open();
        }
        static public void Close()
        {
            Connection.Close();
        }
        //


        //queries
        static public void LoadDatabase()
        {
            List <Band> list = new List<Band>();


        }


        static public List<string[]> FullDatabase()
        {
            string query = "SELECT * FROM BandName";
            SqlCommand command = new SqlCommand(query, Connection);
            var data = new List<string[]>();
            var bands = new List<string[]>();
            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                        bands.Add(new string[] { reader[0].ToString(), reader[1].ToString() });

                foreach (var band in bands)
                {
                    var albums = new List<string[]>();
                    query = $"SELECT * FROM [{band[0]}]";
                    command = new SqlCommand(query, Connection);
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
                    foreach (var album in albums)
                    {
                        query = $"SELECT * FROM [{band[0]}_{album[0]}]";
                        command = new SqlCommand(query, Connection);
                        try
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                                while (reader.Read())
                                    data.Add(new string[] { reader[0].ToString(), album[0], album[1], band[0], band[1] });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw;
                        }
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        static public void AddItem(string song, string album, string band, int year, string genre)
        {
            string query = $"SELECT * FROM BandName WHERE Band=N'{band}'";
            SqlCommand command = new SqlCommand(query, Connection);
            var bands = new List<string[]>();

            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    bands.Add(new string[2] { reader[0].ToString(), reader[1].ToString() });
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

            if (bands.Count == 0)
            {
                query = $"INSERT INTO BandName VALUES(N'{band}',N'{genre}')";
                command = new SqlCommand(query, Connection);
                command.ExecuteNonQuery();

                query = $"CREATE TABLE [{band}] ([Name] NVARCHAR(50) NOT NULL PRIMARY KEY , [Genre]NVARCHAR(50) NOT NULL)";
                command = new SqlCommand(query, Connection);
                command.ExecuteNonQuery();
            }

            query = $"SELECT * FROM [{band}] WHERE Name=N'{album}'";
            command = new SqlCommand(query, Connection);
            var albums = new List<string[]>();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    albums.Add(new string[2] { reader[0].ToString(), reader[1].ToString() });

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

            if (albums.Count == 0)
            {
                query = $"INSERT INTO [{band}] VALUES(N'{album}',N'{year}')";
                command = new SqlCommand(query, Connection);
                command.ExecuteNonQuery();

                query = $"CREATE TABLE [{band}_{album}] ([Name] NVARCHAR(50) NOT NULL)";
                command = new SqlCommand(query, Connection);
                command.ExecuteNonQuery();
            }
            query = $"SELECT * FROM [{band}_{album}] WHERE Name=N'{song}'";
            command = new SqlCommand(query, Connection);
            var songs = new List<string>();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    songs.Add(reader[0].ToString());
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

            if (songs.Count == 0)
            {
                query = $"INSERT INTO [{band}_{album}] VALUES(N'{song}')";
                command = new SqlCommand(query, Connection);
                command.ExecuteNonQuery();
                Added();
            }
            else
            {
                Exists();
            }
        }
        static public List<string[]> Search(List<string> search)
        {
            string query = "SELECT * FROM BandName";
            SqlCommand command = new SqlCommand(query, Connection);
            List<string[]> bands = new List<string[]>();
            List<string[]> result = new List<string[]>();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    bands.Add(new string[] { reader[0].ToString(), reader[1].ToString() });

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

            foreach (var band in bands)
            {
                query = $"SELECT * FROM [{band[0]}]";
                command = new SqlCommand(query, Connection);
                List<string[]> albums = new List<string[]>();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                        albums.Add(new string[] { reader[0].ToString(), reader[1].ToString() });

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }

                foreach (var album in albums)
                {
                    query = $"SELECT * FROM [{band[0]}_{album[0]}]";
                    command = new SqlCommand(query, Connection);
                    List<string> songs = new List<string>();

                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                            songs.Add(reader[0].ToString());

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw;
                    }
                    foreach (var song in songs)
                        if ((song.ToUpper().Contains(search[0].ToUpper()) || search[0] == "Введите текст" || search[0].Length == 0)
                            && (album[0].ToUpper().Contains(search[1].ToUpper()) || search[1] == "Введите текст" || search[1].Length == 0)
                            && (album[1].ToUpper().Contains(search[2].ToUpper()) || search[2] == "Введите текст" || search[2].Length == 0)
                            && (band[0].ToUpper().Contains(search[3].ToUpper()) || search[3] == "Введите текст" || search[3].Length == 0)
                            && (band[1].ToUpper().Contains(search[4].ToUpper()) || search[4] == "Введите текст" || search[4].Length == 0))
                            result.Add(new string[] { song, album[0], album[1], band[0], band[1] });
                }
            }
            return result;
        }
        static public void DeleteSong(List<string> list)
        {
            string query = $"DELETE FROM [{list[0]}_{list[1]}] WHERE Name=N'{list[2]}'";
            SqlCommand command = new SqlCommand(query, Connection);
            command.ExecuteNonQuery();

            query = $"SELECT * FROM [{list[0]}_{list[1]}]";
            command = new SqlCommand(query, Connection);

            try
            {
                List<string> songs = new List<string>();
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                        songs.Add(reader[0].ToString());

                if (songs.Count == 0)
                    DeleteAlbum(new List<string> { list[0], list[1] });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
        static public void DeleteAlbum(List<string> list)
        {
            string query = $"DROP TABLE {list[0]}_{list[1]}";
            SqlCommand command = new SqlCommand(query, Connection);
            command.ExecuteNonQuery();

            query = $"DELETE FROM [{list[0]}] WHERE Name=N'{list[1]}'";
            command = new SqlCommand(query, Connection);
            command.ExecuteNonQuery();

            query = $"SELECT * FROM [{list[0]}]";
            command = new SqlCommand(query, Connection);
            try
            {
                List<string> albums = new List<string>();
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                        albums.Add(reader[0].ToString());

                if (albums.Count == 0)
                {
                    query = $"DELETE FROM BandName WHERE Band=N'{list[0]}'";
                    command = new SqlCommand(query, Connection);
                    command.ExecuteNonQuery();

                    query = $"DROP TABLE [{list[0]}]";
                    command = new SqlCommand(query, Connection);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
        static public void DeleteBand(string band)
        {
            string query = $"SELECT * FROM [{band}]";
            SqlCommand command = new SqlCommand(query, Connection);
            try
            {
                List<string> albums = new List<string>();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        albums.Add(reader[0].ToString());
                }

                foreach (var album in albums)
                    DeleteAlbum(new List<string> { band, album });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
        static public void ChangeSong(List<string> before, List<string> after)
        {
            DeleteSong(new List<string> { before[3], before[1], before[0] });
            int.TryParse(after[2], out int year);
            AddItem(after[0], after[1], after[3], year, after[4]);
        }
        static public void ChangeAlbum(List<string> before, List<string> after)
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
        }
        static public void ChangeBand(List<string> before, List<string> after)
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

        }
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
        static public void SaveToFile(List<string[]> list, bool filtered = false, List<string> filters = null)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text file*|.txt";
            dialog.ShowDialog();
            if (dialog.FileName.Length > 4)
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
                    foreach (var song in list)
                        wr.WriteLine("{0, -15} {1, -15 }{2, -15} {3, -15} {4, -15}", song[0], song[1], song[2], song[3], song[4]);
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
                    string query = $"TRUNCATE TABLE Bandname";
                    SqlCommand command = new SqlCommand(query, Connection);
                    command.ExecuteNonQuery();


                    List<string> tables = new List<string>();
                    query = "SELECT* FROM sysobjects WHERE name != 'Bandname' and xtype = 'U'";
                    command = new SqlCommand(query, Connection);
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                            while (reader.Read())
                                tables.Add(reader[0].ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw;
                    }

                    foreach (var table in tables)
                    {
                        query = $"DROP TABLE [{table}]";
                        command = new SqlCommand(query, Connection);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show(
                       "База данных очищена",
                       "Сообщение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                }
            }
        }









        ///////////////////
        static public void AddSong()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                string query;
            }
        }
        static public void AddAlbum()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                string query;
            }
        }
        static public void AddBand(string band, string genre)
        {
            var list = Bands.Where(x => x.Name == band);
            if (list.Count() == 0)
            {
                string query = $"CREATE TABLE[{band}] ([Name] NVARCHAR(50) NOT NULL PRIMARY KEY, [Genre]NVARCHAR(50) NOT NULL)";
                ExecNonQuery(query);

                query = $"INSERT INTO BandName VALUES(N'{band}',N'{genre}')";
                ExecNonQuery(query);
            }
        }

        static public void ExecNonQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                SqlCommand command = new SqlCommand(query);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
        }
    }
}
