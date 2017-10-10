using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DublettenChecker
{
    class Program
    {
        const string SEARCHPATH = "../../Suchordner";
        const VergleichsModi MODI = VergleichsModi.Size;
        
        static void Main(string[] args)
        {
            Console.WriteLine("Suchmodus: " + DisplaySuchmodi(MODI));

            var checker = new FileDublettenChecker();
            var candidates = checker.Sammle_Kandidaten(SEARCHPATH, MODI);
            var checkedDublicates = checker.Pruefe_Kandidaten(candidates);

            foreach (var item in checkedDublicates)
            {
                Console.WriteLine("Folgende Dubletten gefunden:");
                foreach (var fileList in item.Dateipfade)
                {
                    Console.WriteLine(fileList);
                }
                Console.WriteLine("-----");
            }

            
            Console.ReadLine();
        }

        private static string DisplaySuchmodi(VergleichsModi mode)
        {
            return mode == VergleichsModi.Size ? "Nur nach Größe vergleichen" : "Nach Größe und FileName vergleichen.";
        }
    }
}
