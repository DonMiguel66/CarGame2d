using System;
using UnityEngine;
using UnityEngine.UI;

namespace CarGame2D
{
    public class AbilityButtonView : MonoBehaviour, IView
    {
        public event EventHandler<IItem> UseRequested;
        [SerializeField] private Text _title;
        [SerializeField] private Button _button;
        private IItem _item;

        private void OnEnable()
        {
            _button.onClick.AddListener(ButtonClicked);
        }

        private void ButtonClicked()
        {
            UseRequested.Invoke(this, _item);
        }

        public void Init(IItem item)
        {
            _item = item ?? throw new ArgumentNullException(nameof(item));
            _title.text = _item.Info.Title;
        }
        public void Hide()
        {
            throw new System.NotImplementedException();
        }

        public void Show()
        {
            throw new System.NotImplementedException();
        }
        private void OnDestroy()
        {
            UseRequested = null;
        }
    }
}
