using UnityEngine;

namespace CarGame2D
{
    public class AbilityView : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody2D;

        public Rigidbody2D Rigidbody2D => _rigidbody2D ? _rigidbody2D : GetComponent<Rigidbody2D>();
    }
}
