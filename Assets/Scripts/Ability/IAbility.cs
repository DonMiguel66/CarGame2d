using System;
using System.Collections.Generic;
using UnityEngine;

namespace CarGame2D
{
    public interface IAbility  
    {
        void Apply();
    }

    public interface IAbilitiesController
    {
        void ShowAbilities();
        void HideAbilities();
    }

    public interface IAbilityActivator
    {
        GameObject GetViewObject();
    }


    public interface IAbilityCollectionView :IView
    {
        event Action<IItem> UseRequested;
        void InitView(IReadOnlyList<IItem> abilityItems);
    }

}
