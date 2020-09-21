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

        //TODO: Please use this method somewhere to show eggs quality
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

        //TODO: This method is not used. Please use this.
        public void Dispose()
        {
            Discard();
        }

    }
}
