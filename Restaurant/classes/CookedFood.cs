namespace Restaurant
{
    public abstract class CookedFood : IMenuItem
    {
        public CookedFood() { }

        public virtual void PrepareFood()
        {
            // TODO:
        }

        public abstract void Cook();

        public abstract IMenuItem Obtain();

        public abstract IMenuItem Serve();
    }
}
