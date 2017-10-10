using DublettenChecker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DublettenCheckerTest.Helper
{
    public class FileCandidateMock : IFileCandidate
    {
        public string FileHash { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
    }
}
