namespace Restaurant
{
    public sealed class Tea : Drink
    {
        public override IMenuItem Obtain() => this;

        public override IMenuItem Serve() => this;
    }
}
