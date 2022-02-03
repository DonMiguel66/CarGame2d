using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CarGame2D
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private CustomButton _buttonStart;

        [SerializeField] private CustomButton _buttonReward;

        [SerializeField] private CustomButton _buttonExit;
        public void Init(UnityAction startGame, UnityAction doReward)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonReward.onClick.AddListener(doReward);
            _buttonReward.onClick.AddListener(ExitGame);
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonReward.onClick.RemoveAllListeners();
            _buttonExit.onClick.RemoveAllListeners();
        }
    }
}
