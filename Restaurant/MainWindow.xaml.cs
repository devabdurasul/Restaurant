using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Restaurant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public enum Statuses
    {
        NotRecieved,
        NotSent
    }

    public partial class MainWindow : Window
    {
        public Server server;
        public Cook FirstCook;
        public Cook SecondCook;
        public TableRequests tableRequests;
        public object locker = new object();
        public Statuses status;
        public MainWindow()
        {
            Initialize();
            InitializeComponent();
        }

        public void Receive(object sender, RoutedEventArgs e)
        {
            if (!canRecieve()) return;
            var result = "";
            lock (locker)
            {
                try
                {
                    result = server.Receive(chickenQ.Text, eggQ.Text, customerName.Text, drinkingType.Text, tableRequests);
                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    SetResult(ex.Message);
                }
                finally
                {
                    if (result.Length > 0)
                        SetResult(result);
                    status = Statuses.NotSent;
                }
            }
        }

        private bool canRecieve()
        {
            if (customerName.Text == "")
            {
                SetResult("Please, fill the customer name field!");
                return false;
            }
            return true;
        }

        public void RunTask(Cook cook)
        {
            Task prepare = new Task( () => cook.Process(tableRequests));
            Task<List<string>> prepareServe = prepare.ContinueWith(server.Serve);
            prepare.Start();
            prepareServe.Wait();
            GetQuality(cook);
            Serve(prepareServe.Result);
        }

        public void Send(object sender, RoutedEventArgs e)
        {
            if (status == Statuses.NotRecieved)
                SetResult("Please receive the requests first!");
            else
                Send();
        }

        private void Send()
        {
            try
            {
                while (true)
                {
                    if (FirstCook.State == CookState.Free)
                    {
                        RunTask(FirstCook);
                        return;
                    }
                    else if (SecondCook.State == CookState.Free)
                    {
                        RunTask(SecondCook);
                        return;
                    }
                    else
                    {
                        Thread.Sleep(2000);
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                SetResult(ex.Message);
            }
        }

        public void Serve(List<string> orders)
        {
            if (!CanServe(orders)) return;

            var drinks = server.ServeDrinkings.OrderBy(d => d);
            foreach (var drink in drinks)
                SetResult(drink);

            foreach (var order in orders)
                SetResult(order);
            Reset();
        }

        private bool CanServe(List<string> orders)
        {
            if (orders.Count() == 0 && server.ServeDrinkings.Count() == 0)
            {
                SetResult("Nothing to serve!");
                return false;
            }
            SetResult("Requests are sent!");
            return true;
        }

        private void Reset()
        {
            SetResult("Enjoy your food!");
            customerName.Text = "";
            tableRequests = new TableRequests();
            status = Statuses.NotRecieved;
        }

        private void GetQuality(Cook cook)
        {
            qualityLabel.Content = cook.quality;
        }

        private void Initialize() 
        {
            server = new Server();
            FirstCook = new Cook();
            SecondCook = new Cook();
            tableRequests = new TableRequests();
        }

        private void SetResult(string message) => results.Text += message + "\n";
    }
}
