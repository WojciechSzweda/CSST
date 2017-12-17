using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTI
{
    public static class Caesar
    {
        static List<char> alfa = "AĄBCĆDEĘFGHIJKLŁMNŃOÓPRSŚTUWYZŹŻ".ToCharArray().ToList();
        public static string Encrypt(string input, int move)
        {
            return string.Join(" ", input.Split(' ').Select(word => word.Select(x => alfa[(alfa.IndexOf(char.ToUpper(x)) + (move % alfa.Count)) % alfa.Count]).ToArray()).Select(word => new string(word)).ToArray());
        }

        public static string Decrypt(string input, int move)
        {
            return string.Join(" ", input.Split(' ').Select(word => word.Select(x => alfa[(alfa.IndexOf(char.ToUpper(x)) - (move % alfa.Count) + alfa.Count) % alfa.Count]).ToArray()).Select(word => new string(word)).ToArray());

        }
    }
}
