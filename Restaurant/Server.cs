﻿using System;
using System.Collections.Generic;

namespace Restaurant
{
    public class Server
    {
        public List<string> ServeDrinkings = new List<string>();
        public List<string> ServeOrders = new List<string>();
        //TODO: If these fields are used only by one method then you can move the fields inside that method.
        private int receiveIndex = 0; //TODO: Why this is public?
        private TableRequests tableRequests;
        

        private event ReadyDelegate _ready;
        public event ReadyDelegate Ready
        {
            add => _ready += value;
            remove => _ready -= value;
        }


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
            ServeDrinkings.Add("Customer " + customerName.ToUpper() + " is served " + drinking.GetType().Name);

            for (int i = 0; i < chQ; i++)
                tableRequests.Add<Chicken>(customerName);
            for (int i = 0; i < eQ; i++)
                tableRequests.Add<Egg>(customerName);
            receiveIndex++;
            return "Request received from: " + customerName.ToUpper();
        }

        public int? Send() => _ready?.Invoke(tableRequests);
        
        public void Serve()
        {
            foreach (var request in tableRequests)
            {
                var orders = tableRequests[request.Key];
                var chQ = 0;
                var eQ = 0;
                foreach (var item in orders)
                {
                    if (item == null) continue;
                    if (item is Chicken)
                        chQ++;
                    if (item is Egg)
                        eQ++;
                }
                ServeOrders.Add("Customer " + request.Key.ToString().ToUpper() + " is served " + chQ + " chicken, " + eQ + " egg");
            }
        }
    }
}
