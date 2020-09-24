using System;

namespace Restaurant
{
    public sealed class Egg : CookedFood, IDisposable
    {
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
