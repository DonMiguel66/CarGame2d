using System;
using UnityEngine;
using UnityEngine.UI;

namespace CarGame2D
{
    public class ItemButtonView : MonoBehaviour
    {
        public event EventHandler<(IItem item, bool isInUse)> UseRequested;
        [SerializeField] private Text _title;
        [SerializeField] private Button _button;
        private IItem _item;
        private bool _isInUse = false;

        private void OnEnable()
        {
            _button.onClick.AddListener(ButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void ButtonClicked()
        {
            if (_item == null)
            {
                throw new ArgumentNullException(nameof(_item));
            }

            _isInUse = !_isInUse;
            UseRequested.Invoke(this, (_item, _isInUse));
        }

        public void Init(IItem item)
        {
            _item = item ?? throw new ArgumentNullException(nameof(item));
            _title.text = _item.Info.Title;
        }

        private void OnDestroy()
        {
            UseRequested = null;
        }
    }
}
