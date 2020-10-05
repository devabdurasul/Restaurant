namespace Restaurant
{
    public sealed class Chicken : CookedFood
    {
        public void CutUp() { }

        public override void Cook() { }

        public override void PrepareFood()
        {
            Obtain();
            CutUp();
            Cook();
        }
    }
}
