namespace Restaurant
{
    public sealed class Cola : Drink
    {
        public override IMenuItem Obtain() => this;
      
        public override IMenuItem Serve() => this;
    }
}
