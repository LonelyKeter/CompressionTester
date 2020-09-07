using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressionTester
{
    public class Statistics : ICloneable
    {
        private List<StatisticsItem> _items = new List<StatisticsItem>();
        public IEnumerable<StatisticsItem> Items => _items;

        public void AddItem(in StatisticsItem item)
        {
            _items.Add(item);
        }

        public void Append(IEnumerable<StatisticsItem> statisticItems)
        {
            _items.AddRange(statisticItems);
        }
        public void Append(Statistics statistics)
        {
            _items.AddRange(statistics._items);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public object Clone()
        {
            var copy = new Statistics();
            copy._items = new List<StatisticsItem>(_items.Capacity);
            copy._items.AddRange(this._items);
            return copy;
        }
    }
}
