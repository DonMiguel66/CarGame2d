using System.Collections.Generic;

namespace CarGame2D
{
    public class InventoryModel : IInventoryModel
    {
        private readonly List<IItem> _items = new List<IItem>();
        public IReadOnlyList<IItem> GetEquippedItems()
        {
            return _items;
        }

        //Изменить проверку состояния, если будут категории предметов
        public void EquipItem(IItem item)
        {
            if (_items.Contains(item))
                return;           
            _items.Add(item);            
        }

        
        public void UnequipItem(IItem item)
        {
            if (!_items.Contains(item))
                return;
            _items.Remove(item);
        }
    }
}
