namespace Restaurant
{
    public abstract class CookedFood : IMenuItem
    {
        public CookedFood() { }

        public abstract void PrepareFood();

        public abstract void Cook();

        public virtual IMenuItem Obtain() => this;

        public virtual IMenuItem Serve() => this;
    }
}
