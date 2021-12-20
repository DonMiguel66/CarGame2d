namespace CarGame2D
{
    public class GameController : BasicController
    {
        public GameController()
        {
            var carController = new CarController();
            AddController(carController);
        }
    }
}
