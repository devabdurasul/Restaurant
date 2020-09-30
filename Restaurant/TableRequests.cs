using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant
{
    public class TableRequests : Dictionary<string, List<IMenuItem>>
    {
        public void Add<T>(string customerName) where T : IMenuItem
        {
            if (!ContainsKey(customerName))
                Add(customerName, new List<IMenuItem>());
            this[customerName].Add(Activator.CreateInstance(typeof(T)) as CookedFood);
        }

        public List<IMenuItem> Get<T>()
        {
            return this.SelectMany(order => order.Value).Where(order => order is T).ToList();
        }
    }
}
