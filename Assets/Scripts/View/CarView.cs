using UnityEngine;

namespace CarGame2D
{    
    public class CarView : MonoBehaviour
    {
        public GameObject Body { get => _body; set => _body = value; }
        public GameObject ForwardWheel { get => _forwardWheel; set => _forwardWheel = value; }
        public GameObject BackWheel { get => _backWheel; set => _backWheel = value; }
        public GameObject Accelerator { get => _accelerator; set => _accelerator = value; }
        public Transform BodyPosition { get => _bodyPosition;}
        public Transform ForwardWheelPosition { get => _forwardWheelPosition; }
        public Transform BackWheelPosition { get => _backWheelPosition; }

        private GameObject _body;
        private GameObject _forwardWheel;
        private GameObject _backWheel;
        private GameObject _accelerator;

        [SerializeField]
        private Transform _bodyPosition;
        [SerializeField]
        private Transform _forwardWheelPosition;
        [SerializeField]
        private Transform _backWheelPosition;
        [SerializeField]
        private Transform _acceleratorPosition;

        public CarView(GameObject body, GameObject forwardWheel, GameObject backWheel)
        {
            _body = body;
            _forwardWheel = forwardWheel;
            _backWheel = backWheel;
        }

        public void SetCarPart(CarPartType partType, GameObject partGO)
        {
            switch (partType)
            {
                case CarPartType.Body:
                    if (Body != null)
                        RemoveCarPart(CarPartType.Body);
                    Body = Object.Instantiate(partGO);
                    Body.transform.position = _bodyPosition.position;
                    Body.transform.SetParent(_bodyPosition);
                    break;
                case CarPartType.Wheel:
                    if (Body != null)
                        RemoveCarPart(CarPartType.Wheel);
                    BackWheel = Object.Instantiate(partGO);
                    BackWheel.transform.position = _backWheelPosition.position;
                    BackWheel.transform.SetParent(_backWheelPosition);

                    ForwardWheel = Object.Instantiate(partGO);
                    ForwardWheel.transform.position = _forwardWheelPosition.position;
                    ForwardWheel.transform.SetParent(_forwardWheelPosition);                   
                    break;
                case CarPartType.Accelerator:
                    if (Body != null)
                        RemoveCarPart(CarPartType.Accelerator);
                    Accelerator = Object.Instantiate(partGO);
                    Accelerator.transform.position = _acceleratorPosition.position;
                    Accelerator.transform.SetParent(_acceleratorPosition);
                    break;
            }
        }

        public void RemoveCarPart(CarPartType partType)
        {
            //Пока что реализовал по простому, далее думаю оптимизировать, что бы объекты уходили в пул, а не удалялись
            switch (partType)
            {
                case CarPartType.Body:
                    if (Body != null)
                        Destroy(Body);
                    break;
                case CarPartType.Wheel:
                    if (BackWheel != null && ForwardWheel != null)
                    {
                        Destroy(ForwardWheel);
                        Destroy(BackWheel);
                    }
                    break;
                case CarPartType.Accelerator:
                    if (Accelerator != null)
                        Destroy(Accelerator);
                    break;
            }
        }
    }
}
