using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicLibrary
{
    public partial class EditSong : Form
    {
        private bool Changed;
        private Main Form;
        private List<string> Before;
        public EditSong()
        {
            InitializeComponent();
        }

        public void Edit(List<string> list, Main form)
        {
            Form = form;
            Before = new List<string>(list);
            SongChange.Checked = true;
            NameBefore.Text = Before[0];
            AlbumBefore.Text = Before[1];
            YearBefore.Text = Before[2];
            BandBefore.Text = Before[3];
            GenreBefore.Text = Before[4];

            NameAfter.Text = Before[0];
            AlbumAfter.Text = Before[1];
            YearAfter.Text = Before[2];
            BandAfter.Text = Before[3];
            GenreAfter.Text = Before[4];
            Changed = false;
        }

        private void ChangeTB()
        {
            NameBefore.Text = NameAfter.Text;
            AlbumBefore.Text = AlbumAfter.Text;
            YearBefore.Text = YearAfter.Text;
            BandBefore.Text = BandAfter.Text;
            GenreBefore.Text = GenreAfter.Text;

            Before[0] = NameAfter.Text;
            Before[1] = AlbumAfter.Text;
            Before[2] = YearAfter.Text;
            Before[3] = BandAfter.Text;
            Before[4] = GenreAfter.Text;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            if (!Changed)
            {
                DialogResult result = MessageBox.Show(
                          $"Вы действительно хотите выйти без изменений?",
                          "Подтвердите действие",
                          MessageBoxButtons.YesNo,
                          MessageBoxIcon.Question
                          );
                if (result == DialogResult.Yes)
                    Close();
            }
            else
                Close();
        }

        private bool Valid(List<string> fields)
        {
            bool parsed = int.TryParse(YearAfter.Text, out int n);
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

        private bool NotEqual()
        {
            if (
                NameBefore.Text != NameAfter.Text ||
                AlbumBefore.Text != AlbumAfter.Text ||
                YearBefore.Text != YearAfter.Text ||
                BandBefore.Text != BandAfter.Text ||
                GenreBefore.Text != GenreAfter.Text
            )
                return true;
            MessageBox.Show(
                "Значения до и после должны отличаться",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
                );
            return false;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string> { NameAfter.Text, AlbumAfter.Text, YearAfter.Text, BandAfter.Text, GenreAfter.Text };
            if (NotEqual())
                if (Valid(list))
                {
                    if (SongChange.Checked)
                        Library.ChangeSong(Before, list);
                    else if (AlbumChange.Checked)
                        Library.ChangeAlbum(Before, list);
                    else if (BandChange.Checked)
                        Library.ChangeBand(Before, list);
                    ChangeTB();
                    Form.FullPrint();
                    Changed = true;
                }
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            NameAfter.Text = Before[0];
            AlbumAfter.Text = Before[1];
            YearAfter.Text = Before[2];
            BandAfter.Text = Before[3];
            GenreAfter.Text = Before[4];
        }

        private void SongChange_CheckedChanged(object sender, EventArgs e)
        {
            NameAfter.Enabled = true;
            AlbumAfter.Enabled = true;
            YearAfter.Enabled = true;
            BandAfter.Enabled = true;
            GenreAfter.Enabled = true;
        }

        private void AlbumChange_CheckedChanged(object sender, EventArgs e)
        {
            NameAfter.Enabled = false;
            AlbumAfter.Enabled = true;
            YearAfter.Enabled = true;
            BandAfter.Enabled = true;
            GenreAfter.Enabled = false;
        }

        private void BandChange_CheckedChanged(object sender, EventArgs e)
        {

            NameAfter.Enabled = false;
            AlbumAfter.Enabled = false;
            YearAfter.Enabled = false;
            BandAfter.Enabled = true;
            GenreAfter.Enabled = true;
        }
    }
}
