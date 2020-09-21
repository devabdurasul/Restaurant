using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    //TODO: Using IMenuItem is better name for the interface instead of MenuItemInterface
    public interface MenuItemInterface
    {
        //TODO: Obtain and Serve method should return itself (this)
        void Obtain();
        void Serve();
    }
}
