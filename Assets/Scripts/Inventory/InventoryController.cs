using System;
using System.Collections;
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
        private readonly IInventoryModel _inventoryModel;
        private readonly InventoryView _inventoryView;
        private readonly IItemsRepository _itemsRepository;
        private readonly UpgradeHandlerRepository _upgradeHandlerRepository;
        //private Action _hideAction;
        private readonly ResourcePath _viewPath = new ResourcePath { PathResources = "Prefabs/InventoryView" };

        public InventoryController(IInventoryModel inventoryModel, IItemsRepository itemsRepository, UpgradeHandlerRepository upgradeItemsRepository, ProfilePlayerModel playerModel, CarController carController,Transform inventoryUI)
        {
            _inventoryModel = inventoryModel;
            _itemsRepository = itemsRepository;
            _upgradeHandlerRepository = upgradeItemsRepository;
            _playerModel = playerModel;
            _inventoryView = Object.Instantiate(ResourceLoader.LoadObject<InventoryView>(_viewPath),
                                                inventoryUI,
                                                false);
            AddGameObject(_inventoryView.gameObject);
            _inventoryView.Selected += OnItemSelected;
            _inventoryView.Deselected += OnItemDeselected;
            _carView = carController.GetCarView();
        }
        public void SnowInventory()
        {
            //_hideAction = action;
            var equippedItems = _itemsRepository.Items.Values.ToList();
            _inventoryView.Display(equippedItems);
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
            _carView.SetCarPart(item.Info.CarPartType, item.Info.Prefab);
            Debug.Log($"{item.Info.Title} equipped.");
        }
        private void OnItemDeselected(object sender, IItem item)
        {
            _inventoryModel.UnequipItem(item);
            _carView.RemoveCarPart(item.Info.CarPartType);
            Debug.Log($"{item.Info.Title} unequipped.");
        }
        public IReadOnlyList<IItem> GetEquippedItems()
        {
            return _inventoryModel.GetEquippedItems();
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
