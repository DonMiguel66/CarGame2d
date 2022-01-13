using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CarGame2D
{
    public class CarController : BasicController
    {
        private readonly ResourcePath _carPath = new ResourcePath { PathResources = "Prefabs/Car" };
        private readonly CarView _carView;
        public CarController(IItemsRepository _defaultItemConfigs)
        {
            var defaultItems = _defaultItemConfigs.Items.Values.ToList();
            _carView = LoadView();
            foreach (var carPart in defaultItems)
            {
                //Debug.Log(carPart.partPrefab.name);
                _carView.SetCarPart(carPart.Info.CarPartType, carPart.Info.Prefab);
            }            
        }

        private CarView LoadView()
        {
            var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_carPath));
            
            AddGameObject(objectView);

            if (objectView != null)
                return objectView.GetComponent<CarView>();
            else
                return null;
        }

        public GameObject GetViewObject()
        {
            return _carView.gameObject;
        }

        public CarView GetCarView()
        {
            return _carView;
        }
    }
}
