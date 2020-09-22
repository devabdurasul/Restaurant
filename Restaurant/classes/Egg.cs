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

        //TODO: Can you use "Using statement", so it will automatically call Dispose() method?
        //https://www.c-sharpcorner.com/article/the-using-statement-in-C-Sharp/#:~:text=The%20C%23%20using%20statement%20defines,example%20%2D%20an%20exception%20is%20thrown.
        public void Dispose() => Discard();
    }
}
