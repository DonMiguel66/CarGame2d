using System;
using System.Collections.Generic;
using UnityEngine;

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

        private readonly ResourcePath _viewPath = new ResourcePath { PathResources = "Prefabs/ItemView" };
        private List<ItemButtonView> _itemsView = new List<ItemButtonView>();

        public void InitView(List<IItem> itemInfoCollection)
        {
            foreach (var item in itemInfoCollection)
            {
                var view = Instantiate(ResourceLoader.LoadObject<ItemButtonView>(_viewPath), _placeForUI, false);
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
