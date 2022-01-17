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

        public CarView GetCarView()
        {
            return _carView;
        }

        public void SetCarPart(CarView carView, CarPartType partType, GameObject partGO, bool toSet)
        {
            switch (partType)
            {
                case CarPartType.Body:
                    Object.Destroy(carView.Body);
                    if (toSet)
                        carView.Body = InitCarPart(carView.BodyPosition, partGO);
                    break;
                case CarPartType.Wheel:
                    Object.Destroy(carView.ForwardWheel);
                    Object.Destroy(carView.BackWheel);
                    if (toSet)
                    {
                        carView.BackWheel = InitCarPart(carView.BackWheelPosition, partGO);
                        carView.ForwardWheel = InitCarPart(carView.ForwardWheelPosition, partGO);
                    }
                    break;
                case CarPartType.Accelerator:
                    Object.Destroy(carView.Accelerator);
                    if (toSet)
                        carView.Accelerator = InitCarPart(carView.AcceleratorPosition, partGO);
                    break;
            }

        }

        private GameObject InitCarPart(Transform partPosition, GameObject partPrefab)
        {
            var carView = Object.Instantiate(partPrefab, partPosition, true);
            carView.transform.position = partPosition.position;
            return carView;

        }
    }
}
