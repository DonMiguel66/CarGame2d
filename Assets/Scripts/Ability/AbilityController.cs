using UnityEngine;


namespace CarGame2D
{
    public class AbilityController: BasicController, IAbilitiesController
    {
        private readonly AbilityRepository _abilityRepository;
        private readonly AbilityCollectionView _abilityCollectionView;
        private readonly ItemsRepository _itemsRepository;
        private readonly InventoryModel _abilityInventoryModel;

        private readonly ResourcePath _viewPath = new ResourcePath { PathResources = "Prefabs/AbilitiesCollectionView" };

        public AbilityController(ItemsRepository itemsRepository, InventoryModel abilityInventoryModel, AbilityRepository abilityRepository, IAbilityActivator abilityActivator,Transform placeForUI)
        {
            _abilityRepository = abilityRepository;
            _itemsRepository = itemsRepository;
            _abilityInventoryModel = abilityInventoryModel;
            _abilityCollectionView = Object.Instantiate(ResourceLoader.LoadObject<AbilityCollectionView>(_viewPath),
                                                placeForUI,
                                                false);
            AddGameObject(_abilityCollectionView.gameObject);
            EquipDefaultAbilities(_itemsRepository);
            _abilityCollectionView.InitView(_abilityInventoryModel.GetEquippedItems());
            _abilityCollectionView.UseRequested += OnAbilityUseRequested;

        }
        private void OnAbilityUseRequested(IItem e)
        {
            if (_abilityRepository.Collection.TryGetValue(e.Id, out var ability))
                ability.Apply();
        }
        public void HideAbilities()
        {
            _abilityCollectionView.Active = false;
            _abilityCollectionView.Hide();
        }
        public void ShowAbilities()
        {
            _abilityCollectionView.Active = true;
            _abilityCollectionView.Show();
        }
       
        public bool AbilityStatus()
        {
            return _abilityCollectionView.Active;
        }

        private void EquipDefaultAbilities(ItemsRepository itemsRepository)
        {
            foreach (var ability in itemsRepository.Collection)
            {
                Debug.Log(ability.Value.Info.Title);
                _abilityInventoryModel.EquipItem(ability.Value);
            }
        }

        protected override void OnDispose()
        {
            _abilityCollectionView.UseRequested -= OnAbilityUseRequested;
            base.OnDispose();
        }
    }
}
