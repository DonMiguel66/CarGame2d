using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

namespace CarGame2D
{
    public class UnityAdsTools : MonoBehaviour, IAdsShower
    {
        private const string GameID = "4518645";
        private const string BannerPlacementID = "Banner_Android";
        private void Start()
        {
            Advertisement.Initialize(GameID);
        }
        public void ShowBanner()
        {
            Advertisement.Show(BannerPlacementID);
        }
    }
}
