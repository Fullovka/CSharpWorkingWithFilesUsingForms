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

        // ��� ��� ������ "������� ����":
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog.FileName;
            }
        }

        // ��� ��� ������ "����������������� ����":
        private void button2_Click(object sender, EventArgs e)
        {
            // �������� ���� � ���������� ����� �� ���������� ����
            string inputFilePath = textBox1.Text;

            // ���������, ��� ���� ����������
            if (!File.Exists(inputFilePath))
            {
                textBox2.Text = "���� �� ������!";
                return;
            }

            // ��������� ������� ���� ��� ������
            using (StreamReader reader = new StreamReader(inputFilePath))
            {
                // ���������� ���� � ��������� �����
                string outputFilePath = Path.Combine(Path.GetDirectoryName(inputFilePath), "output.txt");

                // ��������� �������� ���� ��� ������
                using (StreamWriter writer = new StreamWriter(outputFilePath))
                {
                    string line;

                    // ������ ������ �� �����
                    while ((line = reader.ReadLine()) != null)
                    {
                        // ���� ������ ��� ������������� ������ ��� ����� ������ >= 60, �� ����� �� � ����
                        if (line.EndsWith(".") || line.Length >= 60)
                        {
                            writer.WriteLine(line);
                        }
                        else
                        {
                            // ����� ���������� ������ ����� � ���������� �� � ����, ���� ������ �� ���������� ������ ��� �� ����� ��������� ����� ��������
                            while ((line = reader.ReadLine()) != null && !line.EndsWith(".") && (line.Length + writer.NewLine.Length) < 60)
                            {
                                writer.Write(line + " ");
                            }

                            // ����� ������������ ������ � ����
                            writer.WriteLine(line);
                        }
                    }
                }
            }

            textBox2.Text = "���� ���������������� �������!";
        }
    }
}
