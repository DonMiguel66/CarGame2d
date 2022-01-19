using UnityEngine;

namespace CarGame2D
{
    public class AbilityView : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

        public Rigidbody2D Rigidbody2D { get => _rigidbody2D; set => gameObject.GetComponent<Rigidbody2D>(); }
    }
}
