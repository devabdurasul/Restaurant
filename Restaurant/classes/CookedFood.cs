using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public abstract class CookedFood : MenuItemInterface
    {
        public CookedFood() { }

        public virtual void PrepareFood()
        {
            // TODO:
        }

        public abstract void Cook();

        public abstract void Obtain();

        public abstract void Serve();
    }
}
