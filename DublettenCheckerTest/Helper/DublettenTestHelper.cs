using DublettenChecker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DublettenCheckerTest.Helper
{
    public static class DublettenTestHelper
    {
        private static IEnumerable<IFileCandidate> getDublicateFileWithDifferentNames()
        {
            List<IFileCandidate> result = new List<IFileCandidate>();
            result.Add(new FileCandidateMock()
            {
                FileName = "file1",
                FileHash = "hash1",
                FileSize = 10,
                FilePath = "path_of_File1"
            });
            result.Add(new FileCandidateMock()
            {
                FileName = "file2",
                FileHash = "hash1",
                FileSize = 10,
                FilePath = "path_of_File2"
            });

            return result;
        }

        private static IEnumerable<IFileCandidate> getDublicateFileWithSameNames()
        {
            List<IFileCandidate> result = new List<IFileCandidate>();
            result.Add(new FileCandidateMock()
            {
                FileName = "SameFileName",
                FileHash = "hash1",
                FileSize = 10,
                FilePath = "path_of_File1"
            });
            result.Add(new FileCandidateMock()
            {
                FileName = "SameFileName",
                FileHash = "hash1",
                FileSize = 10,
                FilePath = "path_of_File2"
            });

            return result;
        }

        public static IEnumerable<IDublette> GetMatchingDubletteWithDifferentFilenames()
        {
            List<IDublette> result = new List<IDublette>();
            result.Add(new Dublette(getDublicateFileWithDifferentNames()));
            return result;
        }

        public static IEnumerable<IDublette> GetMatchingDubletteWithSameFilenames()
        {
            List<IDublette> result = new List<IDublette>();
            result.Add(new Dublette(getDublicateFileWithSameNames()));
            return result;
        }
    }
}
