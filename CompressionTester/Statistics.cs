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
        private Dictionary<string, StatisticsItem> _items = new Dictionary<string, StatisticsItem>();
        public IEnumerable<StatisticsItem> Items => _items.Values;



        public void AddItem(StatisticsItem item)
        {
            _items[item.Name] = item;
        }

        public void Update(IEnumerable<StatisticsItem> statisticItems)
        {
            _items.Update(statisticItems.Select(item => new KeyValuePair<string, StatisticsItem>(item.Name, item)));
        }        

        public void Clear()
        {
            _items.Clear();
        }

        public object Clone()
        {
            var copy = new Statistics();
            copy._items = new Dictionary<string, StatisticsItem>(_items);
            return copy;
        }
    }
}
