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

        private Project(
            Hierarchy hierarchy, 
            Statistics statistics, 
            ProjectFile projectFile)
        {
            Hierarchy = hierarchy;
            _statistics = statistics;
            _projectFile = projectFile;

            Title = _projectFile.Title;
        }

        public Statistics GetStatistics() => (Statistics)_statistics.Clone();


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
    }
}
