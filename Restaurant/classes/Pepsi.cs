namespace Restaurant
{
    public sealed class Pepsi: Drink
    {
        public override IMenuItem Obtain() => this;

        public override IMenuItem Serve() => this;
    }
}
