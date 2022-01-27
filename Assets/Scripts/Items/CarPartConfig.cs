using UnityEngine;

namespace CarGame2D
{
    [CreateAssetMenu(fileName = "CarPartItemConfig", menuName = "CarPartItemConfig")]
    public class CarPartConfig : ScriptableObject
    {
        [SerializeField]
        private ItemConfig _itemConfig;

        [SerializeField]
        private CarPartType _carPartType;

        //TO DO Сделать вместо префаба Вьюшку, содержащую необходимые поля в себе
        [SerializeField]
        private GameObject  _prefab;

        public int Id => _itemConfig.Id;
        public string Title => _itemConfig.Title;
        public GameObject Prefab => _prefab;
        public CarPartType CarPartType => _carPartType;
    }
}
