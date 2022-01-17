using System.Collections.Generic;
using UnityEngine;

namespace CarGame2D
{
    public class MainController : BasicController
    {
        public MainController(Transform placeForUi, ProfilePlayerModel profilePlayer, List<ItemConfig> itemsCfg, List<UpgradeItemConfig> upgradedItemsCfg, List<ItemConfig> defaultItemsCfg)
        {
            _profilePlayer = profilePlayer;
            _placeForUi = placeForUi;
            _itemConfigs = itemsCfg;
            _defaultItemConfigs = defaultItemsCfg;
            _upgradeItemsCfg = upgradedItemsCfg;
            OnChangeGameState(_profilePlayer.CurrentState.Value);
            profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        }

        private MainMenuController _mainMenuController;
        private GameController _gameController;
        private InventoryController _inventoryController;

        private readonly Transform _placeForUi;
        private readonly ProfilePlayerModel _profilePlayer;
        private readonly List<ItemConfig> _itemConfigs;
        private readonly List<UpgradeItemConfig> _upgradeItemsCfg;
        private readonly List<ItemConfig> _defaultItemConfigs;

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
                    _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                    _gameController?.Dispose();
                    break;
                case GameState.Game:
                    var defaultEquippedItemRepository = new ItemsRepository(_defaultItemConfigs);
                    AddController(defaultEquippedItemRepository);

                    var carController = new CarController(defaultEquippedItemRepository);
                    AddController(carController);

                    var inventoryModel = new InventoryModel();
                    var itemsInventoryRepository = new ItemsRepository(_itemConfigs);
                    AddController(itemsInventoryRepository);

                    var upgradeItemsRepository = new UpgradeHandlerRepository(_upgradeItemsCfg);
                    AddController(upgradeItemsRepository);

                    _inventoryController = new InventoryController(inventoryModel, defaultEquippedItemRepository, itemsInventoryRepository, upgradeItemsRepository, _profilePlayer, carController, _placeForUi);
                    AddController(_inventoryController);
                    _inventoryController.SnowInventory();

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
