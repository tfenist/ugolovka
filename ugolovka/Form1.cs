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






        private void button2_Click(object sender, EventArgs e) // ���������� � ����
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            File.WriteAllText(saveFileDialog1.FileName, richTextBox2.Text);
            MessageBox.Show("���� ��������!");
        }

        public class EnglishtoRussian
        {
            Dictionary<string, string> Dchar = new Dictionary<string, string>()
            {
                {"a", "�"},
                {"b", "�"},
                {"o", "�"},
                {"0", "�"},
                {"3", "�"},
                {"4", "�"},
                {"6", "�"},
                {"7", "�"},
                {"8", "�"},
                {"9", "�"},
                {"x", "�"},
                {"k", "�"},
                {"e", "�"},
                {"c", "�"},
                {"m", "�"},
                {"t", "�"},
                {"y", "�"},
                {"w", "�"},
                {"bi", "�"},
                {"p", "�"},
            };

            public string Swap(string source)
            {
                string result = "";

                foreach (var ch in source)
                {
                    var s = "";

                    if (Dchar.TryGetValue(ch.ToString(), out s))
                        result += s;

                    else result += ch;
                }

                return result;
            }
        }

        public class RussiantoEnglish
        {
            Dictionary<string, string> Dchar = new Dictionary<string, string>()
            {
                {"�", "a"},
                {"�", "b"},
                {"�", "o"},
                {"0", "o"},
                {"1", "l"},
                {"2", "z"},
                {"5", "s"},
                {"7", "t"},
                {"9", "g"},
                {"�", "x"},
                {"�", "k"},
                {"�", "e"},
                {"�", "c"},
                {"�", "m"},
                {"�", "t"},
                {"�", "y"},
                {"�", "w"},
                {"�", "p"},

            };
            public string Swap(string source)
            {
                string result = "";

                foreach (var ch in source)
                {
                    var s = "";

                    if (Dchar.TryGetValue(ch.ToString(), out s))
                        result += s;

                    else result += ch;
                }

                return result;
            }
        }
    }
}