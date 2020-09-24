using System;
using System.Collections;
using System.Collections.Generic;

namespace Restaurant
{
    public class TableRequests : IEnumerable
    {
        public Dictionary<string, List<IMenuItem>> customerOrders = new Dictionary<string, List<IMenuItem>>();
        public int num = 0;

        public void Add<T>(string customerName) where T : IMenuItem
        {
            IMenuItem order = Activator.CreateInstance(typeof(T)) as IMenuItem;
            bool exists = customerOrders.ContainsKey(customerName);
            if (!exists)
                customerOrders.Add(customerName, new List<IMenuItem>());
            customerOrders[customerName].Add(order as CookedFood);
        }

        public List<IMenuItem> Get<T>()
        {
            List<IMenuItem> orders = new List<IMenuItem>();
            foreach (var order in customerOrders)
                foreach (var item in order.Value)
                    if (item is T)
                        orders.Add(item);
            return orders;
        }

        public object this[string customerName]
        {
            get => customerOrders[customerName];
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var order in customerOrders)
                yield return order.Key;
        }
    }
}
