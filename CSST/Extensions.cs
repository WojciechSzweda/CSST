using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BTI
{
    public static class Extensions
    {
        public static IEnumerable<int> ToIntList(this string data)
        {
            return data.Replace(" ", string.Empty).Select(x => (int)char.GetNumericValue(x)).ToList();
        }

        public static string ToHex(this IEnumerable<int> data)
        {
            return string.Join("", Regex.Matches(data.ListToString(), "....").Cast<Match>().Select(x => x.Value).Select(x => Convert.ToInt16(x, 2).ToString("X")).ToList());
        }

        public static string ToDec(this IEnumerable<int> data)
        {
            BigInteger big = 0;

            data.ToList().ForEach(x => { big <<= 1; big += x == 1 ? 1 : 0; });
            return big.ToString();
        }

        public static string ListToString(this IEnumerable<int> data)
        {
            return string.Join("", data);
        }

        public static string BlocksToString(this IEnumerable<IEnumerable<int>> data)
        {
            return string.Join("\n", data.Select(x => x.ListToString()));
        }

        public static string BlocksToHex(this IEnumerable<IEnumerable<int>> data)
        {
            return string.Join("\n", data.Select(x => x.ToHex()));
        }

        public static IEnumerable<int> To64Bit(this IEnumerable<int> input)
        {
            return Enumerable.Repeat(0, Math.Max(0, 64 - input.Count())).Concat(input);
        }

        public static IEnumerable<int> AddInt(this IEnumerable<int> input, int number)
        {
            return Convert.ToString(Convert.ToInt32(input.ListToString(), 2) + number, 2).ToIntList();
        }
    }
}
