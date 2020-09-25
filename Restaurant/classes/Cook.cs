namespace Restaurant
{
    public class Cook
    {
        private int? quality = null;

        private event ProcessedDelegate _processed;
        public event ProcessedDelegate Processed
        {
            add => _processed += value;
            remove => _processed -= value;
        }

        public int? Process(TableRequests tableRequests)
        {
            //TODO: some code duplicated here. Can you use CookedFood type instead of both types Chicken and Egg?
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
            _processed?.Invoke();
            return quality;
        }
    }
}
