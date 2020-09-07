using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressionTester
{
    public class Hierarchy
    {
        public DirectoryInfo RootDir { get; private set; }
        public DirectoryInfo SourceDir { get; private set; }
        public DirectoryInfo CompressDir { get; private set; }
        public DirectoryInfo RestoreDir { get; private set; }

        public Hierarchy(string rootPath)
        {
            RootDir = CreateOrGetDirInfo(rootPath);

            SourceDir = CreateOrGetDirInfo(rootPath + "\\src");
            CompressDir= CreateOrGetDirInfo(rootPath + "\\comp");
            RestoreDir = CreateOrGetDirInfo(rootPath + "\\rest");
        }

        public void AddToSources(string sourcePath, string distName)
        {
            var sourceInfo = new FileInfo(sourcePath);
            var distInfo = new FileInfo(SourceDir.FullName + "\\" + distName + "\\.bm");

            if (distInfo.Exists)
            {
                throw new ArgumentException("File with specified name already exists in source directory", nameof(distName));
            }

            sourceInfo.CopyTo(distInfo.FullName);
        }

        public FileInfo GetSource(string name)
        {
            return new FileInfo(SourceDir.FullName + "\\" + name + "\\.bm");
        }
        public IEnumerable<FileInfo> GetSources(IEnumerable<string> names)
        {
            return names.Select(GetSource);
        }

        public FileInfo GetCompressed(string name, string ext)
        {
            return new FileInfo(SourceDir.FullName + "\\" + name + "\\.bm");
        }
        public IEnumerable<FileInfo> GetCompressed(IEnumerable<string> names, string ext)
        {
            return names.Select(name => GetCompressed(name, ext));
        }

        private static DirectoryInfo CreateOrGetDirInfo(string path)
        {
            var dir = new DirectoryInfo(path);
            if (!dir.Exists) dir.Create();

            return dir;
        }
    }
}
