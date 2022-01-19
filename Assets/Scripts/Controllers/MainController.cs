using System.Collections.Generic;
using UnityEngine;

namespace CarGame2D
{
    public class MainController : BasicController
    {
        public MainController(Transform placeForUi, ProfilePlayerModel profilePlayer, List<CarPartConfig> itemsCfg, List<UpgradeItemConfig> upgradedItemsCfg, List<CarPartConfig> defaultItemsCfg, List<AbilityItemConfig> abilityItemsConfig)
        {
            _profilePlayer = profilePlayer;
            _placeForUI = placeForUi;
            _carItemsConfigs = itemsCfg;
            _defaultCarItemsConfigs = defaultItemsCfg;
            _upgradeItemsCfg = upgradedItemsCfg;
            _abilityItemConfigs = abilityItemsConfig;
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
        private readonly List<CarPartConfig> _carItemsConfigs;
        private readonly List<UpgradeItemConfig> _upgradeItemsCfg;
        private readonly List<CarPartConfig> _defaultCarItemsConfigs;
        private readonly List<AbilityItemConfig> _abilityItemConfigs;

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
                    var defaultEquippedCarItemsRepository = new CarPartsRepository(_defaultCarItemsConfigs);
                    AddController(defaultEquippedCarItemsRepository);

                    var carController = new CarController();
                    AddController(carController);

                    var inventoryModel = new InventoryModel();

                    var carItemsInventoryRepository = new CarPartsRepository(_carItemsConfigs);
                    AddController(carItemsInventoryRepository);

                    var upgradeItemsRepository = new UpgradeHandlerRepository(_upgradeItemsCfg);
                    AddController(upgradeItemsRepository);

                    var abilityItemConfigs = new AbilityRepository(_abilityItemConfigs);
                    AddController(abilityItemConfigs);

                    _inventoryController = new InventoryController(inventoryModel, defaultEquippedCarItemsRepository, carItemsInventoryRepository, upgradeItemsRepository, carController, _placeForUI);
                    AddController(_inventoryController);

                    _abilityController = new AbilityController(abilityItemConfigs, _placeForUI);
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
