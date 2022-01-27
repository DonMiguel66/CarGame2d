using UnityEngine;

namespace CarGame2D
{
    [CreateAssetMenu(fileName = "AbilityItemConfig", menuName = "AbilityItemConfig")]
    public class AbilityItemConfig : ScriptableObject
    {
        [SerializeField]
        private ItemConfig _itemConfig;

        [SerializeField]
        private AbilityView _view;

        [SerializeField]
        private AbilityType _abilityType;

        [SerializeField]
        private float _value;

        public int Id => _itemConfig.Id;
        public string Title => _itemConfig.Title;
        public AbilityView View => _view;
        public AbilityType AbilityType => _abilityType;
        public float Value => _value;
    }
    public enum AbilityType
    {
        None,
        Gun,
        Shield
    }
}
