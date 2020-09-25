using System;

namespace Restaurant
{
    public sealed class Egg : CookedFood, IDisposable
    {
        //TODO: This quantity property and constructors are not used, please use them or just delete them.
        private int quantity;
        private int quality;
        Random rand = new Random();
        public Egg() { }

        public Egg(int quantity)
        {
            this.quantity = quantity;
        }

        public int GetQuality()
        {
            return quality = rand.Next(101);
        }

        public void Crack()
        {
        }

        private void Discard()
        {
        }

        public override void PrepareFood()
        {
            Obtain();
            Crack();
            Cook();
        }

        public override void Cook()
        {
        }

        public void Dispose() => Discard();
    }
}
