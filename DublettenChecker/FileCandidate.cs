using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DublettenChecker
{
    public class FileCandidate : IFileCandidate
    {
        private readonly string _path;
        private FileInfo _fileInfo;

        public FileCandidate(string path)
        {
            _path = path;
            _fileInfo = new FileInfo(path);
        }

        public string Path { get { return _path; } }

        public string FileName
        {
            get { return System.IO.Path.GetFileName(_path); }
        }

        public long FileSize
        {
            get
            {
                return _fileInfo.Length;
            }
        }

        private byte[] ComputeHash(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    return md5.ComputeHash(stream);
                }
            }
        }

        public string FileHash
        {
            get
            {
                string result = Convert.ToBase64String(ComputeHash(_path));
                return result;
            }
        }

        public string FilePath
        {
            get
            {
                return _path;
            }
        }
    }
}
