using System;
using System.Collections.Generic;

namespace CarGame2D
{
    public interface IInventoryView
    {
        event EventHandler<IItem> Selected;
        event EventHandler<IItem> Deselected;
        void Display(List<IItem> items);
    }
}
