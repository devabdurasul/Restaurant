namespace Restaurant
{
    public sealed class Chicken : CookedFood
    {
        //TODO: This quantity property and constructors are not used, please use them or just delete them.
        public void CutUp(){}

        public override void Cook(){}

        public override void PrepareFood()
        {
            Obtain();
            CutUp();
            Cook();
        }
    }
}
