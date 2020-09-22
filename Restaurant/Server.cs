using System;

namespace Restaurant
{
    public class Server
    {
        public string[] ServeDrinkings = new string[8];
        public int receiveIndex = 0;
        private TableRequests tableRequests;
        private Drink drinking = new Pepsi();
        private int chickenQ;
        private int eggQ;


        public string Receive(string chickenQ, string eggQ, string drinkingType, TableRequests tableRequests)
        {
            this.tableRequests = tableRequests;
            if (receiveIndex > 7)
                throw new Exception("Up to 8 customers are allowed per table. Send to Cook first!");
            this.chickenQ = Convert.ToInt32(chickenQ);
            this.eggQ = Convert.ToInt32(eggQ);
            if (drinkingType == "Cola")
                drinking = new Cola();
            if (drinkingType == "Tea")
                drinking = new Tea();
            ServeDrinkings[receiveIndex] = drinking.GetType().Name;

            tableRequests.customerOrders[receiveIndex] = new IMenuItem[this.chickenQ + this.eggQ];
            tableRequests.num = 0;
            for (int i = 0; i < this.chickenQ; i++)
            {
                this.tableRequests.Add(receiveIndex, new Chicken(this.chickenQ));
            }
            for (int i = 0; i < this.eggQ; i++)
            {
                this.tableRequests.Add(receiveIndex, new Egg(this.eggQ));
            }

            receiveIndex++;
            return "Request received!";
        }

        public int? Send(Cook cook)
        {
            return cook.Process(tableRequests);
        }
        public string Serve(int index)
        {
            var chQ = 0;
            var eQ = 0;
            var requests = tableRequests[index] as IMenuItem[];

            //TODO: Use second indexer of table request to get list of a customer orders
            foreach (IMenuItem request in requests)
            {
                if (request == null) continue;
                if (request is Chicken)
                    chQ++;
                if (request is Egg)
                    eQ++;
            }
            var str = "Customer " + index + " is served " + chQ + " chicken, " + eQ + " egg, " + ServeDrinkings[index];
            return str;
        }
    }
}
