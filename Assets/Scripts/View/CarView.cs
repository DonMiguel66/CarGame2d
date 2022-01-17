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
        public Transform AcceleratorPosition { get => _acceleratorPosition;}

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

    }
}
