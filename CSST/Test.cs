using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTI
{
    public static class Test
    {
        public static void TestCaesar()
        {
            var move = 66669;
            var text = "MĘŻNY BĄDŹ, CHROŃ PUŁK TWÓJ I SZEŚĆ FLAG";
            Console.WriteLine($"Text: {text}");
            Console.WriteLine($"Move: {move}");
            var encrypted = Caesar.Encrypt(text, move);
            Console.WriteLine($"Encrypted: {encrypted}");
            var decrypted = Caesar.Decrypt(encrypted, move);
            Console.WriteLine($"Decrypted: {decrypted}");
            Console.WriteLine();
        }

        public static void TestVigener()
        {
            Console.WriteLine("Vigener Cipher");
            var text = "TO JEST BARDZO TAJNY TEKST";
            var keyWord = "NT OJES TBARDZ OTAJN YTEKS";
            Console.WriteLine($"Text: {text}");
            Console.WriteLine($"KeyWord: {keyWord}");
            var vigener = Vigener.Encrypt(keyWord, text);
            Console.WriteLine($"Encrypted: {vigener}");
            var vigenerDe = Vigener.Decrypt(keyWord, vigener);
            Console.WriteLine($"Decrypted: {vigenerDe}");
            Console.WriteLine();
        }

        public static void TestAffine()
        {
            Console.WriteLine("Affine Cipher");
            var text = "KOT";
            var key = new int[] { 7, 5 };
            Console.WriteLine($"text: {text}");
            Console.WriteLine($"key: ({string.Join(",", key.Select(x => x.ToString()).ToArray())})");
            var ciphered = Affine.Encrypt(text, key);
            Console.WriteLine($"Encrypted: {ciphered}");
            var deciphered = Affine.Decrypt(ciphered, key);
            Console.WriteLine($"Decrypted: {deciphered}");
            Console.WriteLine();
        }

        public static void TestPlayfair()
        {
            Console.WriteLine("PlayFair Cipher");
            var keyPF = "Dres";
            var textPF = "fajnie";
            Console.WriteLine($"Text: {textPF}");
            Console.WriteLine($"Key: {keyPF}");
            var cipheredPF = Playfair.Encrypt(textPF, keyPF);
            Console.WriteLine($"Encrypted: {cipheredPF}");
            var decipheredPF = Playfair.Decrypt(cipheredPF, keyPF);
            Console.WriteLine($"Decrypted: {decipheredPF}");
            Console.WriteLine();
        }

        static DES des = new DES();

        public static void DEStest()
        {
            var data = DEShelper.GenerateRandomData().ToList();
            var key = DEShelper.GenerateRandomData().ToList();

            Console.WriteLine($"data BIN: {data.ListToString()}");
            Console.WriteLine($"data HEX: {data.ToHex()}");
            Console.WriteLine();
            Console.WriteLine($"key BIN: {key.ListToString()}");
            Console.WriteLine($"key HEX: {key.ToHex()}");
            Console.WriteLine();

            var encrypted = des.Encrypt(data, key);
            Console.WriteLine($"encypted BIN {encrypted.ListToString()}");
            Console.WriteLine($"encypted HEX {encrypted.ToHex()}");


            Console.WriteLine();
            var decrypted = des.Decrypt(encrypted, key);
            Console.WriteLine($"decrypted BIN: {decrypted.ListToString()}");
            Console.WriteLine($"decrypted HEX: {decrypted.ToHex()}");

        }

        public static void DES_ECBtest()
        {
            var name = "sam";
            var key = DEShelper.GenerateRandomData().ToList();

            des.EncryptImage($"{name}.png", key, $"encrypted{name}.png");
            des.DecryptImage($"encrypted{name}.png", key, $"decrypted{name}.png");

        }

        public static void DES_CBCtest()
        {
            var vector = DEShelper.GenerateRandomData().ToList();
            var key = DEShelper.GenerateRandomData().ToList();

            var name = "blocksCBC";
            DEShelper.GenerateRandomDataBlocks($"{name}.txt", 8);
            var cbcE = des.EncryptCBC(DEShelper.GetDataBlocksFromFile($"{name}.txt"), key, vector);
            DEShelper.SaveBlocksToFile($"{name}encrypted.txt", cbcE);
            var cbcD = des.DecryptCBC(DEShelper.GetDataBlocksFromFile($"{name}encrypted.txt"), key, vector);
            DEShelper.SaveBlocksToFile($"{name}decrypted.txt", cbcD);
        }

        public static void DES_CFBtest()
        {
            var vector = DEShelper.GenerateRandomData().ToList();
            var key = DEShelper.GenerateRandomData().ToList();

            var name = "blocksCFB";
            DEShelper.GenerateRandomDataBlocks($"{name}.txt", 8);
            var cfbE = des.EncryptCFB(DEShelper.GetDataBlocksFromFile($"{name}.txt"), key, vector);
            DEShelper.SaveBlocksToFile($"{name}encrypted.txt", cfbE);
            var cfbD = des.DecryptCFB(DEShelper.GetDataBlocksFromFile($"{name}encrypted.txt"), key, vector);
            DEShelper.SaveBlocksToFile($"{name}decrypted.txt", cfbD);
        }

        public static void DES_OFBtest()
        {
            var vector = DEShelper.GenerateRandomData().ToList();
            var key = DEShelper.GenerateRandomData().ToList();

            var name = "blocksOFB";
            DEShelper.GenerateRandomDataBlocks($"{name}.txt", 8);
            var ofbE = des.OFB(DEShelper.GetDataBlocksFromFile($"{name}.txt"), key, vector);
            DEShelper.SaveBlocksToFile($"{name}encrypted.txt", ofbE);
            var ofbD = des.OFB(DEShelper.GetDataBlocksFromFile($"{name}encrypted.txt"), key, vector);
            DEShelper.SaveBlocksToFile($"{name}decrypted.txt", ofbD);
        }

        public static void DES_PCBCtest()
        {
            var vector = DEShelper.GenerateZeroOne();
            var key = DEShelper.GenerateDataByBytes(0, 1);
            var data = DEShelper.GeneratePreDefArrayBlocks(new int[] { 0, 1, 0 });

            Console.WriteLine($"data:\n{data.BlocksToString()}");
            Console.WriteLine($"\nkey:\n{key.ListToString()}");
            Console.WriteLine($"\nvector:\n{vector.ListToString()}");

            var pcbcE = des.EncryptPCBC(data, key, vector);
            Console.WriteLine($"\nEncrypted:\n{pcbcE.BlocksToString()}");
            var pcbcD = des.DecryptPCBC(pcbcE, key, vector);
            Console.WriteLine($"\nDecrypted:\n{pcbcD.BlocksToString()}");
        }

        public static void DES_CTRtest()
        {
            var key = DEShelper.GenerateDataByBytes(0, 1);
            var data = DEShelper.GeneratePreDefArrayBlocks(new int[] { 0, 1, 0 });
            var nonce = DEShelper.GenerateOneZero(32);
            var counter = DEShelper.GeneratePreDefData(32);

            Console.WriteLine($"data:\n{data.BlocksToString()}");
            Console.WriteLine($"\nkey:\n{key.ListToString()}");
            Console.WriteLine($"\nCTR:\n{nonce.ListToString()}{counter.ListToString()}");

            var func = new Func<int, int>((x) => x + 1);
            var cntFunction = new Func<IEnumerable<int>, IEnumerable<int>>((input) => DEShelper.BitListOperation(input, (x) => x + 1));
            var cntFunction2 = new Func<IEnumerable<int>, IEnumerable<int>>((input) => DEShelper.BitListOperation(input, (x) => 5 * (x + 1) + 3));

            var ctrE = des.CTR(data, key, nonce, counter, cntFunction);
            Console.WriteLine($"\nEncrypted:\n{ctrE.BlocksToString()}");
            var ctrD = des.CTR(ctrE, key, nonce, counter, cntFunction);
            Console.WriteLine($"\nDecrypted:\n{ctrD.BlocksToString()}");
        }

        public static void LSB_test()
        {
            var text = "hahah ATH najlepsza uczelnia";
            var imgName = "ATH.bmp";
            LSB.EncryptInImage(imgName, text);

            var output = LSB.DecryptTextFromImage($"encrypted{imgName}", text.Length);

            Console.WriteLine($"Decrypted: {output}");
        }
    }
}
