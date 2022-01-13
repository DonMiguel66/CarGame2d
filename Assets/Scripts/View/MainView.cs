using System.Linq;
using UnityEngine;

namespace CarGame2D
{
    public class MainView : MonoBehaviour
    {
        [SerializeField]
        private Transform _placeForUi;

        [SerializeField]
        private UnityAdsTools _unityAdsTools;

        private MainController _mainController;

        [SerializeField]
        private ItemConfig[] _defaultItemsConfig;

        [SerializeField]
        private ItemConfig[] _inventoryItemsConfig;

        [SerializeField]
        private UpgradeItemConfig[] _upgradeItemsCfg;

        [SerializeField]
        private CarPartsConfig _carPartsConfig;

        private void Awake()
        {
            var profilePlayer = new ProfilePlayerModel(15f, _unityAdsTools);
            profilePlayer.CurrentState.Value = GameState.Start;
            _mainController = new MainController(_placeForUi, profilePlayer, _inventoryItemsConfig.ToList(), _upgradeItemsCfg.ToList(), _defaultItemsConfig.ToList());
        }

        protected void OnDestroy()
        {
            _mainController?.Dispose();
        }
    }
}
