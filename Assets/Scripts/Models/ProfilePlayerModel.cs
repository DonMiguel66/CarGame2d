namespace CarGame2D
{
    public class ProfilePlayerModel
    {
        public CarModel CurrentCar { get; }

        public SubscribeProperty<GameState> CurrentState { get; }
        public ProfilePlayerModel(float speed)
        {
            CurrentState = new SubscribeProperty<GameState>();
            CurrentCar = new CarModel(speed);
        }

    }
}
