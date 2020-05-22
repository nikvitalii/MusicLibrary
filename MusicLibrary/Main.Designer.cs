namespace MusicLibrary
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AddSong = new System.Windows.Forms.Button();
            this.PrintButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DeleteSong = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.libraryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Песня = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Альбом = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Год = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Band = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Жанр = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.musicCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DeleteAlbum = new System.Windows.Forms.Button();
            this.DeleteBand = new System.Windows.Forms.Button();
            this.SearchNameBox = new System.Windows.Forms.TextBox();
            this.SearchName = new System.Windows.Forms.Label();
            this.SearchAlbumBox = new System.Windows.Forms.TextBox();
            this.SearchAlbum = new System.Windows.Forms.Label();
            this.SearchYearBox = new System.Windows.Forms.TextBox();
            this.SearchYear = new System.Windows.Forms.Label();
            this.SearchBandBox = new System.Windows.Forms.TextBox();
            this.SearchBand = new System.Windows.Forms.Label();
            this.SearchGenreBox = new System.Windows.Forms.TextBox();
            this.SearchGenre = new System.Windows.Forms.Label();
            this.EditSong = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьВыборкуКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.очиститьБазуДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ComparisonButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.libraryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.musicCollectionBindingSource)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddSong
            // 
            this.AddSong.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.AddSong.Location = new System.Drawing.Point(567, 226);
            this.AddSong.Name = "AddSong";
            this.AddSong.Size = new System.Drawing.Size(150, 23);
            this.AddSong.TabIndex = 6;
            this.AddSong.Text = "Добавить песню";
            this.AddSong.UseVisualStyleBackColor = true;
            this.AddSong.Click += new System.EventHandler(this.AddSong_Click);
            // 
            // PrintButton
            // 
            this.PrintButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PrintButton.Location = new System.Drawing.Point(567, 255);
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(150, 23);
            this.PrintButton.TabIndex = 7;
            this.PrintButton.Text = "Вывести всю коллекцию";
            this.PrintButton.UseVisualStyleBackColor = true;
            this.PrintButton.Click += new System.EventHandler(this.PrintButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 2;
            // 
            // DeleteSong
            // 
            this.DeleteSong.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.DeleteSong.Location = new System.Drawing.Point(567, 313);
            this.DeleteSong.Name = "DeleteSong";
            this.DeleteSong.Size = new System.Drawing.Size(150, 23);
            this.DeleteSong.TabIndex = 8;
            this.DeleteSong.Text = "Удалить песню";
            this.DeleteSong.UseVisualStyleBackColor = true;
            this.DeleteSong.Click += new System.EventHandler(this.DeleteSong_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ExitButton.Location = new System.Drawing.Point(567, 400);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(150, 23);
            this.ExitButton.TabIndex = 11;
            this.ExitButton.Text = "Выход";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Песня,
            this.Альбом,
            this.Год,
            this.Band,
            this.Жанр});
            this.dataGridView1.Location = new System.Drawing.Point(12, 28);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(549, 393);
            this.dataGridView1.TabIndex = 0;
            // 
            // Песня
            // 
            this.Песня.HeaderText = "Песня";
            this.Песня.Name = "Песня";
            this.Песня.ReadOnly = true;
            // 
            // Альбом
            // 
            this.Альбом.HeaderText = "Альбом";
            this.Альбом.Name = "Альбом";
            this.Альбом.ReadOnly = true;
            // 
            // Год
            // 
            this.Год.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Год.HeaderText = "Год";
            this.Год.MaxInputLength = 4;
            this.Год.Name = "Год";
            this.Год.ReadOnly = true;
            this.Год.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Год.Width = 49;
            // 
            // Band
            // 
            this.Band.HeaderText = "Группа";
            this.Band.Name = "Band";
            this.Band.ReadOnly = true;
            // 
            // Жанр
            // 
            this.Жанр.HeaderText = "Жанр";
            this.Жанр.Name = "Жанр";
            this.Жанр.ReadOnly = true;
            // 
            // DeleteAlbum
            // 
            this.DeleteAlbum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.DeleteAlbum.Location = new System.Drawing.Point(567, 342);
            this.DeleteAlbum.Name = "DeleteAlbum";
            this.DeleteAlbum.Size = new System.Drawing.Size(150, 23);
            this.DeleteAlbum.TabIndex = 9;
            this.DeleteAlbum.Text = "Удалить альбом";
            this.DeleteAlbum.UseVisualStyleBackColor = true;
            this.DeleteAlbum.Click += new System.EventHandler(this.DeleteAlbum_Click);
            // 
            // DeleteBand
            // 
            this.DeleteBand.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.DeleteBand.Location = new System.Drawing.Point(567, 371);
            this.DeleteBand.Name = "DeleteBand";
            this.DeleteBand.Size = new System.Drawing.Size(150, 23);
            this.DeleteBand.TabIndex = 10;
            this.DeleteBand.Text = "Удалить исполнителя";
            this.DeleteBand.UseVisualStyleBackColor = true;
            this.DeleteBand.Click += new System.EventHandler(this.DeleteBand_Click);
            // 
            // SearchNameBox
            // 
            this.SearchNameBox.Location = new System.Drawing.Point(567, 39);
            this.SearchNameBox.Name = "SearchNameBox";
            this.SearchNameBox.Size = new System.Drawing.Size(150, 22);
            this.SearchNameBox.TabIndex = 1;
            this.SearchNameBox.Text = "Введите текст";
            this.SearchNameBox.TextChanged += new System.EventHandler(this.SearchBox_TextChanged);
            this.SearchNameBox.Enter += new System.EventHandler(this.SearchNameBox_Enter);
            this.SearchNameBox.Leave += new System.EventHandler(this.SearchNameBox_Leave);
            // 
            // SearchName
            // 
            this.SearchName.Location = new System.Drawing.Point(567, 23);
            this.SearchName.Name = "SearchName";
            this.SearchName.Size = new System.Drawing.Size(149, 14);
            this.SearchName.TabIndex = 16;
            this.SearchName.Text = "Название";
            this.SearchName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SearchAlbumBox
            // 
            this.SearchAlbumBox.Location = new System.Drawing.Point(567, 78);
            this.SearchAlbumBox.Name = "SearchAlbumBox";
            this.SearchAlbumBox.Size = new System.Drawing.Size(150, 22);
            this.SearchAlbumBox.TabIndex = 2;
            this.SearchAlbumBox.Text = "Введите текст";
            this.SearchAlbumBox.TextChanged += new System.EventHandler(this.SearchBox_TextChanged);
            this.SearchAlbumBox.Enter += new System.EventHandler(this.SearchAlbumBox_Enter);
            this.SearchAlbumBox.Leave += new System.EventHandler(this.SearchAlbumBox_Leave);
            // 
            // SearchAlbum
            // 
            this.SearchAlbum.Location = new System.Drawing.Point(567, 61);
            this.SearchAlbum.Name = "SearchAlbum";
            this.SearchAlbum.Size = new System.Drawing.Size(149, 14);
            this.SearchAlbum.TabIndex = 16;
            this.SearchAlbum.Text = "Альбом";
            this.SearchAlbum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SearchYearBox
            // 
            this.SearchYearBox.Location = new System.Drawing.Point(567, 117);
            this.SearchYearBox.Name = "SearchYearBox";
            this.SearchYearBox.Size = new System.Drawing.Size(149, 22);
            this.SearchYearBox.TabIndex = 3;
            this.SearchYearBox.Text = "Введите текст";
            this.SearchYearBox.TextChanged += new System.EventHandler(this.SearchBox_TextChanged);
            this.SearchYearBox.Enter += new System.EventHandler(this.SearchYearBox_Enter);
            this.SearchYearBox.Leave += new System.EventHandler(this.SearchYearBox_Leave);
            // 
            // SearchYear
            // 
            this.SearchYear.Location = new System.Drawing.Point(567, 101);
            this.SearchYear.Name = "SearchYear";
            this.SearchYear.Size = new System.Drawing.Size(149, 14);
            this.SearchYear.TabIndex = 16;
            this.SearchYear.Text = "Год";
            this.SearchYear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SearchBandBox
            // 
            this.SearchBandBox.Location = new System.Drawing.Point(567, 156);
            this.SearchBandBox.Name = "SearchBandBox";
            this.SearchBandBox.Size = new System.Drawing.Size(150, 22);
            this.SearchBandBox.TabIndex = 4;
            this.SearchBandBox.Text = "Введите текст";
            this.SearchBandBox.TextChanged += new System.EventHandler(this.SearchBox_TextChanged);
            this.SearchBandBox.Enter += new System.EventHandler(this.SearchBandBox_Enter);
            this.SearchBandBox.Leave += new System.EventHandler(this.SearchBandBox_Leave);
            // 
            // SearchBand
            // 
            this.SearchBand.Location = new System.Drawing.Point(567, 139);
            this.SearchBand.Name = "SearchBand";
            this.SearchBand.Size = new System.Drawing.Size(149, 14);
            this.SearchBand.TabIndex = 16;
            this.SearchBand.Text = "Группа";
            this.SearchBand.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SearchGenreBox
            // 
            this.SearchGenreBox.Location = new System.Drawing.Point(567, 196);
            this.SearchGenreBox.Name = "SearchGenreBox";
            this.SearchGenreBox.Size = new System.Drawing.Size(150, 22);
            this.SearchGenreBox.TabIndex = 5;
            this.SearchGenreBox.Text = "Введите текст";
            this.SearchGenreBox.TextChanged += new System.EventHandler(this.SearchBox_TextChanged);
            this.SearchGenreBox.Enter += new System.EventHandler(this.SearchGenreBox_Enter);
            this.SearchGenreBox.Leave += new System.EventHandler(this.SearchGenreBox_Leave);
            // 
            // SearchGenre
            // 
            this.SearchGenre.Location = new System.Drawing.Point(567, 179);
            this.SearchGenre.Name = "SearchGenre";
            this.SearchGenre.Size = new System.Drawing.Size(149, 14);
            this.SearchGenre.TabIndex = 16;
            this.SearchGenre.Text = "Жанр";
            this.SearchGenre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EditSong
            // 
            this.EditSong.Location = new System.Drawing.Point(567, 284);
            this.EditSong.Name = "EditSong";
            this.EditSong.Size = new System.Drawing.Size(150, 23);
            this.EditSong.TabIndex = 17;
            this.EditSong.Text = "Редактировать";
            this.EditSong.UseVisualStyleBackColor = true;
            this.EditSong.Click += new System.EventHandler(this.EditSong_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.оПрограммеToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(729, 24);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьКакToolStripMenuItem,
            this.сохранитьВыборкуКакToolStripMenuItem,
            this.очиститьБазуДанныхToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // сохранитьКакToolStripMenuItem
            // 
            this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            this.сохранитьКакToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.сохранитьКакToolStripMenuItem.Text = "Сохранить как";
            this.сохранитьКакToolStripMenuItem.Click += new System.EventHandler(this.сохранитьКакToolStripMenuItem_Click_1);
            // 
            // сохранитьВыборкуКакToolStripMenuItem
            // 
            this.сохранитьВыборкуКакToolStripMenuItem.Name = "сохранитьВыборкуКакToolStripMenuItem";
            this.сохранитьВыборкуКакToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.сохранитьВыборкуКакToolStripMenuItem.Text = "Сохранить выборку как";
            this.сохранитьВыборкуКакToolStripMenuItem.Click += new System.EventHandler(this.сохранитьВыборкуКакToolStripMenuItem_Click);
            // 
            // очиститьБазуДанныхToolStripMenuItem
            // 
            this.очиститьБазуДанныхToolStripMenuItem.Name = "очиститьБазуДанныхToolStripMenuItem";
            this.очиститьБазуДанныхToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.очиститьБазуДанныхToolStripMenuItem.Text = "Очистить базу данных";
            this.очиститьБазуДанныхToolStripMenuItem.Click += new System.EventHandler(this.очиститьБазуДанныхToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // ComparisonButton
            // 
            this.ComparisonButton.Location = new System.Drawing.Point(686, 116);
            this.ComparisonButton.Name = "ComparisonButton";
            this.ComparisonButton.Size = new System.Drawing.Size(31, 23);
            this.ComparisonButton.TabIndex = 19;
            this.ComparisonButton.Text = "=";
            this.ComparisonButton.UseVisualStyleBackColor = true;
            this.ComparisonButton.Click += new System.EventHandler(this.ComparisonButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 432);
            this.Controls.Add(this.ComparisonButton);
            this.Controls.Add(this.EditSong);
            this.Controls.Add(this.SearchGenre);
            this.Controls.Add(this.SearchBand);
            this.Controls.Add(this.SearchYear);
            this.Controls.Add(this.SearchAlbum);
            this.Controls.Add(this.SearchName);
            this.Controls.Add(this.SearchGenreBox);
            this.Controls.Add(this.SearchBandBox);
            this.Controls.Add(this.SearchYearBox);
            this.Controls.Add(this.SearchAlbumBox);
            this.Controls.Add(this.SearchNameBox);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.DeleteBand);
            this.Controls.Add(this.DeleteAlbum);
            this.Controls.Add(this.DeleteSong);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PrintButton);
            this.Controls.Add(this.AddSong);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.Text = "MusicLibrary";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.MusicLibrary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.libraryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.musicCollectionBindingSource)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddSong;
        private System.Windows.Forms.Button PrintButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button DeleteSong;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.BindingSource libraryBindingSource;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource musicCollectionBindingSource;
        private System.Windows.Forms.Button DeleteAlbum;
        private System.Windows.Forms.Button DeleteBand;
        private System.Windows.Forms.TextBox SearchNameBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Песня;
        private System.Windows.Forms.DataGridViewTextBoxColumn Альбом;
        private System.Windows.Forms.DataGridViewTextBoxColumn Год;
        private System.Windows.Forms.DataGridViewTextBoxColumn Band;
        private System.Windows.Forms.DataGridViewTextBoxColumn Жанр;
        private System.Windows.Forms.Label SearchName;
        private System.Windows.Forms.TextBox SearchAlbumBox;
        private System.Windows.Forms.Label SearchAlbum;
        private System.Windows.Forms.TextBox SearchYearBox;
        private System.Windows.Forms.Label SearchYear;
        private System.Windows.Forms.TextBox SearchBandBox;
        private System.Windows.Forms.Label SearchBand;
        private System.Windows.Forms.TextBox SearchGenreBox;
        private System.Windows.Forms.Label SearchGenre;
        private System.Windows.Forms.Button EditSong;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьВыборкуКакToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem очиститьБазуДанныхToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.Button ComparisonButton;
    }
}

