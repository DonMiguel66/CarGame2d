namespace CarGame2D
{
    public class ProfilePlayerModel
    {
        public CarModel CurrentCar { get; }
        public SubscribeProperty<GameState> CurrentState { get; }
        public UnityAnaliticsTools AnalyticsTools { get; }

        public IAdsShower AdsShower { get; }

        public ProfilePlayerModel(float speed, UnityAdsTools _unityAdsTools, UnityAnaliticsTools analyticsTools)
        {
            CurrentState = new SubscribeProperty<GameState>();
            CurrentCar = new CarModel(speed);
            AnalyticsTools = analyticsTools;
            AdsShower = _unityAdsTools;
        }


    }
}
