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
            // TODO:
        }

        private void Discard()
        {
            // TODO:
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

        public override void Obtain()
        {
        }

        public override void Serve()
        {
        }

        public void Dispose()
        {
            Discard();
        }

    }
}
