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

        private void Awake()
        {
            var profilePlayer = new ProfilePlayerModel(15f, _unityAdsTools);
            profilePlayer.CurrentState.Value = GameState.Start;
            _mainController = new MainController(_placeForUi, profilePlayer);
        }

        protected void OnDestroy()
        {
            _mainController?.Dispose();
        }
    }
}
