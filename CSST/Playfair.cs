using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BTI
{
    public static class Playfair
    {
        static char[][] getPFMatrix(string key)
        {
            var set = new HashSet<char>(key.ToUpper() + pfAlphabet);

            var matrix = new char[5][];
            for (int i = 0; i < 5; i++)
            {
                matrix[i] = set.Skip(5 * i).Take(5).ToArray();
            }

            return matrix;
        }

        static string PlayFairCipher(string input, string key)
        {
            var matrix = getPFMatrix(key);
            var text = input.ToUpper().Replace('J', 'I').Replace(" ", string.Empty);
            if (text.Length % 2 != 0)
                text += 'X';
            var pairs = Regex.Matches(text, "..").Cast<Match>().Select(x => DuplicatesPlayFair(x.Value.ToCharArray())).ToArray();

            return string.Join("", pairs.Select(x => new string(PairEncryption(matrix, x, 1))).ToArray());

        }

        static string PlayFair(string input, string key, int dir)
        {
            var set = new HashSet<char>(key.ToUpper() + pfAlphabet);

            var matrix = new char[5][];
            for (int i = 0; i < 5; i++)
            {
                matrix[i] = set.Skip(5 * i).Take(5).ToArray();
            }
            var text = input.ToUpper().Replace('J', 'I').Replace(" ", string.Empty);
            if (text.Length % 2 != 0)
                text += 'X';
            var pairs = Regex.Matches(text, "..").Cast<Match>().Select(x => DuplicatesPlayFair(x.Value.ToCharArray())).ToArray();

            return string.Join("", pairs.Select(x => new string(PairEncryption(matrix, x, dir))).ToArray());
        }


        public static string Encrypt(string input, string key)
        {
            return PlayFair(input, key, 1);
        }

        public static string Decrypt(string input, string key)
        {
            return PlayFair(input, key, -1);
        }

        static char[] PairEncryption(char[][] matrix, char[] pair, int dir)
        {
            var pairInfo = pair.Select(x => new { c = x, yx = GetYX(matrix, x) }).ToArray();
            var newPair = pair;
            var sameX = pair.Select(x => GetYX(matrix, x)).GroupBy(x => x.x).Count() == 1;
            var sameY = pair.Select(x => GetYX(matrix, x)).GroupBy(x => x.y).Count() == 1;
            if (sameX)
            {
                newPair = pairInfo.Select(c => matrix[((c.yx.y + dir) % 5 + 5) % 5][c.yx.x]).ToArray();
            }
            else if (sameY)
            {
                newPair = pairInfo.Select(c => matrix[c.yx.y][((c.yx.x + dir) % 5 + 5) % 5]).ToArray();
            }
            else
            {
                newPair[0] = matrix[pairInfo[0].yx.y][pairInfo[1].yx.x];
                newPair[1] = matrix[pairInfo[1].yx.y][pairInfo[0].yx.x];
            }

            return newPair;

        }

        public struct YX
        {
            public int x;
            public int y;
            public YX(int _x, int _y)
            {
                x = _x;
                y = _y;
            }
        }
        static YX GetYX(char[][] matrix, char c)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == c)
                        return new YX(j, i);
                }
            }
            return new YX(-1, -1);
        }

        static char[] DuplicatesPlayFair(char[] pair)
        {
            if (pair[0] == pair[1])
                return new char[] { pair[0], 'X' };
            return pair;
        }

        static string pfAlphabet = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
    }
}
