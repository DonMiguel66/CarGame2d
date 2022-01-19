using System;
using System.Collections.Generic;

namespace CarGame2D
{
    public interface IAbility  
    {
        string Title { get; }
        void Apply();
    }

    public interface IAbilitiesController
    {
        void ShowAbilities();
        void HideAbilities();
    }

    public interface IAbilityCollectionView :IView
    {
        event EventHandler<IAbility> UseRequested;
        void InitView(IReadOnlyList<IAbility> abilityItems);
    }

}
