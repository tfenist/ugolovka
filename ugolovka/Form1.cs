using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ugolovka
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*"; // ������ ��� ����� ������
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
        }

        public string fileText = "";

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            StreamReader sr = new StreamReader(openFileDialog1.FileName, Encoding.UTF8); // ���������� ������ �� ����� � ��������� utf-8

            // �������� ��������� ����
            string filename = openFileDialog1.FileName;

            // ������ ���� �� ��������� � ���������� ����� � ���� ������
            fileText += sr.ReadToEnd();
            fileText = fileText.Replace("\r\n", " ");

            string pattern = @"\w+\s*"; // ������ �������
            Regex regex = new Regex(pattern);

            string result = "";

            foreach (Match match in regex.Matches(fileText)) // ��������� � ������ ���������-�������� � ���������� ������� 
            {
                result += match.Value;
            }

            richTextBox1.Text = result.ToLowerInvariant(); // ��������� ����� � ������ �������
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            File.WriteAllText(saveFileDialog1.FileName, richTextBox2.Text);
            MessageBox.Show("���� ��������!");
        }
    }
}