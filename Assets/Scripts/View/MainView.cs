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
        private ItemConfig[] _itemsConfig;

        [SerializeField]
        private UpgradeItemConfig[] _upgradeItemsCfg;

        private void Awake()
        {
            var profilePlayer = new ProfilePlayerModel(15f, _unityAdsTools);
            profilePlayer.CurrentState.Value = GameState.Start;
            _mainController = new MainController(_placeForUi, profilePlayer, _itemsConfig.ToList(), _upgradeItemsCfg.ToList());
        }

        protected void OnDestroy()
        {
            _mainController?.Dispose();
        }
    }
}
