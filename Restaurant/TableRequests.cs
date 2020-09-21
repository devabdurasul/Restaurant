using System;

namespace Restaurant
{
    public class TableRequests
    {
        public MenuItemInterface[][] customerOrders = new MenuItemInterface[8][];
        public static int num = 0;
        public int customer = 0;

        private MenuItemInterface[] array;
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
            customerOrders[index] = new MenuItemInterface[summaryOrder];
            this.customer = index;
        }

        public void Add(int customer, MenuItemInterface menuItem) => customerOrders[customer][num++] = menuItem;

        public void add(MenuItemInterface data)
        {
            if (count == size)
                growSize();
            array[count] = data;
            count++;
        }

        //TODO: You don't need this method to copy arrays. You can just use Array.Resize(...)
        public void growSize()
        {
            MenuItemInterface[] temp = null;
            if (count == size)
            {
                temp = new MenuItemInterface[size * 2];
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
            array = new MenuItemInterface[1];
            count = 0;
            size = 1;
        }

        public void ClearCustomerOrders() => customerOrders = new MenuItemInterface[8][];

        public MenuItemInterface[] this[MenuItemInterface menuItem]
        {
            get
            {
                //TODO: Why 2 separate 'if' condition for 'chicken' and 'egg'? You should consider all type of menus: chicken, egg, and all type of drinks.
                if (menuItem is Chicken)
                {
                    ArrayInit();
                    for (int i = 0; i < customerOrders.Length; i++)
                    {
                        if (customerOrders[i] == null) continue;
                        for (int j = 0; j < customerOrders[i].Length; j++)
                        {
                            if (customerOrders[i][j] is Chicken)
                                add(customerOrders[i][j]);
                        }
                    }
                    return array;
                }

                if (menuItem is Egg)
                {
                    ArrayInit();
                    for (int i = 0; i < customerOrders.Length; i++)
                    {
                        if (customerOrders[i] == null) continue;
                        for (int j = 0; j < customerOrders[i].Length; j++)
                        {
                            if (customerOrders[i][j] is Egg)
                                add(customerOrders[i][j]);
                        }
                    }
                    return array;
                }
                return array;
            }
        }


        //TODO: Handle cases when customer is not between 1-8.
        public MenuItemInterface[] this[int customer]
        {
            get
            {
                return customerOrders[customer];
            }
        }
    }
}
