using System;
using JoostenProductions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CarGame2D
{
    public class SwipeInputView : BasicInputView
    {
        [SerializeField] private DragHandlerView _dragHandlerView;
        private int _accelerationRate = 0;

        public override void Init(SubscribeProperty<float> leftMove, SubscribeProperty<float> rightMove, float speed)
        {
            base.Init(leftMove, rightMove, speed);
        }

        private void OnEnable()
        {
            UpdateManager.SubscribeToUpdate(Move);
            _dragHandlerView.DragLeft += SetMoveLeft;
            _dragHandlerView.DragRight += SetMoveRight;
            _dragHandlerView.DragStop += SetMoveStop;
        }

        private void OnDisable()
        {
            UpdateManager.UnsubscribeFromUpdate(Move);
            _dragHandlerView.DragLeft -= SetMoveLeft;
            _dragHandlerView.DragRight -= SetMoveRight;
            _dragHandlerView.DragStop -= SetMoveStop;
        }

        private void SetMoveLeft(object serder, EventArgs e) => _accelerationRate = -1;
        private void SetMoveRight(object serder, EventArgs e) => _accelerationRate = 1;
        private void SetMoveStop(object serder, EventArgs e) => _accelerationRate = 0;

        private void Move()
        {
            float movementDir = _speed * Time.deltaTime * _accelerationRate;
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
