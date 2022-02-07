using System.Collections.Generic;
using UnityEngine;

namespace CarGame2D
{
    public class MainController : BasicController
    {
        private readonly RewardView _rewardView;
        private readonly CurrencyView _currencyView;
        private readonly FightWindowView _fightWindowView;
        private readonly StartFightView _startFightView;

        public MainController(Transform placeForUi, ProfilePlayerModel profilePlayer, RewardView rewardView, CurrencyView currencyView, FightWindowView fightWindowView, StartFightView startFightView, 
            List<ItemConfig> itemConfigs, List<CarPartConfig> carPartsCfg, List<UpgradeItemConfig> upgradedItemsCfg, List<CarPartConfig> defaultCarPartsCfg, List<AbilityItemConfig> abilityItemsConfig)

        {
            _profilePlayer = profilePlayer;
            _placeForUI = placeForUi;

            _rewardView = rewardView;
            _currencyView = currencyView;
            _fightWindowView = fightWindowView;
            _startFightView = startFightView;

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
        private CarController _carController;
        private InGameUIController _inGameUIController;
        private InventoryController _inventoryController;
        private AbilityController _abilityController;
        private RewardController _rewardController;
        private FightWindowController _fightWindowController;
        private StartFightController _startFightController;

        private readonly Transform _placeForUI;
        private readonly ProfilePlayerModel _profilePlayer;
        private readonly List<CarPartConfig> _carPartsConfigs;
        private readonly List<CarPartConfig> _defaultCarPartsConfigs;
        private readonly List<ItemConfig> _itemsConfigs;
        private readonly List<AbilityItemConfig> _abilityItemsConfigs;
        private readonly List<UpgradeItemConfig> _upgradeItemsCfg;

        protected override void OnDispose()
        {
            DisposeAllControllers();

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
                    _rewardController?.Dispose();
                    break;
                case GameState.Game:
                    _gameController = new GameController(_profilePlayer);
                    _startFightController = new StartFightController(_placeForUI, _profilePlayer, _startFightView);

                    var defaultEquippedCarItemsRepository = new CarPartsRepository(_defaultCarPartsConfigs);
                    AddController(defaultEquippedCarItemsRepository);

                    _carController = new CarController();
                    AddController(_carController);

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

                    _inventoryController = new InventoryController(inventoryModel, defaultEquippedCarItemsRepository, carItemsInventoryRepository, upgradeItemsRepository, _carController, _placeForUI);
                    AddController(_inventoryController);

                    _abilityController = new AbilityController(itemsRepository, abilityInventoryModel, abilityItemsRepository, _carController, _placeForUI);
                    AddController(_abilityController);

                    _inGameUIController = new InGameUIController(_placeForUI, _inventoryController, _abilityController);
                    AddController(_inGameUIController);

                    _mainMenuController?.Dispose();
                    _fightWindowController?.Dispose();

                    break;
                case GameState.Reward:
                    _rewardController = new RewardController(_placeForUI, _profilePlayer, _rewardView, _currencyView);
                    _rewardController.RefreshView();

                    _mainMenuController?.Dispose();

                    break;
                case GameState.Fight:
                    _fightWindowController = new FightWindowController(_placeForUI, _profilePlayer, _fightWindowView);
                    _fightWindowController.RefreshView();

                    _carController?.Dispose();
                    _inGameUIController?.Dispose();
                    _gameController?.Dispose();
                    _startFightController?.Dispose();
                    _inventoryController?.Dispose();
                    _inGameUIController?.Dispose();
                    _abilityController?.Dispose();
                    break;
                default:
                    DisposeAllControllers();
                    break;
            }
        }

        private void DisposeAllControllers()
        {
            _mainMenuController?.Dispose();
            _gameController?.Dispose();
            _carController?.Dispose();
            _fightWindowController?.Dispose();
            _rewardController?.Dispose();
            _startFightController?.Dispose(); 
            _inGameUIController?.Dispose();
        }
    }
}
