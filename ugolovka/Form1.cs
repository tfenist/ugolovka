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

            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*"; // Фильтр для типов файлов
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
        }

        public string fileText = "";


        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            StreamReader sr = new StreamReader(openFileDialog1.FileName, Encoding.UTF8); // Считывание текста из файла в кодировке utf-8

            // получаем выбранный файл
            string filename = openFileDialog1.FileName;

            // читаем файл по символьно и записываем текст в обну строку
            fileText += sr.ReadToEnd();
            fileText = fileText.Replace("\r\n", " ");

            string pattern = @"\w+\s*"; // Задаем паттерн
            Regex regex = new Regex(pattern);

            string result = "";

            foreach (Match match in regex.Matches(fileText)) // Оставляем в тексте алфавитно-цифровые и пробельные символы 
            {
                result += match.Value;
            }

            richTextBox1.Text = result.ToLowerInvariant(); // Переведим текст в нижний регистр

            string[] words = richTextBox1.Text.Split(" ");
           

            foreach (string word in words)
            {
                int Ru = 0, Eng = 0, Num = 0;

                for (int i = 0; i < word.Length; i++)
                {
                    char c = word[i];

                    if ((c >= 'а') & (c <= 'я'))
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

        private void button2_Click(object sender, EventArgs e) // сохранение в файл
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            File.WriteAllText(saveFileDialog1.FileName, richTextBox2.Text);
            MessageBox.Show("Фаил сохранен!");
        }

        public class EnglishtoRussian
        {
            Dictionary<string, string> Dchar = new Dictionary<string, string>()
            {
                {"a", "а"},
                {"b", "ь"},
                {"o", "о"},
                {"0", "о"},
                {"3", "з"},
                {"4", "ч"},
                {"6", "б"},
                {"7", "т"},
                {"8", "в"},
                {"9", "д"},
                {"x", "х"},
                {"k", "к"},
                {"e", "е"},
                {"c", "с"},
                {"m", "м"},
                {"t", "т"},
                {"y", "у"},
                {"w", "ш"},
                {"bi", "ы"},
                {"p", "р"},
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
                {"а", "a"},
                {"ь", "b"},
                {"о", "o"},
                {"0", "o"},
                {"1", "l"},
                {"2", "z"},
                {"5", "s"},
                {"7", "t"},
                {"9", "g"},
                {"х", "x"},
                {"к", "k"},
                {"е", "e"},
                {"с", "c"},
                {"м", "m"},
                {"т", "t"},
                {"у", "y"},
                {"ш", "w"},
                {"р", "p"},

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