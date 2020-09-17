using System;

namespace Restaurant
{
    public class Server
    {
        public string[] ServeOrders = new string[8];
        public string[] ServeDrinkings  = new string[8];
        public int receiveIndex = 0;
        private TableRequests tableRequests;
        private Drink drinking = new Pepsi();


        public string Receive(string chickenQ, string eggQ, string drinkingType, TableRequests tableRequests)
        {
            this.tableRequests = tableRequests;
            if (receiveIndex > 7)
                throw new Exception("Up to 8 customers are allowed per table. Send to Cook first!");
           
            if (drinkingType == "Cola")
                drinking = new Cola();
            if (drinkingType == "Tea")
                drinking = new Tea();
            ServeDrinkings[receiveIndex] = drinkingType;

            var summaryOrder = Convert.ToInt32(chickenQ) + Convert.ToInt32(eggQ);
            tableRequests.InitOrder(receiveIndex, summaryOrder);

            //this.tableRequests.Add(receiveIndex, new Chicken(Convert.ToInt32(chickenQ)));
            //this.tableRequests.Add(receiveIndex, new Egg(Convert.ToInt32(eggQ)));

            for (int i = 0; i < Convert.ToInt32(chickenQ); i++)
            {
                this.tableRequests.Add(receiveIndex, new Chicken(Convert.ToInt32(chickenQ)));
            }
            for (int i = 0; i < Convert.ToInt32(eggQ); i++)
            {
                this.tableRequests.Add(receiveIndex, new Egg(Convert.ToInt32(eggQ)));
            }

            receiveIndex++;
            return "Request received!";
        }

        public void Send(Cook cook)
        {
            cook.Process(tableRequests);
        }
        public string[] Serve()
        {
            var index = 0;
            foreach (MenuItemInterface[] row in tableRequests.customerOrders)
            {
                if (row == null) continue;
                var chQ = 0;
                var eQ = 0;
                for (int i = 0; i < row.Length; i++)
                {
                    if (row[i] is Chicken)
                        chQ++;
                    if (row[i] is Egg)
                        eQ++;
                }
                ServeOrders[index] = "Customer " + index + " is served " + chQ + " chicken, " + eQ + " egg, " + ServeDrinkings[index];
                index++;
            }
            return ServeOrders;
        }
    }
}
