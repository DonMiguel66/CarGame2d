using UnityEngine;

namespace CarGame2D
{
    [CreateAssetMenu(fileName = "UpgradeItemConfig", menuName = "UpgradeItemConfig")]
    public class UpgradeItemConfig : ScriptableObject
    {
        [SerializeField]
        private ItemConfig itemConfig;

        [SerializeField]
        private UpgradeType _upgradeType;

        [SerializeField]
        private float _valueUpgrade;

        public int Id  => itemConfig.Id; 
        public UpgradeType UpgradeType => _upgradeType;
        public float ValueUpgrade => _valueUpgrade;
    }

    public enum UpgradeType
    {
        None,
        Speed,
        Control
    }
}
