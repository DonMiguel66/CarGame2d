using UnityEngine;

namespace CarGame2D
{
    public class CurrencyController : BasicController
    {
        public CurrencyController(Transform placeForUI, CurrencyView currencyView)
        {
            var currencyViewInstance = Object.Instantiate(currencyView, placeForUI);
            AddGameObject(currencyViewInstance.gameObject);
        }
    }
}
