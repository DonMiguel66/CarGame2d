using UnityEngine;
using Object = UnityEngine.Object;

namespace CarGame2D
{
    public class InGameUIController : BasicController
    {
        private InGameUIView _view;
        private InventoryController _inventoryController;
        private AbilityController _abilityController;
        private Transform _placeForUi;
        private readonly ResourcePath _viewPath = new ResourcePath { PathResources = "Prefabs/InGameUI" };

        public InGameUIController(Transform placeForUI, InventoryController inventoryController, AbilityController abilityController)
        {
            _placeForUi = placeForUI;
            _view = LoadView(_placeForUi);
            _inventoryController = inventoryController;
            _abilityController = abilityController;
            _view.Init(Inventory, Ability);
        }

        private InGameUIView LoadView(Transform placeForUI)
        {
            var objectView = Object.Instantiate(ResourceLoader.LoadObject<InGameUIView>(_viewPath), _placeForUi,false);
            AddGameObject(objectView.gameObject);
            return objectView;
        }

        private void Inventory()
        {
            if (!_inventoryController.InventoryStatus())
                _inventoryController.SnowInventory();
            else
                _inventoryController.HideInventory();
        }

        private void Ability()
        {
            if (!_abilityController.AbilityStatus())
                _abilityController.SnowInventory();
            else
                _abilityController.HideInventory();
        }
    }
}
