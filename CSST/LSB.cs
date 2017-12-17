using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace BTI
{
    public static class LSB
    {
        static byte ChangeByte(byte input, int value)
        {
            var c = Convert.ToString(input, 2).ToCharArray();
            c[c.Length - 1] = value.ToString()[0];
            return Convert.ToByte(new string(c), 2);
        }

        static int GetLastBit(byte input)
        {
            return (int)char.GetNumericValue(Convert.ToString(input, 2).Last());
        }


        public static void EncryptInImage(string path, string input)
        {
            var img = new Bitmap(path);
            var pixels = DEShelper.GetPixels(img).ToArray();
            var binInput = DEShelper.TextToBinary(input).ToArray();
            for (int i = 0; i < binInput.Count(); i++)
            {
                img.SetPixel(i % img.Width, i / img.Width, Color.FromArgb(pixels[i].A, ChangeByte(pixels[i].R, binInput[i]), pixels[i].G, pixels[i].B));
            }
            img.Save($"encrypted{path}");
        }
        public static string DecryptTextFromImage(string path, int length)
        {
            var img = new Bitmap(path);
            var pixels = DEShelper.GetPixels(img).ToArray();
            var binOutput = new List<int>();
            for (int i = 0; i < 8 * length; i++)
            {
                binOutput.Add(GetLastBit(pixels[i].R));
            }
            return DEShelper.BinaryToText(binOutput.ListToString());
        }
    }
}
