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

            SourceDir = CreateOrGetDirInfo(Path.Combine(rootPath , "src"));
            CompressDir= CreateOrGetDirInfo(Path.Combine(rootPath, "comp"));
            RestoreDir = CreateOrGetDirInfo(Path.Combine(rootPath, "rest"));
        }

        public void AddToSources(string sourcePath, string distName, string extension)
        {
            var sourceInfo = new FileInfo(sourcePath);
            var distInfo = new FileInfo(Path.Combine(SourceDir.FullName, distName + extension));

            if (distInfo.Exists)
            {
                throw new ArgumentException("File with specified name already exists in source directory", nameof(distName));
            }

            sourceInfo.CopyTo(distInfo.FullName);
        }

        public FileInfo GetSource(string fileName)
            => new FileInfo(Path.Combine(SourceDir.FullName, fileName + ".bm"));
        
        public IEnumerable<FileInfo> GetSources(IEnumerable<string> fileNames)
            => fileNames.Select(GetSource);

        public IEnumerable<FileInfo> GetSources()
            => Directory
                .GetFiles(SourceDir.FullName)
                .Select(fileName => new FileInfo(fileName));

        public FileInfo GetCompressed(string name, string ext) 
            => new FileInfo(Path.Combine(SourceDir.FullName, name + ext));        

        public IEnumerable<FileInfo> GetCompressed(IEnumerable<string> names, string ext) 
            => names.Select(name => GetCompressed(name, ext));

        private static DirectoryInfo CreateOrGetDirInfo(string path)
        {
            var dir = new DirectoryInfo(path);
            if (!dir.Exists) dir.Create();

            return dir;
        }
    }
}
