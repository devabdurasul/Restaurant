using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public abstract class Drink : MenuItemInterface
    {
        public abstract void Obtain();

        public abstract void Serve();
    }
}
