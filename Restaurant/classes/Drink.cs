namespace Restaurant
{
    public abstract class Drink : IMenuItem
    {
        //TODO: Can we implement these 2 methods? Because implementation for all child classes are the same.
        public virtual IMenuItem Obtain() => this;

        public virtual IMenuItem Serve() => this;
    }
}
