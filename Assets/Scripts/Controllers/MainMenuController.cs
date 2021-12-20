using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarGame2D
{
    public class MainMenuController : BasicController 
    {
        private readonly ResourcePath _carPath = new ResourcePath { PathResources = "Prefabs/MainMenu" };
        private readonly ProfilePlayerModel _profilePlayer;
        private readonly MainMenuView _mainMenuView;

        public MainMenuController(Transform placeForUI, ProfilePlayerModel profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _mainMenuView = LoadView(placeForUI);
            _mainMenuView.Init(StartGame);
        }

        private MainMenuView LoadView(Transform placeForUI)
        {
            var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_carPath), placeForUI, false);
            AddGameObject(objectView);

            if (objectView != null)
                return objectView.GetComponent<MainMenuView>();
            else
                return null;
        }

        private void StartGame()
        {
            _profilePlayer.CurrentState.Value = GameState.Game;
        }
    }
}
