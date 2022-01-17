using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CarGame2D
{
    public class InventoryController : BasicController, IInventoryController
    {
        private readonly ProfilePlayerModel _playerModel;
        private readonly CarView _carView;
        private readonly CarController _carController;
        private readonly IInventoryModel _inventoryModel;
        private readonly InventoryView _inventoryView;
        private readonly IItemsRepository _itemsRepository;
        private readonly IItemsRepository _defaultEquippedItemRepository;
        private readonly UpgradeHandlerRepository _upgradeHandlerRepository;
        //private Action _hideAction;
        private readonly ResourcePath _viewPath = new ResourcePath { PathResources = "Prefabs/InventoryView" };

        public InventoryController(IInventoryModel inventoryModel, IItemsRepository defaultEquippedItemRepository, IItemsRepository itemsRepository, UpgradeHandlerRepository upgradeItemsRepository, ProfilePlayerModel playerModel, CarController carController,Transform inventoryUI)
        {
            _inventoryModel = inventoryModel;
            _itemsRepository = itemsRepository;
            _defaultEquippedItemRepository = defaultEquippedItemRepository;
            _upgradeHandlerRepository = upgradeItemsRepository;
            _playerModel = playerModel;
            _carController = carController;
            _carView = carController.GetCarView();
            EquipDefaultItems();

            _inventoryView = Object.Instantiate(ResourceLoader.LoadObject<InventoryView>(_viewPath),
                                                inventoryUI,
                                                false);
            AddGameObject(_inventoryView.gameObject);

            _inventoryView.Selected += OnItemSelected;
            _inventoryView.Deselected += OnItemDeselected;
        }
        public void SnowInventory()
        {
            var equippedItems = _itemsRepository.Items.Values.ToList();
            _inventoryView.Display(equippedItems);
            ShowItems();
        }

        //public void SnowInventory(Action action)
        //{
        //    //_hideAction = action;

        //    var equippedItems = _itemsRepository.Items.Values.ToList();
        //    _inventoryView.Display(equippedItems);
        //}
        public void HideInventory()
        {
            //_hideAction?.Invoke();
        }        
        private void OnItemSelected(object sender, IItem item)
        {            
            _inventoryModel.EquipItem(item);
            _carController.SetCarPart(_carView, item.Info.CarPartType, item.Info.Prefab, true);
            Debug.Log($"{item.Info.Title} equipped.");
            ShowItems(); //дл€ дебага
        }
        private void OnItemDeselected(object sender, IItem item)
        {
            _inventoryModel.UnequipItem(item);
            _carController.SetCarPart(_carView, item.Info.CarPartType, item.Info.Prefab, false);
            Debug.Log($"{item.Info.Title} unequipped.");
            ShowItems(); //дл€ дебага
        }
        public void EquipDefaultItems()
        {
            var defaultItems = _defaultEquippedItemRepository.Items.Values.ToList();
            foreach (var carPart in defaultItems)
            {
                _inventoryModel.EquipItem(carPart);
                _carController.SetCarPart(_carView, carPart.Info.CarPartType, carPart.Info.Prefab, true);
            }
        }

        public IReadOnlyList<IItem> GetEquippedItems()
        {
            return _inventoryModel.GetEquippedItems();
        }        
        //ћетод дл€ дебага, потом удалю
        private void ShowItems()
        {
            foreach (var item in GetEquippedItems())
            {
                Debug.Log(item.Id);
                Debug.Log(item.Info.Title);
                Debug.Log(item.Info.CarPartType);
            }
        }

        private void UpgradeCarWithEquippedItems(IUpgradableCar upgradableCar, IReadOnlyList<IItem> equippedItems, IReadOnlyDictionary<int, IUpgradeCarHandler> upgradeHandlers)
        {
            foreach (var equippedItem in equippedItems)
            {
                if (upgradeHandlers.TryGetValue(equippedItem.Id, out var handler))
                {
                    handler.Upgrade(upgradableCar);
                }
            }
        }

        protected override void OnDispose()
        {
            _inventoryView.Selected -= OnItemSelected;
            _inventoryView.Deselected -= OnItemDeselected;

            base.OnDispose();
        }
    }
}
