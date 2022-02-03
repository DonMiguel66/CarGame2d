using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CarGame2D
{
    public class InventoryController : BasicController, IInventoryController
    {
        private readonly CarView _carView;
        private readonly CarController _carController;
        private readonly IInventoryModel _inventoryModel;
        private readonly InventoryView _inventoryView;
        private readonly CarPartsRepository _inventoryCarItemsRepository;
        private readonly CarPartsRepository _defaultCarItemsRepository;
        private readonly UpgradeHandlerRepository _upgradeHandlerRepository;

        private readonly ResourcePath _viewPath = new ResourcePath { PathResources = "Prefabs/InventoryView" };


        public InventoryController(IInventoryModel inventoryModel, CarPartsRepository defaultCarItemsRepository, CarPartsRepository inventoryCarItemsRepository, UpgradeHandlerRepository upgradeItemsRepository, CarController carController,Transform placeForUI)
        {
            _inventoryModel = inventoryModel;
            _inventoryCarItemsRepository = inventoryCarItemsRepository;
            _defaultCarItemsRepository = defaultCarItemsRepository;
            _upgradeHandlerRepository = upgradeItemsRepository;
            _carController = carController;
            _carView = carController.GetCarView();
            EquipDefaultItems();

            _inventoryView = Object.Instantiate(ResourceLoader.LoadObject<InventoryView>(_viewPath),
                                                placeForUI,
                                                false);
            AddGameObject(_inventoryView.gameObject);

            var equippedItems = _inventoryCarItemsRepository.Collection.Values.ToList();
            _inventoryView.InitView(equippedItems);

            _inventoryView.Selected += OnItemSelected;
            _inventoryView.Deselected += OnItemDeselected;
        }
        public void HideInventory()
        {
            _inventoryView.Active = false;
            _inventoryView.Hide();
            Debug.Log(_inventoryView.Active);
        }
        public void SnowInventory()
        {
            _inventoryView.Active = true;
            _inventoryView.Show();
            ShowItems();
            Debug.Log(_inventoryView.Active);
        }
        private void OnItemSelected(object sender, IItem item)
        {            
            _inventoryModel.EquipItem(item);
            _carController.SetCarPart(_carView, item.Info.CarPartType, item.Info.Prefab, true);
            //Debug.Log($"{item.Info.Title} equipped.");
            ShowItems(); //дл€ дебага
        }
        private void OnItemDeselected(object sender, IItem item)
        {
            _inventoryModel.UnequipItem(item);
            _carController.SetCarPart(_carView, item.Info.CarPartType, item.Info.Prefab, false);
            //Debug.Log($"{item.Info.Title} unequipped.");
            ShowItems(); //дл€ дебага
        }
        public void EquipDefaultItems()
        {
            var defaultItems = _defaultCarItemsRepository.Collection.Values.ToList();
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

        public bool InventoryStatus()
        {
            return _inventoryView.Active;
        }

        //ћетод дл€ дебага, потом удалю
        private void ShowItems()
        {
            foreach (var item in GetEquippedItems())
            {
                Debug.Log(item.Id);
                Debug.Log(item.Info.Title);
                Debug.Log(item.Info.CarPartType);
                Debug.Log("======================");
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
