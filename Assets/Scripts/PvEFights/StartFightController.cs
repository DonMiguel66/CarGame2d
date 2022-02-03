using UnityEngine;

namespace CarGame2D
{
    public class StartFightController :BasicController
    {
        private StartFightView _startFightView;
        private ProfilePlayerModel _profilePlayer;
        public StartFightController(Transform placeForUI, ProfilePlayerModel profilePlayer, StartFightView startFightView)
        {
            _profilePlayer = profilePlayer;
            _startFightView = Object.Instantiate(startFightView, placeForUI);
            AddGameObject(_startFightView.gameObject);

            SubscribesButtons();

        }

        private void SubscribesButtons()
        {
            _startFightView.StartFightButton.onClick.AddListener(StartFight);
        }

        private void StartFight()
        {
            _profilePlayer.CurrentState.Value = GameState.Fight;
        }

        protected override void OnDispose()
        {
            _startFightView.StartFightButton.onClick.RemoveAllListeners();
            base.OnDispose();
        }
    }
}
