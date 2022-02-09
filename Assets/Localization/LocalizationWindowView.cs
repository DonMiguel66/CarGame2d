using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace CarGame2D
{
    public class LocalizationWindowView : MonoBehaviour
    {
        [SerializeField]
        private Button _ruButton;

        [SerializeField]
        private Button _engButton;
        void Start()
        {
            _ruButton.onClick.AddListener(() => ChangeLocale(1));
            _engButton.onClick.AddListener(() => ChangeLocale(0));
        }

        private void ChangeLocale(int index)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        }

        private void OnDestroy()
        {
            _ruButton.onClick.RemoveAllListeners();
            _engButton.onClick.RemoveAllListeners();
        }
    }
}
