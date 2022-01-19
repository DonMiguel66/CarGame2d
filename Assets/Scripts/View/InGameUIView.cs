using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CarGame2D
{
    public class InGameUIView : MonoBehaviour
    {
        [SerializeField] private Button _buttonInventoryMenu;
        [SerializeField] private Button _buttonAbilityMenu;

        public void Init(UnityAction openInventory, UnityAction openAbilities)
        {
            _buttonInventoryMenu.onClick.AddListener(openInventory);
            _buttonAbilityMenu.onClick.AddListener(openAbilities);
        }
        protected void OnDestroy()
        {
            _buttonInventoryMenu.onClick.RemoveAllListeners();
            _buttonAbilityMenu.onClick.RemoveAllListeners();
        }
    }
}
