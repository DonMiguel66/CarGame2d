using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CarGame2D
{
    public class MainView : MonoBehaviour
    {
        private MainController _mainController;
        private UnityAnaliticsTools analyticTools;
        [SerializeField] private Button _buttonInventory;
        [SerializeField] private Button _buttonAbility;


        [SerializeField]
        private Transform _placeForUi;
        [SerializeField]
        private UnityAdsTools _unityAdsTools;
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
            _mainController = new MainController(_placeForUi, profilePlayer, _inventoryItemsConfig.ToList(), _upgradeItemsCfg.ToList(), _defaultItemsConfig.ToList(), _abilityItemsCfg.ToList());
        }

        protected void OnDestroy()
        {
            _mainController?.Dispose();
        }
    }
}
