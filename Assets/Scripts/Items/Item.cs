using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarGame2D
{
    public class Item : IItem
    {        
        public int Id { get; }
        public ItemInfo Info { get; }
        public Item(int id, ItemInfo info)
        {
            Id = id;
            Info = info;
        }

    }
}
