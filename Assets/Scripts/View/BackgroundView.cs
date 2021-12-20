using UnityEngine;

namespace CarGame2D
{
    public class BackgroundView : MonoBehaviour
    {

        [SerializeField]
        private Background[] _backgrounds;

        private IReadonlySubscribeProperty<float> _diff;

        public void Init(IReadonlySubscribeProperty<float> diff)
        {
            _diff = diff;
            _diff.SubscribeOnChange(Move);
        }

        protected void OnDestroy()
        {
            _diff?.SubscribeOnChange(Move);
        }

        private void Move(float value)
        {
            foreach (var background in _backgrounds)
                background.Move(-value);
        }
    }
}
