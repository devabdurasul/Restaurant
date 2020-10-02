using System.Threading;
using System.Threading.Tasks;

namespace Restaurant
{
    public enum CookState
    {
        Free,
        Busy
    }
    public class Cook
    {
        //TODO: It's better to have method to get quality. If you make egg's GetQuality() method as static then the OnProcess method will be more smaller. 
        public int? quality = null;
        public CookState State;
        Semaphore semaphore = new Semaphore(2, 2);

        public Task Process(TableRequests tableRequests) => Task.Run(() => OnRunProcess(tableRequests));

        private void OnRunProcess(TableRequests tableRequests)
        {
            semaphore.WaitOne();
            State = CookState.Busy;
            OnProcess(tableRequests);
            Thread.Sleep(14000);
            State = CookState.Free;
            semaphore.Release();
        }

        private void OnProcess(TableRequests tableRequests)
        {
            //TODO: Can you refactor this foreach to use Parallel?
            var chickenOrders = tableRequests.Get<Chicken>();
            foreach (var item in chickenOrders)
                (item as Chicken).PrepareFood();

            var eggOrders = tableRequests.Get<Egg>();
            foreach (var item in eggOrders)
            {
                var egg = item as Egg;
                using (egg)
                {
                    quality = egg.GetQuality();
                    egg.PrepareFood();
                }
            }
        }
    }
}
