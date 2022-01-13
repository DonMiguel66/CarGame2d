namespace CarGame2D
{
    public class CarModel : IUpgradableCar
    {
        private readonly float _defaultSpeed;
        public float Speed { get; set; }

        public CarModel(float speed)
        {
            _defaultSpeed = speed;
            ResetSpeed();
        }

        public void ResetSpeed()
        {
            Speed = _defaultSpeed;
        }

    }
}