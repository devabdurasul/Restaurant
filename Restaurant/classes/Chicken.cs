namespace Restaurant
{
    public sealed class Chicken : CookedFood
    {
        private int quantity;
        public Chicken() { }

        public Chicken(int quantity)
        {
            this.quantity = quantity;
        }

        public int Quantity {
            get => quantity;
        }


        public void CutUp()
        {
        }

        public override void Cook()
        {
        }

        public override void PrepareFood()
        {
            Obtain();
            CutUp();
            Cook();
        }
    }
}
