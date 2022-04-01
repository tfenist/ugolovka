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

            string[] words = richTextBox1.Text.Split(" ");
           

            foreach (string word in words)
            {
                int Ru = 0, Eng = 0, Num = 0;

                for (int i = 0; i < word.Length; i++)
                {
                    char c = word[i];

                    if ((c >= '�') & (c <= '�'))
                        Ru++;

                    else if ((c >= 'a') & (c <= 'z'))
                        Eng++;

                    else if ((c >= '0') & (c <= '9'))
                        Num++;
                }

                if (Ru > Eng)
                {
                    EnglishtoRussian etr = new EnglishtoRussian();
                    richTextBox2.Text += etr.Swap(word) + " ";

                }

                else if (Eng > Ru)
                {
                    RussiantoEnglish rte = new RussiantoEnglish();
                    richTextBox2.Text += rte.Swap(word) + " ";
                }

                else if (word == "")
                {
                    richTextBox2.Text += " ";
                }

                else richTextBox2.Text += word;
            }


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