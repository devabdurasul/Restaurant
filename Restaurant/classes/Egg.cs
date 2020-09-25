using System;

namespace Restaurant
{
    public sealed class Egg : CookedFood, IDisposable
    {
        public int GetQuality() => new Random().Next(101);

        public void Crack() { }

        private void Discard() { }

        public override void PrepareFood()
        {
            Obtain();
            Crack();
            Cook();
        }

        public override void Cook() { }

        public void Dispose() => Discard();
    }
}
