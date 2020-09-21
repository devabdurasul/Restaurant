using System;

namespace Restaurant
{
    public class TableRequests
    {
        public IMenuItem[][] customerOrders = new IMenuItem[8][];
        public int num = 0;
        public int customer = 0;

        private IMenuItem[] array;
        private int count;
        private int size;
        public TableRequests()
        {
            ArrayInit();
        }

        //TODO: You don't need this method
        public void InitOrder(int index, int summaryOrder)
        {
            num = 0;
            customerOrders[index] = new IMenuItem[summaryOrder];
            this.customer = index;
        }

        public void Add(int customer, IMenuItem menuItem) => customerOrders[customer][num++] = menuItem;

        public void add(IMenuItem data)
        {
            Array.Resize(ref array, array.Length + 1);
            array[count] = data;
            count++;
        }

        //TODO: You don't need this method to copy arrays. You can just use Array.Resize(...)
        public void growSize()
        {
            IMenuItem[] temp = null;
            if (count == size)
            {
                temp = new IMenuItem[size * 2];
                {
                    for (int i = 0; i < size; i++)
                        temp[i] = array[i];
                }
            }
            array = temp;
            size = size * 2;
        }

        private void ArrayInit()
        {
            array = new IMenuItem[1];
            count = 0;
        }

        public void ClearCustomerOrders() => customerOrders = new IMenuItem[8][];

        public IMenuItem[] this[IMenuItem menuItem]
        {
            get
            {
                //TODO: Why 2 separate 'if' condition for 'chicken' and 'egg'? You should consider all type of menus: chicken, egg, and all type of drinks.
              
                    ArrayInit();
                    for (int i = 0; i < customerOrders.Length; i++)
                    {
                        if (customerOrders[i] == null) continue;
                        for (int j = 0; j < customerOrders[i].Length; j++)
                        {
                            if (customerOrders[i][j].GetType() == menuItem.GetType())
                                add(customerOrders[i][j]);
                        }
                    }
                    return array;
            }
        }


        //TODO: Handle cases when customer is not between 1-8.
        public object this[int customer]
        {
            get
            {
                if (customer > 0 && customer < 9)
                    return customerOrders[customer];
                else return "Wrong customer!";
            }
        }
    }
}
