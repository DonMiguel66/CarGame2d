using UnityEngine;
using Object = UnityEngine.Object;

namespace CarGame2D
{
    public class CarController : BasicController
    {
        private readonly ResourcePath _carPath = new ResourcePath { PathResources = "Prefabs/Car" };
        private readonly CarView _carView;

        public CarController()
        {
            _carView = LoadView();
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
    }
}
