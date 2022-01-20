using System.Collections.Generic;
using UnityEngine;

namespace CarGame2D
{
    public class MainController : BasicController
    {
        public MainController(Transform placeForUi, ProfilePlayerModel profilePlayer, List<ItemConfig> itemConfigs, List<CarPartConfig> carPartsCfg, List<UpgradeItemConfig> upgradedItemsCfg, List<CarPartConfig> defaultCarPartsCfg, List<AbilityItemConfig> abilityItemsConfig)
        {
            _profilePlayer = profilePlayer;
            _placeForUI = placeForUi;
            _itemsConfigs = itemConfigs;
            _carPartsConfigs = carPartsCfg;
            _defaultCarPartsConfigs = defaultCarPartsCfg;
            _upgradeItemsCfg = upgradedItemsCfg;
            _abilityItemsConfigs = abilityItemsConfig;
            OnChangeGameState(_profilePlayer.CurrentState.Value);
            profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        }

        private MainMenuController _mainMenuController;
        private GameController _gameController;
        private InGameUIController _inGameUIController;
        private InventoryController _inventoryController;
        private AbilityController _abilityController;

        private readonly Transform _placeForUI;
        private readonly ProfilePlayerModel _profilePlayer;
        private readonly List<CarPartConfig> _carPartsConfigs;
        private readonly List<CarPartConfig> _defaultCarPartsConfigs;
        private readonly List<ItemConfig> _itemsConfigs;
        private readonly List<AbilityItemConfig> _abilityItemsConfigs;
        private readonly List<UpgradeItemConfig> _upgradeItemsCfg;

        protected override void OnDispose()
        {
            _mainMenuController?.Dispose();
            _gameController?.Dispose();
            _profilePlayer.CurrentState.UnsubscribeOnChange(OnChangeGameState);
            base.OnDispose();
        }

        private void OnChangeGameState(GameState state)
        {
            switch (state)
            {
                case GameState.Start:
                    _mainMenuController = new MainMenuController(_placeForUI, _profilePlayer);
                    _gameController?.Dispose();
                    break;
                case GameState.Game:
                    var defaultEquippedCarItemsRepository = new CarPartsRepository(_defaultCarPartsConfigs);
                    AddController(defaultEquippedCarItemsRepository);

                    var carController = new CarController();
                    AddController(carController);

                    var inventoryModel = new InventoryModel();
                    var abilityInventoryModel = new InventoryModel();

                    var carItemsInventoryRepository = new CarPartsRepository(_carPartsConfigs);
                    AddController(carItemsInventoryRepository);

                    var upgradeItemsRepository = new UpgradeHandlerRepository(_upgradeItemsCfg);
                    AddController(upgradeItemsRepository);

                    var itemsRepository = new ItemsRepository(_itemsConfigs);
                    AddController(itemsRepository);

                    var abilityItemsRepository = new AbilityRepository(_abilityItemsConfigs);
                    AddController(abilityItemsRepository);

                    _inventoryController = new InventoryController(inventoryModel, defaultEquippedCarItemsRepository, carItemsInventoryRepository, upgradeItemsRepository, carController, _placeForUI);
                    AddController(_inventoryController);

                    _abilityController = new AbilityController(itemsRepository, abilityInventoryModel, abilityItemsRepository, carController,_placeForUI);
                    AddController(_abilityController);

                    _inGameUIController = new InGameUIController(_placeForUI, _inventoryController, _abilityController);
                    AddController(_inGameUIController);

                    _gameController = new GameController(_profilePlayer);
                    _mainMenuController?.Dispose();
                    break;
                default:
                    _mainMenuController?.Dispose();
                    _gameController?.Dispose();
                    break;
            }
        }
    }
}
