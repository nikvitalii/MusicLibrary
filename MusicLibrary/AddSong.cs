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
    public partial class AddSong : Validateable 
    {
        public AddSong() : base()
        {
            InitializeComponent();
        }
        // Очистка текстбоксов.
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
        //

        // Очистка всех текстбоксов.
        private void ClearAllButton_Click(object sender, EventArgs e)
        {
            SongName.Text = "";
            Album.Text = "";
            Year.Text = "";
            Band.Text = "";
            Genre.Text = "";
        }
        
        // Добавление записи.
        private void AddButton_Click(object sender, EventArgs e)
        {
            if (Valid(new List<string> { SongName.Text, Album.Text, Year.Text, Band.Text, Genre.Text }))
            {
                Library.AddItem(SongName.Text, Album.Text, Band.Text, Year.Text, Genre.Text);
                Changed = true;
            }
        }

        // Выход из формы.
        private void ExitButtonClick(object sender, EventArgs e)
        {
            ClosingForm();
        }
    }
}
