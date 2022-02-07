using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CarGame2D
{
    public class AbilityCollectionView : MonoBehaviour, IAbilityCollectionView
    {
        public event Action<IItem> UseRequested;

        [SerializeField]
        private Transform _placeForUI;

        private bool _active;
        public bool Active { get => _active; set => _active = value; }

        private IReadOnlyList<IItem> _abilityItems;
        private List<AbilityButtonView> _abilityViews;
        private readonly string _key = "AbilityView";
        //private readonly ResourcePath _viewPath = new ResourcePath { PathResources = "Prefabs/AbilityView" };
        private void UseAbility(IItem item)
        {
            UseRequested?.Invoke(item);
        }    

        public async void InitView(IReadOnlyList<IItem> abilityItems)
        {
            _abilityItems = abilityItems;
            _abilityViews = new List<AbilityButtonView>();

            foreach (var ability in _abilityItems)
            {
                var view = (await Addressables.InstantiateAsync(_key, _placeForUI, false).Task).GetComponent<AbilityButtonView>();
                //var view = Instantiate(ResourceLoader.LoadObject<AbilityButtonView>(_viewPath), _placeForUI, false);
                view.Init(ability);
                view.UseRequested += (sender, item) => UseAbility(item);
                _abilityViews.Add(view);
            }
            gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            foreach (var view in _abilityViews)
            {
                Destroy(view);
            }
        }
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
