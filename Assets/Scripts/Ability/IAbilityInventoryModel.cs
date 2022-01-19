using System.Collections.Generic;

namespace CarGame2D
{
    public interface IAbilityInventoryModel
    {
        IReadOnlyList<IItem> GetEquippedItems();

        void EquipItem(IItem item);

        void UnequipItem(IItem item);
    }
}
