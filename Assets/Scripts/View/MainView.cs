using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CarGame2D
{
    public class MainView : MonoBehaviour
    {
        private MainController _mainController;
        private UnityAnaliticsTools analyticTools;

        [SerializeField]
        private RewardView _rewardView;
        [SerializeField]
        private CurrencyView _currencyView;
        [SerializeField]
        private FightWindowView _fightWindowView;
        [SerializeField]
        private StartFightView _startFightView;

        [SerializeField]
        private Transform _placeForUi;
        [SerializeField]
        private UnityAdsTools _unityAdsTools;
        [SerializeField]
        private ItemConfig[] _itemsConfig;
        [SerializeField]
        private CarPartConfig[] _defaultItemsConfig;
        [SerializeField]
        private CarPartConfig[] _inventoryItemsConfig;
        [SerializeField]
        private UpgradeItemConfig[] _upgradeItemsCfg;
        [SerializeField]
        private AbilityItemConfig[] _abilityItemsCfg;



        private void Awake()
        {
            var profilePlayer = new ProfilePlayerModel(15f, _unityAdsTools, analyticTools);
            profilePlayer.CurrentState.Value = GameState.Start;
            _mainController = new MainController(_placeForUi, profilePlayer, _rewardView, _currencyView, _fightWindowView, _startFightView,
                _itemsConfig.ToList(), _inventoryItemsConfig.ToList(), _upgradeItemsCfg.ToList(), _defaultItemsConfig.ToList(), _abilityItemsCfg.ToList());
        }

        protected void OnDestroy()
        {
            _mainController?.Dispose();
        }
    }
}
