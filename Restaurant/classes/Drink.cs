namespace Restaurant
{
    public abstract class Drink : IMenuItem
    {
        public virtual IMenuItem Obtain() => this;

        public virtual IMenuItem Serve() => this;
    }
}
