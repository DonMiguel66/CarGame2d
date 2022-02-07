using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CarGame2D
{
    public class MainMenuController : BasicController 
    {
        private readonly string _key = "MainMenu";
        private readonly ProfilePlayerModel _profilePlayer;
        private readonly MainMenuView _mainMenuView;

        public MainMenuController(Transform placeForUI, ProfilePlayerModel profilePlayer)
        {
            _profilePlayer = profilePlayer; 
            LoadViewAsync(placeForUI);            
        }

        

        private async void LoadViewAsync(Transform placeForUI)
        {
            //var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_path), placeForUI, false);
            var objectView = (await Addressables.InstantiateAsync(_key, placeForUI, false).Task).GetComponent<MainMenuView>();
            AddGameObject(objectView.gameObject);
            objectView.Init(StartGame, DoReward);
        }

        private void StartGame()
        {
            _profilePlayer.CurrentState.Value = GameState.Game;
            //_profilePlayer.AnalyticsTools.SendMessage("start_game", ("time", Time.realtimeSinceStartup));
            _profilePlayer.AdsShower.ShowBanner();
        }

        private void DoReward()
        {
            _profilePlayer.CurrentState.Value = GameState.Reward;
        }

    }
}
