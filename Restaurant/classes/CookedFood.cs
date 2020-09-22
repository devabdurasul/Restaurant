namespace Restaurant
{
    public abstract class CookedFood : IMenuItem
    {
        public CookedFood() { }

        //TODO: What about making this method abstract? 
        public abstract void PrepareFood();

        public abstract void Cook();

        //TODO: We discussed about implementing Obtain and Serve methods in this abstract class. Is it possible making them as virtual method and implementing?
        public virtual IMenuItem Obtain() => this;

        public virtual IMenuItem Serve() => this;
    }
}
