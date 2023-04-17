# C# - working with files using forms

```csharp

using System;
using System.IO;
using System.Windows.Forms;

namespace WorkingWithFilesUsingForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Код для кнопки "Выбрать файл":
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog.FileName;
            }
        }

        // Код для кнопки "Переформатировать файл":
        private void button2_Click(object sender, EventArgs e)
        {
            // Получаем путь к выбранному файлу из текстового поля
            string inputFilePath = textBox1.Text;

            // Проверяем, что файл существует
            if (!File.Exists(inputFilePath))
            {
                textBox2.Text = "Файл не найден!";
                return;
            }

            // Открываем входной файл для чтения
            using (StreamReader reader = new StreamReader(inputFilePath))
            {
                // Определяем путь к выходному файлу
                string outputFilePath = Path.Combine(Path.GetDirectoryName(inputFilePath), "output.txt");

                // Открываем выходной файл для записи
                using (StreamWriter writer = new StreamWriter(outputFilePath))
                {
                    string line;

                    // Читаем строки из файла
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Если строка уже заканчивается точкой или длина строки >= 60, то пишем ее в файл
                        if (line.EndsWith(".") || line.Length >= 60)
                        {
                            writer.WriteLine(line);
                        }
                        else
                        {
                            // Иначе продолжаем чтение строк и объединяем их в одну, пока строка не закончится точкой или не будет достигнут лимит символов
                            while ((line = reader.ReadLine()) != null && !line.EndsWith(".") && (line.Length + writer.NewLine.Length) < 60)
                            {
                                writer.Write(line + " ");
                            }

                            // пишем объединенную строку в файл
                            writer.WriteLine(line);
                        }
                    }
                }
            }

            textBox2.Text = "Файл переформатирован успешно!";
        }
    }
}


```
