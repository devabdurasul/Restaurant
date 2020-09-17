using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Restaurant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public Server server;
        public Cook cook;
        public TableRequests tableRequests;
        public int status = 0;
        public static int clickIndex = 0;
        public int customer = 0;
        public MainWindow()
        {
            InitializeComponent();
            server = new Server();
            cook = new Cook();
            tableRequests = new TableRequests();
        }

        public void Receive(object sender, RoutedEventArgs e)
        {
            if (status > 1) return;
            var result = "";
            try
            {
                result = server.Receive(chickenQ.Text, eggQ.Text, drinkingType.Text, tableRequests);
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
                customer++;
                status = 1;
            }
        }

        public void Send(object sender, RoutedEventArgs e)
        {
            if (status == 0)
                SetResult("Please receive the requests first!");
            else if (clickIndex == 1)
                SetResult("Already sent");
            else
            {
                Send();
            }
        }

        private void Send()
        {
                clickIndex++;
                try
                {
                    server.Send(cook);
                }
                catch (Exception ex)
                {
                    SetResult(ex.Message);
                }
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

        private void Serve()
        {
            server.receiveIndex = 0;
            var statuses = server.Serve();
            foreach (string status in statuses)
            {
                if (status == null) continue;
                SetResult(status);
            }
            SetResult("Enjoy your food!");
            tableRequests.ClearCustomerOrders();
            server.ServeOrders = new string[8];
            status = 0;
        }

        private void SetResult(string message) => results.Text += message + "\n";
    }
}
