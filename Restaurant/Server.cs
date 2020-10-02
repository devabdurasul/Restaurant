using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Server
    {
        public List<string> ServeDrinkings = new List<string>();
        public List<string> ServeOrders = new List<string>();
        private int receiveIndex = 0; 
        private TableRequests tableRequests;        

        public string Receive(string chickenQ, string eggQ, string customerName, string drinkingType, TableRequests tableRequests)
        {
            this.tableRequests = tableRequests;
            if (receiveIndex > 7)
                throw new Exception("Up to 8 customers are allowed per table. Send to Cook first!");
            var chQ = Convert.ToInt32(chickenQ);
            var eQ = Convert.ToInt32(eggQ);
            Drink drinking = new Pepsi();
            if (drinkingType == "Cola")
                drinking = new Cola();
            if (drinkingType == "Tea")
                drinking = new Tea();
            ServeDrinkings.Add(customerName.ToUpper() + " is served " + drinking.GetType().Name);

            for (int i = 0; i < chQ; i++)
                tableRequests.Add<Chicken>(customerName);
            for (int i = 0; i < eQ; i++)
                tableRequests.Add<Egg>(customerName);
            receiveIndex++;
            return "Request received from: " + customerName.ToUpper();
        }

        public List<string> Serve(Task t)
        {
            var orders = tableRequests.OrderBy(request => request.Key);
            //TODO: Please LINQ here instead of foreach
            foreach (var order in orders)
            {
                var chQ = order.Value.Where(request => request is Chicken).Count();
                var eQ = order.Value.Where(request => request is Egg).Count();
                ServeOrders.Add(order.Key.ToUpper() + " is served " + chQ + " chicken, " + eQ + " egg");
            }
            Thread.Sleep(1000);
            return ServeOrders;
        }
    }
}
