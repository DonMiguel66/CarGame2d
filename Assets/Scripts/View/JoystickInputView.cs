using JoostenProductions;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace CarGame2D
{
    public class JoystickInputView : BasicInputView
    {
        public override void Init(SubscribeProperty<float> leftMove, SubscribeProperty<float> rightMove, float speed)
        {
            base.Init(leftMove, rightMove, speed);
            UpdateManager.SubscribeToUpdate(Move);
        }

        private void OnDestroy()
        {
            UpdateManager.UnsubscribeFromUpdate(Move);
        }

        private void Move()
        {
            float moveStep = 10 * Time.deltaTime * CrossPlatformInputManager.GetAxis("Horizontal");
            if (moveStep > 0)
                OnRightMove(moveStep);
            else if (moveStep < 0)
                OnLeftMove(moveStep);
        }

    }
}
