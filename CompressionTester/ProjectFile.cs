using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressionTester
{
    public class ProjectFile
    {
        private readonly FileInfo _fileInfo;

        public string DirectoryName => _fileInfo.DirectoryName;
        public string Title { get; private set; }

        public bool Opened { get; private set; }

        private ProjectFile(FileInfo fileInfo, string title)
        {
            _fileInfo = fileInfo;
            Title = title;
        }

        public static bool ExistsInDirectory(DirectoryInfo directoryInfo) 
            => Directory.GetFiles(directoryInfo.FullName, "*.ctst").Length > 0;
        
        public static ProjectFile Create(string rootDirectory, string title)
            => Create(new DirectoryInfo(rootDirectory), title);

        public static ProjectFile Create(DirectoryInfo rootDirectory, string title)
        {
            Assert.FileDoesNotExistInDirectory(rootDirectory);

            var info = new FileInfo(Path.Combine(rootDirectory.FullName, title + ".ctst"));
            using (var fs = info.Create())
            {
                var encoding = Encoding.ASCII;
                //Write title length in bytes
                fs.Write(
                    BitConverter.GetBytes(encoding.GetByteCount(title)),
                    0,
                        sizeof(int));
                //Write title
                fs.Write(
                    encoding.GetBytes(title),
                    0,
                    encoding.GetByteCount(title));
            }

            return new ProjectFile(info, title);
        }

        public static ProjectFile Open(string path) 
            => Open(new FileInfo(path));

        public static ProjectFile Open(FileInfo fileInfo)
        {
            Assert.FileExists(fileInfo);
            Assert.FileExtensionMatches(fileInfo);

            string title;

            using (var fs = fileInfo.OpenRead())
            {
                var encoding = Encoding.ASCII;
                //Read byte length of title
                var buffer = new byte[sizeof(int)];
                fs.Read(
                    buffer,
                    0,
                    sizeof(int));

                //Read title
                buffer = new byte[BitConverter.ToInt32(buffer, 0)];
                fs.Read(
                    buffer,
                    0,
                    buffer.Length);

                title = encoding.GetString(buffer);
            }

            return new ProjectFile(fileInfo, title);
        }

        

        private static class Assert
        {
            public static void FileExists(FileInfo info)
            {
                if (!info.Exists)
                    throw new ArgumentException("Invalid path. Project file does not exist");
            }

            public static void FileDoesNotExistInDirectory(DirectoryInfo rootDirectory)
            {
                if (ExistsInDirectory(rootDirectory))
                    throw new ArgumentException("Invalid path. Project file already exists in specified directory");
            }

            public static void FileExtensionMatches(FileInfo info)
            {
                if (!info.Extension.Equals(".ctst"))
                    throw new ArgumentException("Invalid project file extension.");
            }
        }
    }
}
