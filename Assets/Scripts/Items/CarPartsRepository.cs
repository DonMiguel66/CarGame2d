using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGame2D
{
    public class CarPartsRepository : BasicController, IRepository<int, IItem>
    {        
        public IReadOnlyDictionary<int, IItem> Collection => _itemsMapById;

        private readonly Dictionary<int, IItem> _itemsMapById = new Dictionary<int, IItem>();

        public CarPartsRepository(List<CarPartConfig> itemConfigs)
        {
            PopulateItems(itemConfigs);
        }

        protected override void OnDispose()
        {
            _itemsMapById.Clear();
        }

        private void PopulateItems(List<CarPartConfig> configs)
        {
            foreach (var config in configs)
            {
                if (_itemsMapById.ContainsKey(config.Id))
                    continue;

                _itemsMapById.Add(config.Id, CreateItem(config));
            }
        }

        private IItem CreateItem(CarPartConfig config)
        {
            var itemInfo = new ItemInfo { Title = config.Title, CarPartType = config.CarPartType, Prefab = config.Prefab };
            return new Item(config.Id, itemInfo);
        }
    }
}
