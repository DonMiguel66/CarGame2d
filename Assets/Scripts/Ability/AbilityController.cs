using System.Linq;
using UnityEngine;


namespace CarGame2D
{
    public class AbilityController:BasicController
    {
        private AbilityRepository _abilityRepository;
        private AbilityCollectionView _abilityCollectionView;

        private readonly ResourcePath _viewPath = new ResourcePath { PathResources = "Prefabs/AbilitiesView" };

        public AbilityController(AbilityRepository abilityRepository, Transform placeForUI)
        {
            _abilityRepository = abilityRepository;
            _abilityCollectionView = Object.Instantiate(ResourceLoader.LoadObject<AbilityCollectionView>(_viewPath),
                                                placeForUI,
                                                false);
            _abilityCollectionView.InitView(_abilityRepository.Collection.Values.ToList());
            _abilityCollectionView.UseRequested += OnAbilityUseRequested;

        }
        private void OnAbilityUseRequested(object sender, IAbility e)
        {
            e.Apply();
        }
        public void HideInventory()
        {
            _abilityCollectionView.Active = false;
            _abilityCollectionView.Hide();
        }
        public void SnowInventory()
        {
            _abilityCollectionView.Active = true;
            _abilityCollectionView.Show();
        }
        public bool AbilityStatus()
        {
            return _abilityCollectionView.Active;
        }
        protected override void OnDispose()
        {
            _abilityCollectionView.UseRequested -= OnAbilityUseRequested;
            base.OnDispose();
        }
    }
}
