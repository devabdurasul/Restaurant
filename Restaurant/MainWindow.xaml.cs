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

    public enum Statuses
    {
        notRecieved,
        notSent,
        notServed
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
            //TODO: I tried to receive orders second time for the same person, it says "Please enter another name or try to enter your surname!". In this case it should increase orders od the person.
            if (!canRecieve()) return;//TODO: You should show a message about it, because user doesn't know why he can not receive.
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
                status = Statuses.notSent;
            }
        }

        private bool canRecieve()
        {
            if (status == Statuses.notServed)
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
            if (status == Statuses.notRecieved)
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
            status = Statuses.notServed;
        }

        public void Serve(object sender, RoutedEventArgs e)
        {
            //TODO: It would be better you create enum for the statuses
            switch (status)
            {
                case Statuses.notRecieved:
                    SetResult("Please receive the requests first!");
                    break;
                case Statuses.notSent:
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
