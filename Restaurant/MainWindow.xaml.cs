using System;
using System.Windows;

namespace Restaurant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public delegate int? Ready(TableRequests tableRequests);
    public delegate void Processed();

    public enum Statuses
    {
        NotRecieved,
        NotSent,
        NotServed
    }

    public partial class MainWindow : Window
    {
        public Server server;
        public Cook cook;
        public TableRequests tableRequests;
        public Statuses status;
        public static int clickIndex = 0;
        public int? quality = null;
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
                status = Statuses.NotSent;
            }
        }

        private bool canRecieve()
        {
            if (status == Statuses.NotServed)
            {
                SetResult("Please, serve the requests sent first!");
                return false;
            }
            if (customerName.Text == "")
            {
                SetResult("Please, fill the customer name field!");
                return false;
            }
            return true;
        }

        public void Send(object sender, RoutedEventArgs e)
        {
            if (status == Statuses.NotRecieved)
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
            status = Statuses.NotServed;
        }

        public void Serve(object sender, RoutedEventArgs e)
        {
            switch (status)
            {
                case Statuses.NotRecieved:
                    SetResult("Please receive the requests first!");
                    break;
                case Statuses.NotSent:
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
            customerName.Text = "";
            status = 0;
        }

        private void SetResult(string message) => results.Text += message + "\n";
    }
}
