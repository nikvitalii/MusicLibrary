using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicLibrary
{
    public partial class Main : Form
    {
        AddSong add;
        EditSong edit;
        public Main()
        {
            InitializeComponent();
        }
        private void MusicLibrary_Load(object sender, EventArgs e)
        {
            Library.LoadDatabase();
            FullPrint();
            edit = null;
            add = null;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                      $"Вы действительно хотите выйти?",
                      "Подтвердите действие",
                      MessageBoxButtons.YesNo,
                      MessageBoxIcon.Question
                      );
            if (result == DialogResult.Yes)
                Close();
        }

        //print
        private void PrintButton_Click(object sender, EventArgs e)
        {
            FullPrint();
        }

        public void FullPrint()
        {
            dataGridView1.Rows.Clear();
            PrintFromBandList(Library.FullDatabase());

            SearchNameBox.Text = "Введите текст";
            SearchAlbumBox.Text = "Введите текст";
            SearchBandBox.Text = "Введите текст";
            SearchYearBox.Text = "Введите текст";
            SearchGenreBox.Text = "Введите текст";
        }
        private void PrintFromBandList(List<Band> list)
        {
            dataGridView1.Rows.Clear();
            if (list != null)
                foreach (var band in list)
                    foreach (var album in band.Albums)
                        foreach (var song in album.Songs)
                            dataGridView1.Rows.Add(new string[] { song.Name, album.Name, album.Year, band.Name, band.Genre });
        }

        private void PrintFromList(List<string[]> list)
        {
            dataGridView1.Rows.Clear();
            foreach (var row in list)
                dataGridView1.Rows.Add(row);
        }
        //add
        private void AddSong_Click(object sender, EventArgs e)
        {
            if (add == null || add.Text == "")
            {
                add = new AddSong();
                add.Show();
            }
            else if (CheckOpened(edit.Text))
            {
                add.WindowState = FormWindowState.Normal;
                add.Focus();
            }
        }
        //delete
        private void DeleteSong_Click(object sender, EventArgs e)
        {
            if (NotEmpty())
            {
                DialogResult result = MessageBox.Show(
                    $"Вы действительно хотите удалить песню {dataGridView1.SelectedRows[0].Cells[0].Value.ToString()}?",
                    "Подтвердите действие",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                    );
                if (result == DialogResult.Yes)
                {
                    Library.DeleteSong(new List<string>
                    {
                        dataGridView1.SelectedRows[0].Cells[3].Value.ToString(),
                        dataGridView1.SelectedRows[0].Cells[1].Value.ToString(),
                        dataGridView1.SelectedRows[0].Cells[0].Value.ToString()
                    });
                    MessageBox.Show(
                        "Песня удалена",
                        "Сообщение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                    FullPrint();
                }
            }
        }
        private void DeleteAlbum_Click(object sender, EventArgs e)
        {
            if (NotEmpty())
            {
                DialogResult result = MessageBox.Show(
                    $"Вы действительно хотите удалить альбом {dataGridView1.SelectedRows[0].Cells[1].Value.ToString()}?",
                    "Подтвердите действие",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                    );
                if (result == DialogResult.Yes)
                {
                    Library.DeleteAlbum(
                            dataGridView1.SelectedRows[0].Cells[3].Value.ToString(),
                            dataGridView1.SelectedRows[0].Cells[1].Value.ToString()
                        );
                    MessageBox.Show(
                        "Альбом удален",
                        "Сообщение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                    FullPrint();
                }
            }
        }
        private void DeleteBand_Click(object sender, EventArgs e)
        {
            if (NotEmpty())
            {
                DialogResult result = MessageBox.Show(
                      $"Вы действительно хотите удалить исполнителя {dataGridView1.SelectedRows[0].Cells[3].Value.ToString()}?",
                      "Подтвердите действие",
                      MessageBoxButtons.YesNo,
                      MessageBoxIcon.Question
                      );
                if (result == DialogResult.Yes)
                {
                    Library.DeleteBand(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
                    MessageBox.Show(
                        "Исполнитель удален",
                        "Сообщение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    FullPrint();
                }
            }
        }

        private bool NotEmpty()
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show(
                        "Библиотека пуста",
                        "Сообщение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                return false;
            }
            return true;
        }

        //search
        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            List<string> list = new List<string> { SearchNameBox.Text, SearchAlbumBox.Text, SearchYearBox.Text, SearchBandBox.Text, SearchGenreBox.Text };
            PrintFromList(Library.Search(list));
        }


        #region BOXFOCUS
        private void Lost(TextBox box)
        {
            if (box.TextLength == 0)
                box.Text = "Введите текст";
        }

        private void Focus(TextBox box)
        {
            if (box.Text == "Введите текст")
                box.Text = "";
        }


        private void SearchNameBox_Enter(object sender, EventArgs e)
        {
            Focus(SearchNameBox);
        }

        private void SearchNameBox_Leave(object sender, EventArgs e)
        {
            Lost(SearchNameBox);
        }

        private void SearchAlbumBox_Enter(object sender, EventArgs e)
        {
            Focus(SearchAlbumBox);
        }

        private void SearchAlbumBox_Leave(object sender, EventArgs e)
        {
            Lost(SearchAlbumBox);

        }

        private void SearchYearBox_Enter(object sender, EventArgs e)
        {
            Focus(SearchYearBox);
        }

        private void SearchYearBox_Leave(object sender, EventArgs e)
        {
            Lost(SearchYearBox);
        }

        private void SearchBandBox_Enter(object sender, EventArgs e)
        {
            Focus(SearchBandBox);
        }

        private void SearchBandBox_Leave(object sender, EventArgs e)
        {
            Lost(SearchBandBox);
        }

        private void SearchGenreBox_Enter(object sender, EventArgs e)
        {
            Focus(SearchGenreBox);
        }

        private void SearchGenreBox_Leave(object sender, EventArgs e)
        {
            Lost(SearchGenreBox);
        }

        #endregion

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private bool CheckOpened(string name)
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm.Text == name)
                {
                    return true;
                }
            }
            return false;
        }

        private void EditSong_Click(object sender, EventArgs e)
        {
            if (NotEmpty())
            {
                List<string> list = new List<string>();
                for (int i = 0; i < 5; ++i)
                    list.Add(dataGridView1.SelectedRows[0].Cells[i].Value.ToString());
                if (edit == null || edit.Text == "")
                {
                    edit = new EditSong();
                    edit.Show();
                    edit.Edit(list, this);
                }
                else if (CheckOpened(edit.Text))
                {
                    edit.WindowState = FormWindowState.Normal;
                    edit.Focus();
                }
            }
        }


        private void сохранитьКакToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Library.SaveToFile(Library.FullDatabase());
        }

        private void сохранитьВыборкуКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string> { SearchNameBox.Text, SearchAlbumBox.Text, SearchYearBox.Text, SearchBandBox.Text, SearchGenreBox.Text };
            bool filtered = false;
            for (int i = 0; i < 5; ++i)
            {
                if (list[i] == "Введите текст" || list[i].Length == 0)
                    list[i] = "";
                else
                    filtered = true;
            }
            Library.SaveToFile(Library.Search(list), filtered, list);
        }

        private void очиститьБазуДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Library.ClearDatabase(); 
            FullPrint();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }
    }
}
