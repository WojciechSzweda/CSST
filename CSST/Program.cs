using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BTI
{
    class Program
    {
        delegate void F();

        static void TestAll()
        {
            var methods = typeof(Test).GetMethods(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(x => x.Name.ToLower().Contains("test"));

            foreach (var method in methods)
            {
                ((F)method.CreateDelegate(typeof(F)))();
            }
        }

        static void Main(string[] args)
        {

            TestAll();

            //Test.TestCaesar();
            //Test.TestAffine();
            //Test.TestVigener();
            //Test.TestPlayfair();
        }
    }
}
