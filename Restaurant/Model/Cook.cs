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
            var chickenOrders = tableRequests.Get<Chicken>();
            Parallel.ForEach(chickenOrders, chicken =>
            {
                ((Chicken)chicken).PrepareFood();
            });

            var eggOrders = tableRequests.Get<Egg>();
            Parallel.ForEach(eggOrders, egg =>
            {
                using (egg as Egg)
                    (egg as Egg).PrepareFood();
            });
        }
    }
}
