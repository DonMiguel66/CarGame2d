using JoostenProductions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarGame2D
{
    public class GyroscopeInputView : BasicInputView
    {
        public override void Init(SubscribeProperty<float> leftMove, SubscribeProperty<float> rightMove, float speed)
        {
            base.Init(leftMove, rightMove, speed);
            Input.gyro.enabled = true;
            UpdateManager.SubscribeToUpdate(Move);
        }

        private void OnDestroy()
        {
            UpdateManager.UnsubscribeFromUpdate(Move);
        }

        private void Move()
        {
            if (!SystemInfo.supportsGyroscope)
                return;
            Quaternion quaternion = Input.gyro.attitude;
            quaternion.Normalize();
            OnRightMove((quaternion.x + quaternion.y) * Time.deltaTime * _speed);
        }
    }
}
