using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DublettenChecker
{
    public class FileDublettenChecker : IDublettenpruefung
    {
        /// <summary>
        /// Check if candidates really matching.
        /// </summary>
        /// <param name="kandidaten">Possible same file candidates.</param>
        /// <returns>Returns all matching candidates with same hash.</returns>
        public IEnumerable<IDublette> Pruefe_Kandidaten(IEnumerable<IDublette> kandidaten)
        {
            List<IDublette> result = new List<IDublette>();
            foreach(IDublette dub in kandidaten)
            {
                var hits = dub.Candidates
                    .GroupBy(h => h.FileHash)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.ToList())
                    .Select(l => new Dublette(l));
                result.AddRange(hits);
            }
            return result;
        }

        /// <summary>
        /// Returns all files recursively from a path.
        /// </summary>
        /// <param name="pfad">The search root path.</param>
        /// <returns>List of files.</returns>
        private IEnumerable<FileCandidate> GetFiles(string pfad)
        {
            var allFiles = Directory.GetFiles(pfad, "*.*", SearchOption.AllDirectories);
            return allFiles.Select(file => new FileCandidate(file));
        }

        /// <summary>
        /// Collects file candidates for matching.
        /// </summary>
        /// <param name="pfad">The search root path.</param>
        /// <returns>Returns possible dublicates.</returns>
        public IEnumerable<IDublette> Sammle_Kandidaten(string pfad)
        {
            return GetFiles(pfad)
                .GroupBy(size => size.FileSize)
                .Where(g => g.Count() > 1)
                .Select(g => new Dublette(g));
        }

        /// <summary>
        /// Collects file candidates for matching.
        /// </summary>
        /// <param name="pfad">The search root path.</param>
        /// <param name="modus">The comparison mode.</param>
        /// <returns></returns>
        public IEnumerable<IDublette> Sammle_Kandidaten(string pfad, VergleichsModi modus)
        {
            IEnumerable<IDublette> result;
            switch (modus)
            {
                case VergleichsModi.SizeAndName:
                    result = GetFiles(pfad)
                        .GroupBy(x => new { x.FileName, x.FileSize })
                        .Where(g => g.Count() > 1)
                        .Select(g => new Dublette(g));
                    break;

                case VergleichsModi.Size:
                    result = Sammle_Kandidaten(pfad); ;
                    break;
                default:
                    throw new ArgumentException("Unbekannter Vergleichsmodi");
            }

            return result;
        }
    }
}
