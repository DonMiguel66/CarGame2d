using UnityEngine;

namespace CarGame2D
{
    public class GameController : BasicController
    {
        public GameController(ProfilePlayerModel playerProfile)
        {
            var leftMoveDiff = new SubscribeProperty<float>();
            var rightMoveDiff = new SubscribeProperty<float>();

            var tapeBackgroundController = new BackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, playerProfile.CurrentCar);
            AddController(inputGameController);

            var carController = new CarController();
            AddController(carController);            

        }
    }
}
