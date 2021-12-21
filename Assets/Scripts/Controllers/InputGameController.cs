using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarGame2D
{
    public class InputGameController : BasicController
    {

        //private readonly ResourcePath _viewPath = new ResourcePath { PathResources = "Prefabs/endlessMove" };
        //private readonly ResourcePath _viewPath = new ResourcePath { PathResources = "Prefabs/SwipeInput" };
        //private readonly ResourcePath _viewPath = new ResourcePath { PathResources = "Prefabs/JoystickInput" };
        private readonly ResourcePath _viewPath = new ResourcePath { PathResources = "Prefabs/TouchButtonInput" };
        private BasicInputView _view;

        public InputGameController(SubscribeProperty<float> leftMove, SubscribeProperty<float> rightMove, CarModel car)
        {
            _view = LoadView();
            _view.Init(leftMove, rightMove, car.Speed);
        }

        private BasicInputView LoadView()
        {
            var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
            AddGameObject(objectView);

            if (objectView != null)
                return objectView.GetComponent<BasicInputView>();
            else
                return null;
        }
    }
}
