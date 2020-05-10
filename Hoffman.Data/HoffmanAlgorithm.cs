using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoffman.Data
{
    public class HoffmanAlgorithm
    {

        private Node Root;
        private List<int> Code;
        private Dictionary<char, List<int>> Table;

        // Длина строки
        private int Length = 0;

        public HoffmanAlgorithm()
        {
            Code = new List<int>();
            Table = new Dictionary<char, List<int>>();
        }

        //Словарь для создания дерева
        private Dictionary<char, int> CreateDictionaryOfElements(string text)
        {

            Dictionary<char, int> d = new Dictionary<char, int>();

            Length = text.Length;
            //Проход по  тексту и составляем словарь с количеством букв           
            for (int i = 0; i < text.Length; i++)
            {
                char ch = text[i];
                if (!d.ContainsKey(ch))
                {
                    d.Add(ch, 0);
                }
                d[ch]++;
            }
            return d;
        }
        // Сортируем и убираем первые два элемента и на его место кладем созданный на основе их
        private void CreateTree(Dictionary<char, int> dictionary)
        {
            LinkedList<Node> lst = new LinkedList<Node>();

            foreach (var item in dictionary)
            {
                Node node = new Node()
                {
                    c = item.Key,
                    i = item.Value
                };
                lst.AddLast(node);
            }


            int length = lst.Count;
            while (length != 1)
            {
                lst.QSort();
                Node left = lst.First.Value;
                lst.RemoveFirst();
                length--;

                Node right = lst.First.Value;
                lst.RemoveFirst();
                length--;

                Node last = new Node()
                {
                    Left = left,
                    Right = right,
                    i = left.i + right.i,
                    c = '\0'
                };
                lst.AddLast(last);
                length++;
            }
            // Определяем вершину
            this.Root = lst.First.Value;
        }

        private void BuildTable(Node Root)
        {

            if (Root.Left != null)
            {
                Code.Add(0);
                BuildTable(Root.Left);
            }

            if (Root.Right != null)
            {
                Code.Add(1);
                BuildTable(Root.Right);
            }

            if (Root.c != '\0')
            {
                List<int> lst = new List<int>();
                for (int i = 0; i < Code.Count; i++)
                {
                    lst.Add(Code[i]);
                }
                Table[Root.c] = lst;
            }
            if (Code.Count != 0)
            {
                // Удалить последнее значение
                Code.RemoveAt(Code.Count - 1);
            }
        }

        public string CodingOutput(string text)
        {
            this.CreateTree(CreateDictionaryOfElements(text));

            this.BuildTable(Root);

            List<char> c = new List<char>();

            string temp = string.Empty;

            for (int i = 0; i < text.Length; i++)
            {
                List<int> ch = Table[text[i]];
                int length = ch.Count;
                for (int j = 0; j < length; j++)
                {
                    if (ch[j] == 1) temp += '1';
                    else temp += '0';
                }
            }
            string output = BinaryToCharArray(temp);

            return output;
        }

        private string Decoding(string c)
        {
            string str = string.Empty;

            for (int i = 0; i < c.Length; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    str += ((c[i] >> 15 - j) & 1);
                }
            }
            return str;
        }

        public string DecodingOutput(string ch)
        {
            string str = Decoding(ch);

            string output = string.Empty;

            Node n = Root;

            for (int i = 0; i < str.Length; i++)
            {

                if (str[i] == '1') n = n.Right;
                else n = n.Left;

                if (n.Left == null && n.Right == null) { output += n.c; n = Root; }
            }

            return output.Substring(0,this.Length);
        }
      
        private string BinaryToCharArray(string bin)
        {
            //16 bit
            int count = 0;
            int litera = 0;
            string str = string.Empty;

            List<char> c = new List<char>();

            for (int i = 0; i < bin.Length; i++, count++)
            {
                if (bin[i] == '1')
                {
                    litera = litera | (1 << 15 - count);
                }

                if (count == 15 || i == bin.Length - 1)
                {
                    count = -1;
                    str += Convert.ToChar(litera);
                    litera = 0;
                }
            }
            return str;
        }
    }
}
