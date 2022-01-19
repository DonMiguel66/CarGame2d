using System.Collections.Generic;
using UnityEngine;

namespace CarGame2D
{
    public class AbilityRepository : BasicController, IRepository<int, IAbility>
    {
        public IReadOnlyDictionary<int, IAbility> Collection => _abilityMapByID;

        private Dictionary<int, IAbility> _abilityMapByID = new Dictionary<int, IAbility>();

        public AbilityRepository(List<AbilityItemConfig> configs)
        {
            PopulateItems(configs);
        }

        private void PopulateItems(List<AbilityItemConfig> configs)
        {
            foreach (var config in configs)
            {
                if (_abilityMapByID.ContainsKey(config.Id))
                    continue;

                _abilityMapByID.Add(config.Id, CreateItem(config));
            }
        }
        private IAbility CreateItem(AbilityItemConfig config)
        {
            switch(config.AbilityType)
            {
                case AbilityType.Gun:
                    return new BombAbility(config);
                default:
                    Debug.LogError("Not ability type!");
                    return null;
            }    
        }

        protected override void OnDispose()
        {
            _abilityMapByID.Clear();
        }
    }
}
