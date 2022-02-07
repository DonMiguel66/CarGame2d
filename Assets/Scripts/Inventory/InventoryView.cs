using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


namespace CarGame2D
{
    public class InventoryView : MonoBehaviour, IInventoryView
    {
        public event EventHandler<IItem> Selected;
        public event EventHandler<IItem> Deselected;

        private bool _active;

        [SerializeField]
        private Transform _placeForUI;
        
        public Transform PlaceFotUI => _placeForUI;

        public bool Active { get => _active; set => _active = value; }

        private readonly string _key = "ItemButtonView";
        private List<ItemButtonView> _itemsView = new List<ItemButtonView>();

        public async void InitView(List<IItem> itemInfoCollection)
        {
            foreach (var item in itemInfoCollection)
            {
                var view = (await Addressables.InstantiateAsync(_key, _placeForUI, false).Task).GetComponent<ItemButtonView>();
                view.Init(item);
                view.UseRequested += (sender, arg) =>
                {
                    if (arg.isInUse)
                    {
                        OnSelected(arg.item);
                    }
                    else
                    {
                        OnDeselected(arg.item);
                    }
                };              
                _itemsView.Add(view);
            }
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(Active);
        }

        public void Hide()
        {
            gameObject.SetActive(Active);
        }

        protected virtual void OnSelected(IItem e)
        {
            Selected?.Invoke(this, e);
        }

        protected virtual void OnDeselected(IItem e)
        {
            Deselected?.Invoke(this, e);
        }

        public void OnDestroy()
        {
            foreach (var view in _itemsView)
            {
                Destroy(view);
            }
        }
    }
}
