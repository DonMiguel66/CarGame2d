using UnityEngine;
using UnityEngine.UI;

namespace CarGame2D
{
    public class StartFightView : MonoBehaviour
    {
        [SerializeField]
        private Button _startFightButton;

        public Button StartFightButton  => _startFightButton;
    }
}
