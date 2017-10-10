using System.Collections.Generic;

namespace DublettenChecker
{
    public interface IDublettenpruefung
    {
        IEnumerable<IDublette> Sammle_Kandidaten(string pfad);
        IEnumerable<IDublette> Sammle_Kandidaten(string pfad, VergleichsModi modus);
        IEnumerable<IDublette> Pruefe_Kandidaten(IEnumerable<IDublette> kandidaten);
    }
}
