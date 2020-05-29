using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.BaseComponents
{
    internal class MiniCart
    {
        readonly DomElement _container;

        public MiniCart(DomElement minicartContainer)
        {
            _container = minicartContainer;
        }
    }
}
