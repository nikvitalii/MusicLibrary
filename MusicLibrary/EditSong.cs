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
    public partial class EditSong : Validateable
    {
        // Ссылка на главную форму.
        private Main Form;

        // Коллекция строк до изменений.
        private List<string> Before;

        public EditSong():base()
        {
            InitializeComponent();
        }

        // Заполнение текстбоксов при открытии формы.
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

        // Изменение текстбоксов после изменения.
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

        // Выход из формы.
        private void Cancel_Click(object sender, EventArgs e)
        {
            ClosingForm();
        }

        // Проверка, равны ли варианты до и после.
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

        // Сохранение изменений.
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

        // Сброс изменения.
        private void Reset_Click(object sender, EventArgs e)
        {
            NameAfter.Text = Before[0];
            AlbumAfter.Text = Before[1];
            YearAfter.Text = Before[2];
            BandAfter.Text = Before[3];
            GenreAfter.Text = Before[4];
        }

        // Изменение песни.
        private void SongChange_CheckedChanged(object sender, EventArgs e)
        {
            NameAfter.Enabled = true;
            AlbumAfter.Enabled = true;
            YearAfter.Enabled = true;
            BandAfter.Enabled = true;
            GenreAfter.Enabled = true;
        }

        // Изменение альбома.
        private void AlbumChange_CheckedChanged(object sender, EventArgs e)
        {
            NameAfter.Enabled = false;
            AlbumAfter.Enabled = true;
            YearAfter.Enabled = true;
            BandAfter.Enabled = true;
            GenreAfter.Enabled = false;
        }

        // Изменение исполнителя.
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
