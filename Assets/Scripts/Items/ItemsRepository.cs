using System.Collections.Generic;

namespace CarGame2D
{
    public class ItemsRepository : BasicController, IRepository<int, IItem>
    {
        public IReadOnlyDictionary<int, IItem> Collection => _itemsMapById;

        private readonly Dictionary<int, IItem> _itemsMapById = new Dictionary<int, IItem>();

        public ItemsRepository(List<ItemConfig> itemConfigs)
        {
            PopulateItems(itemConfigs);
        }

        protected override void OnDispose()
        {
           _itemsMapById.Clear();
        }

        private void PopulateItems(List<ItemConfig> configs)
        {
            foreach(var config in configs)
            {
                if (_itemsMapById.ContainsKey(config.Id))
                    continue;

                _itemsMapById.Add(config.Id, CreateItem(config));
            }
        }

        private IItem CreateItem(ItemConfig config)
        {
            var itemInfo = new ItemInfo { Title = config.Title };           
            return new Item(config.Id, itemInfo);
        }
    }
}
