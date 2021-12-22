namespace CarGame2D
{
    public class ProfilePlayerModel
    {
        public CarModel CurrentCar { get; }
        public SubscribeProperty<GameState> CurrentState { get; }
        public IAnalyticsTools AnalyticsTools { get; }

        public IAdsShower AdsShower { get; }

        public ProfilePlayerModel(float speed, UnityAdsTools _unityAdsTools)
        {
            CurrentState = new SubscribeProperty<GameState>();
            CurrentCar = new CarModel(speed);
            AnalyticsTools = new UnityAnaliticsTools();
            AdsShower = _unityAdsTools;
        }


    }
}
