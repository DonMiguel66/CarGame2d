using UnityEngine;

namespace CarGame2D
{
    [CreateAssetMenu(fileName = "ItemConfig", menuName = "ItemConfig")]
    public class ItemConfig : ScriptableObject
    {
        [SerializeField]
        private int _id;

        [SerializeField]
        private CarPartType _carPartType;

        [SerializeField]
        private string _title;

        [SerializeField]
        private GameObject _prefab;

        public int Id => _id;
        public string Title  => _title;
        public CarPartType CarPartType => _carPartType;
        public GameObject Prefab => _prefab;
    }
}
