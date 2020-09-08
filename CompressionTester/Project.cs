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
        private static string _tagSeparator = "_";

        public string Title { get; private set; }

        public Hierarchy Hierarchy { get; private set; }

        private ProjectFile _projectFile;
        private Statistics _statistics;

        private Dictionary<string, FileInfo> _sources;
        public IEnumerable<string> SourceTags => _sources.Keys;

        private Project(
            Hierarchy hierarchy, 
            Statistics statistics, 
            ProjectFile projectFile)
        {
            Hierarchy = hierarchy;
            _statistics = statistics;
            _projectFile = projectFile;

            Title = _projectFile.Title;

            _sources = new Dictionary<string, FileInfo>();

            AddSources(Hierarchy.GetSources());
        }

        public Statistics GetStatistics() => _statistics.Clone() as Statistics;

        public void AddSource(string filePath)
            => AddSource(new FileInfo(filePath));
        public void AddSource(FileInfo fileInfo)
        {
            Assert.FileExists(fileInfo);
            Assert.SourceDoesNotContain(fileInfo, _sources);

            var tag = CreateSourceTag(fileInfo);
            Hierarchy.AddToSources(fileInfo.FullName, tag, ".bm");
            _sources.Add(tag, fileInfo);
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
                tag = dirInfo.Name + _tagSeparator + "tag";
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

            public static void SourceDoesNotContain(FileInfo info, Dictionary<string, FileInfo> sources)
            {
                if (sources.ContainsValue(info))
                {
                    throw new ArgumentException("File is already included as source");
                }
            }
        }
    }
}
