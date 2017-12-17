using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTI
{
    public static class Vigener
    {
        static List<List<char>> codeMatrix = new List<List<char>>();

        static Vigener()
        {
            GenerateCodeMatrix();
        }

        public static string Encrypt(string key, string input)
        {
            var keyWord = key.Replace(" ", string.Empty);
            var keyWordIndex = 0;
            var inputs = input.Split(' ').Select(x => x.ToCharArray()).ToArray();
            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    inputs[i][j] = codeMatrix[char.ToUpper(inputs[i][j]) - 'A'][keyWord.ToUpper()[keyWordIndex] - 'A'];
                    keyWordIndex++;
                    keyWordIndex %= keyWord.Length;
                }
            }
            return string.Join(" ", inputs.Select(x => new string(x)));
        }
        public static string Decrypt(string key, string input)
        {
            var keyWord = key.Replace(" ", string.Empty);
            var keyChar = 'A';
            int keyIndex = 0;
            var text = input.Split(' ').Select(x => x.ToCharArray()).ToArray();
            for (int i = 0; i < text.Length; i++)
            {

                for (int j = 0; j < text[i].Length; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        keyChar = codeMatrix[key[keyIndex] - 'A'][text[i][j] - 'A'];
                        text[i][j] = keyChar;
                        continue;
                    }

                    text[i][j] = (char)(((text[i][j] - keyChar) % 26 + 26) % 26 + 65);
                    keyChar = text[i][j];
                }
            }

            return string.Join(" ", text.Select(x => new string(x)));
        }

        static void GenerateCodeMatrix()
        {
            var letters = 26;
            for (int i = 0; i < letters; i++)
            {
                var list = new List<char>();
                for (int j = 0; j < letters; j++)
                {
                    list.Add((char)('A' + (i + j) % letters));
                }
                codeMatrix.Add(list);
            }
        }

    }
}
