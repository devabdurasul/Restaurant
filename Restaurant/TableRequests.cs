using System;

namespace Restaurant
{
    public class TableRequests
    {
        public IMenuItem[][] customerOrders = new IMenuItem[8][]; //TODO: It should be private.
        public int num = 0;

        public void Add(int customer, IMenuItem menuItem) => customerOrders[customer][num++] = menuItem;

        public void ClearCustomerOrders() => customerOrders = new IMenuItem[8][];

        public IMenuItem[] this[IMenuItem menuItem]
        {
            get
            {                
                IMenuItem[]  array = new IMenuItem[1];
                int count = 0;

                for (int i = 0; i < customerOrders.Length; i++)
                {
                    if (customerOrders[i] == null) continue;
                    for (int j = 0; j < customerOrders[i].Length; j++)
                    {
                        if (customerOrders[i][j].GetType() == menuItem.GetType())
                        {
                            Array.Resize(ref array, array.Length + 1);
                            array[count] = customerOrders[i][j];
                            count++;
                        }

                    }
                }
                return array;
            }
        }


        //TODO: This indexer should be used when serving order to customers by server to get each customer orders
        public object this[int customer]
        {
            get
            {
                if (customer >= 0 && customer < 9)
                    return customerOrders[customer];
                else return "Wrong customer!";
            }
        }
    }
}
