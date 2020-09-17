using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // TODO:
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

        public override void PrepareFood()
        {
            Obtain();
            CutUp();
            Cook();
        }
    }
}
