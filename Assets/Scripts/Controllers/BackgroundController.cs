
using UnityEngine;

namespace CarGame2D
{
    public class BackgroundController : BasicController
    {
        private readonly ResourcePath _viewPath = new ResourcePath { PathResources = "Prefabs/background" };
        private BackgroundView _view;
        private readonly SubscribeProperty<float> _diff;
        private readonly IReadonlySubscribeProperty<float> _leftMove;
        private readonly IReadonlySubscribeProperty<float> _rightMove;

        public BackgroundController(IReadonlySubscribeProperty<float> leftMove,
        IReadonlySubscribeProperty<float> rightMove)
        {
            _view = LoadView();
            _diff = new SubscribeProperty<float>();

            _leftMove = leftMove;
            _rightMove = rightMove;

            _view.Init(_diff);

            _leftMove.SubscribeOnChange(Move);
            _rightMove.SubscribeOnChange(Move);
        }


        protected override void OnDispose()
        {
            _leftMove.UnsubscribeOnChange(Move);
            _rightMove.UnsubscribeOnChange(Move);

            base.OnDispose();
        }

        private BackgroundView LoadView()
        {
            var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
            AddGameObject(objView);

            return objView.GetComponent<BackgroundView>();
        }

        private void Move(float value)
        {
            _diff.Value = value;
        }
    }
}
