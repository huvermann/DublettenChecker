# DublettenChecker
Checks for file dublicates.
Command line arguments are not supported, change the SearchPath and MODI manually.

## Sample Code

The Sammle_Kandidaten() method collects possible dublicates by file size and file name depending on Vergleichsmodi.
The Pruefe_Kandidaten takes the result of "Sammle_Kandidaten" and compares their md5 file hash code and returns the matches.


```csharp
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