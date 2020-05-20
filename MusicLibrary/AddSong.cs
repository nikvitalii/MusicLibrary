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
    public partial class AddSong : Form
    {
        public AddSong()
        {
            InitializeComponent();
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SongName.Text = "";
        }
        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Album.Text = "";
        }
        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Year.Text = "";
        }
        private void LinkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Band.Text = "";
        }
        private void LinkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Genre.Text = "";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SongName.Text = "";
            Album.Text = "";
            Year.Text = "";
            Band.Text = "";
            Genre.Text = "";
        }
        private bool Valid()
        {
            bool parsed = int.TryParse(Year.Text, out int n);
            if (!parsed)
            {
                MessageBox.Show(
                       "Введите целое число в год",
                       "Ошибка",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error
                   );
                return false;
            }
            else if (n > DateTime.Now.Year || n < 1000)
            {
                MessageBox.Show(
                       "Введите корректный год",
                       "Ошибка",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error
                   );
                return false;
            }
            List<string> fields = new List<string> { Band.Text, Album.Text, SongName.Text, Year.Text, Genre.Text };
            List<string> errors = new List<string> { "название группы", "название альбома", "название песни", "год опубликования", "жанр" };
            List<char> forbidden = new List<char> { '.', ',', '-', '\\', '/', '\'', '\"', '=', '-', '+' };
            for (int i = 0; i < 5; ++i)
            {
                if (fields[i].Length == 0)
                {
                    MessageBox.Show(
                      $"Введите {errors[i]}",
                      "Ошибка",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Error
                    );
                    return false;
                }
                if (fields[i].Length > 50)
                {
                    MessageBox.Show(
                      $"Слишком длинный текст (Максимум 50 символов)",
                      "Ошибка",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Error
                    );
                    return false;
                }
                foreach (var s in forbidden)
                    if (fields[i].Contains(s))
                    {
                        MessageBox.Show(
                          $"Текст не должен содержать символ {s}",
                          "Ошибка",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error
                        );
                        return false;
                    }
            }
            return true;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (Valid())
            {
                Library.AddItem(SongName.Text, Album.Text, Band.Text, int.Parse(Year.Text), Genre.Text);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
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
    }
}
