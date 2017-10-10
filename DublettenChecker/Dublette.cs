using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DublettenChecker
{
    public class Dublette : IDublette
    {
        private readonly IEnumerable<IFileCandidate> _candidates;


        public Dublette(IEnumerable<IFileCandidate> candidates)
        {
            _candidates = candidates;
        }
        public IEnumerable<string> Dateipfade
        {
            get
            {
                return _candidates.Select(c => c.FilePath).ToList();
            }
        }

        public IEnumerable<IFileCandidate> Candidates { get { return _candidates; } }
    }
}
