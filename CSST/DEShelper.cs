using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTI
{

    public static class DEShelper
    {
        static Random rng = new Random();

        public static IEnumerable<int> GenerateRandomData(int count = 64)
        {
            return Enumerable.Range(0, count).Select(x => rng.Next(2));
        }

        public static void GenerateRandomDataBlocks(string name, int blocks = 5)
        {
            SaveBlocksToFile(name, Enumerable.Range(0, blocks).Select(x => GenerateRandomData()));
        }

        public static IEnumerable<int> GeneratePreDefData(int count = 64, int num = 0)
        {
            return Enumerable.Range(0, count).Select(x => num);
        }

        public static IEnumerable<int> GenerateDataByBytes(int first, int second)
        {
            var list = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                list = list.Concat(Enumerable.Range(0, 8).Select(x => first).Concat(Enumerable.Range(0, 8).Select(x => second)).ToList()).ToList();

            }
            return list.ToList();
        }

        public static IEnumerable<int> GenerateZeroOne(int count = 64)
        {
            return Enumerable.Range(0, count).Select(x => x % 2);
        }

        public static IEnumerable<int> GenerateOneZero(int count = 64)
        {
            return Enumerable.Range(0, count).Select(x => (x % 2 + 1) % 2);
        }

        public static void GeneratePreDefDataBlocks(string name, int num = 0, int blocks = 5)
        {
            SaveBlocksToFile(name, Enumerable.Range(0, blocks).Select(x => GeneratePreDefData(64, num)));
        }

        public static IEnumerable<IEnumerable<int>> GeneratePreDefArrayBlocks(IEnumerable<int> nums)
        {
            return nums.Select(x => GeneratePreDefData(64, x).ToList());
        }

        public static IEnumerable<IEnumerable<int>> GeneratePreDefFuncArrayBlocks(IEnumerable<Func<IEnumerable<int>>> funcs)
        {
            return funcs.Select(x => x());
        }

        public static IEnumerable<int> GenerateByBitsCount(int count, int first, int second)
        {
            return Enumerable.Range(0, 64).Select(x => (x % (count * 2)) >= count ? first : second);
        }

        public static void SaveToFile(string name, string text)
        {
            File.WriteAllText(name, text);
        }

        public static void SaveBlocksToFile(string name, IEnumerable<IEnumerable<int>> data)
        {
            File.WriteAllLines(name, data.Select(x => string.Join("", x)));
        }

        public static void MakeArray(string text)
        {
            File.WriteAllText("array.txt", $"static int[] name = new int[] {{ { string.Join(",", text.Replace(Environment.NewLine, string.Empty).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))} }};");
        }

        public static IEnumerable<int> stringTiList(string data)
        {
            return data.Select(x => (int)char.GetNumericValue(x)).ToList();
        }

        public static IEnumerable<int> GetDataFromFile(string name)
        {
            return File.ReadAllText(name).ToIntList();
        }

        public static IEnumerable<IEnumerable<int>> GetDataBlocksFromFile(string name)
        {
            return File.ReadAllLines(name).Select(x => x.ToIntList());
        }

        public static IEnumerable<Color> GetPixels(string path)
        {
            var img = new Bitmap(path);
            var colors = new List<Color>();
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    colors.Add(img.GetPixel(j, i));
                }
            }

            return colors;
        }
        public static IEnumerable<Color> GetPixels(Bitmap img)
        {
            var colors = new List<Color>();
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    colors.Add(img.GetPixel(j, i));
                }
            }

            return colors;
        }
        public static void SaveImage64x64(IEnumerable<IEnumerable<int>> data, string name)
        {
            var colors = new List<string>();
            data.Select(x => x.ToHex()).ToList().ForEach(x => { colors.Add(new string(x.Take(8).ToArray())); colors.Add(new string(x.Skip(8).ToArray())); });
            int index = 0;
            var bitmap = new Bitmap(64, 64);
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    bitmap.SetPixel(i, j, Color.FromArgb(Int32.Parse(colors[index++], NumberStyles.HexNumber)));
                }
            }

            bitmap.Save(name);
        }

        public static IEnumerable<int> BitListOperation(IEnumerable<int> input, Func<int, int> func)
        {
            return Convert.ToString(func(Convert.ToInt32(input.ListToString(), 2)), 2).PadLeft(32, '0').ToIntList();
        }

        public static IEnumerable<int> TextToBinary(string text)
        {
            return string.Join("", Encoding.UTF8.GetBytes(text).Select(x => Convert.ToString(x, 2).PadLeft(8, '0'))).ToIntList();
        }

        public static string BinaryToText(string input)
        {
            var text = new List<byte>();
            var bytes = Math.Ceiling(input.Count() / 8.0);
            for (int i = 0; i < bytes; i++)
            {
                text.Add(Convert.ToByte(new string(input.Skip(i * 8).Take(8).ToArray()), 2));
            }

            return Encoding.UTF8.GetString(text.ToArray());
        }
    }
}
