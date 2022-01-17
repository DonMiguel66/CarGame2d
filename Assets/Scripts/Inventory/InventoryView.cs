using System;
using System.Collections.Generic;
using UnityEngine;

namespace CarGame2D
{
    public class InventoryView : MonoBehaviour, IInventoryView
    {
        public event EventHandler<IItem> Selected;
        public event EventHandler<IItem> Deselected;
        [SerializeField]
        private Transform _placeForUI;
        public Transform PlaceFotUI => _placeForUI;

        private readonly ResourcePath _viewPath = new ResourcePath { PathResources = "Prefabs/ItemView" };
        private List<ItemButtonView> _itemsView = new List<ItemButtonView>();

        public void Display(List<IItem> itemInfoCollection)
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
