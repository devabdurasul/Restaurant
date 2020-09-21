namespace Restaurant
{
    public abstract class Drink : IMenuItem
    {
        public abstract IMenuItem Obtain();

        public abstract IMenuItem Serve();
    }
}
