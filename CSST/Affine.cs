using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTI
{
    public static class Affine
    {
        public static string Encrypt(string input, int[] key)
        {
            var text = input.ToUpper().Split(' ').Select(w => new string(w.Select(x => f(x - 'A', key[0], key[1])).ToArray())).ToArray();
            return string.Join(" ", text);
        }

        static char f(int x, int a, int b)
        {
            return (char)((a * x + b) % 26 + 'A');
        }

        static char d(int y, int x, int b)
        {
            return (char)(((x * (y - b)) % 26 + 26) % 26 + 'A');
        }
        public static string Decrypt(string input, int[] key)
        {
            var mod = 1;
            while ((key[0] * mod % 26) != 1)
                mod++;
            var text = input.ToUpper().Split(' ').Select(w => new string(w.Select(x => d(x - 'A', mod, key[1])).ToArray())).ToArray();
            return string.Join(" ", text);
        }
    }
}
