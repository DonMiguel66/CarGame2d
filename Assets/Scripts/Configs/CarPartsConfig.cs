using System;
using System.Collections.Generic;
using UnityEngine;

namespace CarGame2D
{    
    [CreateAssetMenu(fileName = "CarPartsCfg", menuName = "CarConfigs")]
    public class CarPartsConfig : ScriptableObject
    {
        [Serializable]
        public sealed class CarPart
        {
            [SerializeField]
            private CarPartType _carPartType;
            public CarPartType CarPartType => _carPartType;
            public int ID;
            public GameObject partPrefab;
        }

        public List<CarPart> partsList = new List<CarPart>();
    }
}
