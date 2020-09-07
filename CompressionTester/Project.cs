using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CompressionTester
{
    public class Project
    {
        public string Title { get; private set; }

        public Hierarchy Hierarchy { get; private set; }

        private ProjectFile _projectFile;
        private Statistics _statistics;

        private HashSet<string> _sourceTags;

        private Project(
            Hierarchy hierarchy, 
            Statistics statistics, 
            ProjectFile projectFile)
        {
            Hierarchy = hierarchy;
            _statistics = statistics;
            _projectFile = projectFile;

            Title = _projectFile.Title;

            _sourceTags = new HashSet<string>();
        }

        public Statistics GetStatistics() => (Statistics)_statistics.Clone();

        public void AddSource(string filePath)
            => AddSource(new FileInfo(filePath));
        public void AddSource(FileInfo fileInfo)
        {
            Assert.FileExists(fileInfo);

            var tag = CreateSourceTag(fileInfo);
            Hierarchy.AddToSources(fileInfo.FullName, tag, ".bm");
            _sourceTags.Add(tag);
        }

        public void AddSources(IEnumerable<string> filePaths)
            => AddSources(filePaths.Select(path => new FileInfo(path)));
        public void AddSources(IEnumerable<FileInfo> fileInfos)
        {
            foreach (var info in fileInfos)
            {
                AddSource(info);
            }
        }

        private string CreateSourceTag(FileInfo sourceInfo)
        {
            var tag = Path.GetFileNameWithoutExtension(sourceInfo.Name);
            var dirInfo = sourceInfo.Directory;
            
            while (_sourceTags.Contains(tag))
            {
                tag = dirInfo.Name + "_" + "tag";
                dirInfo = dirInfo.Parent;
            }

            return tag;
        }


        #region Static creation
        public static Project Create(string dirPath, string name)
            => Create(new DirectoryInfo(dirPath), name);

        public static Project Create(DirectoryInfo dirInfo, string name)
        {
            var projectFile = ProjectFile.Create(dirInfo, name);
            return FromProjectFile(projectFile);
        }

        public static Project Open(string path)
            => Open(new FileInfo(path));
        
        public static Project Open(FileInfo fileInfo)
        {
            var projectFile = ProjectFile.Open(fileInfo);
            return FromProjectFile(projectFile);
        }

        private static Project FromProjectFile(ProjectFile projectFile)
        {
            var hierarchy = new Hierarchy(projectFile.DirectoryName);
            var statistics = new Statistics();

            return new Project(hierarchy, statistics, projectFile);
        }
        #endregion //Static creation

        private static class Assert
        {
            public static void FileExists(FileInfo fileInfo)
            {
                if (!fileInfo.Exists)
                {
                    throw new ArgumentException("Specified file should exist");
                }
            }
        }
    }
}
