using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicLibrary
{
    public abstract class Validateable : Form
    {
        // Изменилась ли база данных
        public bool Changed;

        // Проверка вводных данных на валидность
        public bool Valid(List<string> fields)
        {
            bool parsed = int.TryParse(fields[2], out int n);
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
            List<string> errors = new List<string> { "название песни", "название альбома", "год опубликования", "название группы",  "жанр" };
            List<char> forbidden = new List<char> { '.', ',', '-', '\\', '/', '\'', '\"', '=', '-', '+', '*', ';', ':'};
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

        public Validateable()
        {
            Changed = false;
        }

        // Подтверждение закрытия формы
        public void ClosingForm()
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
    }
}
