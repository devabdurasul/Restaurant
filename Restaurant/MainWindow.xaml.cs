using System;
using System.Windows;

namespace Restaurant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public delegate int? ReadyDelegate(TableRequests tableRequests);
    public delegate void ProcessedDelegate();

    public partial class MainWindow : Window
    {
        public Server server;
        public Cook cook;
        public TableRequests tableRequests;
        public int status = 0;
        public static int clickIndex = 0;
        public int? quality = null;
        public string customerPrevName;
        public MainWindow()
        {
            Initialize();
            InitializeComponent();
        }

        public void Receive(object sender, RoutedEventArgs e)
        {
            if (!canRecieve()) return;
            var result = "";
            try
            {
                result = server.Receive(chickenQ.Text, eggQ.Text, customerName.Text, drinkingType.Text, tableRequests);
            }
            catch (Exception ex)
            {
                SetResult(ex.Message);
            }
            finally
            {
                if (result.Length > 0)
                    SetResult(result);
                clickIndex = 0;
                status = 1;
                customerPrevName = customerName.Text;
            }
        }

        private bool canRecieve()
        {
            if (status > 1) return false;
            if (customerName.Text == "")
            {
                SetResult("Please, fill the customer name field!");
                return false;
            }
            if (customerName.Text == customerPrevName)
            {
                SetResult("Request from " + customerPrevName.ToUpper() + " have already been received, Please enter another name or try to enter your surname!");
                return false;
            }
            return true;
        }

        public void Send(object sender, RoutedEventArgs e)
        {
            if (status == 0)
                SetResult("Please receive the requests first!");
            else if (clickIndex == 1)
                SetResult("Already sent");
            else
                Send();
        }

        private void Send()
        {
            clickIndex++;
            try
            {
                quality = server.Send();
            }
            catch (Exception ex)
            {
                SetResult(ex.Message);
            }
            qualityLabel.Content = quality;
            SetResult("Requests are sent!");
            status = 2;
        }

        public void Serve(object sender, RoutedEventArgs e)
        {
            switch (status)
            {
                case 0:
                    SetResult("Please receive the requests first!");
                    break;
                case 1:
                    SetResult("Please send the requests first!");
                    break;
                default:
                    Serve();
                    break;
            }
        }

        private void Initialize() 
        {
            server = new Server();
            cook = new Cook();
            tableRequests = new TableRequests();
            server.Ready += (TableRequests tableRequests) => cook.Process(tableRequests);
            cook.Processed += server.Serve;
        }

        private void ClearCustomerName()
        {
            customerName.Text = "";
            customerPrevName = "";
        }

        private void Serve()
        {
            foreach (var drink in server.ServeDrinkings)
            {
                SetResult(drink);
            }
            foreach (var order in server.ServeOrders)
            {
                SetResult(order);
            }
            SetResult("Enjoy your food!");
            Initialize();
            ClearCustomerName();
            status = 0;
        }

        private void SetResult(string message) => results.Text += message + "\n";
    }
}
