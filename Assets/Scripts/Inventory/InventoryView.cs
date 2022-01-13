using System;
using System.Collections.Generic;
using UnityEngine;

namespace CarGame2D
{
    public class InventoryView : MonoBehaviour, IInventoryView
    {
        //Возможно, изменить вью под монобех с выводом информации(поля, свойства из контроллера) через канвас (визуал) и т.д.
        public event EventHandler<IItem> Selected;
        public event EventHandler<IItem> Deselected;
        [SerializeField]
        private Transform _placeForUI;
        public Transform PlaceFotUI => _placeForUI;

        private List<IItem> _itemInfoCollection;
        private readonly ResourcePath _viewPath = new ResourcePath { PathResources = "Prefabs/ItemView" };
        private List<ItemView> _itemsView = new List<ItemView>();

        public void Display(List<IItem> itemInfoCollection)
        {
            _itemInfoCollection = itemInfoCollection;
            foreach (var item in _itemInfoCollection)
            {
                var view = Instantiate(ResourceLoader.LoadObject<ItemView>(_viewPath), _placeForUI, false);
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
    }
}
