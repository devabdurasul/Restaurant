using System;
using System.Collections;
using System.Collections.Generic;

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
            List<IMenuItem> orders = new List<IMenuItem>();
            foreach (var order in this)
                foreach (var item in order.Value)
                    if (item is T)
                        orders.Add(item);
            return orders;
        }
    }
}
