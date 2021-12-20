using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CarGame2D
{
    public class DragHandlerView : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        public event EventHandler DragLeft = delegate { };
        public event EventHandler DragRight = delegate { };
        public event EventHandler DragStop = delegate { };
        private enum DraggedDirection
        {
            Up,
            Down,
            Right,
            Left
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
            Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
            switch (GetDragDirection(dragVectorDirection))
            {
                case DraggedDirection.Left:
                    DragLeft.Invoke(this, null);
                    break;
                case DraggedDirection.Right:
                    DragRight.Invoke(this, null);
                    break;
                default:
                    DragStop.Invoke(this, null);
                    break;
            }
        }

        private DraggedDirection GetDragDirection(Vector3 dragVector)
        {
            float positiveX = Mathf.Abs(dragVector.x);
            float positiveY = Mathf.Abs(dragVector.y);
            DraggedDirection draggedDir;
            if (positiveX > positiveY)
            {
                draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
            }
            else
            {
                draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
            }
            Debug.Log(draggedDir);
            return draggedDir;
        }

        public void OnDrag(PointerEventData eventData)
        {
        }
    }
}
