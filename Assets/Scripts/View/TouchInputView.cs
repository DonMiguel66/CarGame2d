using JoostenProductions;
using System;
using UnityEngine;

namespace CarGame2D
{
    public class TouchInputView : BasicInputView
    {
        [SerializeField] private TouchButtonView _leftButton;
        [SerializeField] private TouchButtonView _rightButton;
        private int _accelerationRate = 0;

        public override void Init(SubscribeProperty<float> leftMove, SubscribeProperty<float> rightMove, float speed)
        {
            base.Init(leftMove, rightMove, speed);
        }

        private void OnEnable()
        {
            UpdateManager.SubscribeToUpdate(Move);
            _leftButton.ButtonDown += Deceleration;
            _leftButton.ButtonUp += Acceleration;
            _rightButton.ButtonDown += Acceleration;
            _rightButton.ButtonUp += Deceleration;
        }

        private void OnDisable()
        {
            UpdateManager.UnsubscribeFromUpdate(Move);
            _leftButton.ButtonDown -= Deceleration;
            _leftButton.ButtonUp -= Acceleration;
            _rightButton.ButtonDown -= Acceleration;
            _rightButton.ButtonUp -= Deceleration;
        }

        private void Deceleration(object sender, EventArgs e)
        {
            _accelerationRate--;
        }

        private void Acceleration(object sender, EventArgs e)
        {
            _accelerationRate++;
        }

        private void Move()
        {
            float movementDir = base._speed * Time.deltaTime * _accelerationRate;
            //Debug.Log(movementDir.ToString());

            if (movementDir > 0)
            {
                OnRightMove(movementDir);
            }
            else if (movementDir < 0)
            {
                OnLeftMove(movementDir);
            }
        }
    }
}
