using UnityEngine;

namespace CarGame2D
{
    [CreateAssetMenu(fileName = "UpgradeItemConfigDataSource", menuName = "UpgradeItemConfigDataSource")]
    public class UpgradeItemConfigDataSource : ScriptableObject
    {
        [SerializeField]
        private UpgradeItemConfig[] _itemConfigs;

        public UpgradeItemConfig[] ItemConfigs => _itemConfigs;
    }
}
