using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressionTester
{
    public class Preview
    {
        private Dictionary<string, FileInfo> _sources;
        private Queue<(string Key, Image Image)> _cache;
        private int _cacheSize;

        public void AddSource(string key, string path)
        {
            var info = new FileInfo(path);

            if (!info.Exists)
            {
                throw new ArgumentException($"Invalid path to source ({path})", nameof(path));
            }

            _sources[key] = info;
        }

        public void AddSources(IEnumerable<(string, string)> sources)
        {
            foreach (var (key, path) in sources)
            {
                AddSource(key, path);
            }
        }

        public Image this[string key]
        {
            get
            {
                if (!_sources.TryGetValue(key, out var info))
                {
                    throw new ArgumentException($"Invalid source key ({key})", nameof(key));
                }

                return GetCached(key) ?? Load(key);
            }
        }

        public void Clear()
        {
            _sources.Clear();
            _cache.Clear();
        }

        private Image GetCached(string key) => 
            _cache.FirstOrDefault(cached => cached.Key.Equals(key)).Image;

        private Image Load(string key)
        {
            var info = _sources[key];
            var image = Image.FromFile(info.FullName);
            _cache.Enqueue((key, image));

            if (_cache.Count > _cacheSize)
                _cache.Dequeue();

            return image;
        }
    }
}
