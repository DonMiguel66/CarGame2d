using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

namespace CarGame2D
{
    public class CustomButton : Button
    {
        public static string ChangeButtonType => nameof(_animationButtonType);
        public static string Duration => nameof(_duration);
        public static string PauseTime => nameof(_pauseTime);
        public static string PunchDuration => nameof(_punchDuration);
        public static string PunchVibrato => nameof(_punchVibrato);
        public static string PunchElasticity => nameof(_punchElasticity);
        public static string PunchScale => nameof(_punchScale);
        public static string Strength => nameof(_strength);


        [SerializeField]
        private AnimationButtonType _animationButtonType = AnimationButtonType.Bouncing;
        [SerializeField]
        private float _duration = 0.1f;
        [SerializeField]
        private float _strength = 30f;
        [SerializeField]
        private float _pauseTime = 0.1f;
        [SerializeField]
        private Vector3 _punchScale = new Vector3(.7f, .7f, 0);
        [SerializeField]
        private float _punchDuration = 0.811f;
        [SerializeField]
        private int _punchVibrato = 4;
        [SerializeField]
        private float _punchElasticity = 1.0f;

        private RectTransform _rectTransform;

        private bool _inAnimation;
        protected override void Awake()
        {
            base.Awake();

            _rectTransform = GetComponent<RectTransform>();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            StartAnimation();
        }

        private void StartAnimation()
        {
            if (_inAnimation)
                return;

            switch(_animationButtonType)
            {
                case AnimationButtonType.Bouncing:
                    DoBouncing(_rectTransform);
                    break;
                case AnimationButtonType.Shaking:
                    DoShaking(_rectTransform);
                    break;
            }
        }
        
        private void DoBouncing(RectTransform rectTransform)
        {            
            _inAnimation = true;
            Vector3 startScale = rectTransform.localScale;
            Sequence bounceSeq = DOTween.Sequence();

            bounceSeq.Append(rectTransform.DOScale(startScale, _duration));
            bounceSeq.Insert(_pauseTime, rectTransform.DOPunchScale(_punchScale, _punchDuration, _punchVibrato, _punchElasticity));
            bounceSeq.InsertCallback(_pauseTime + _punchDuration, () => _inAnimation = false);

            
        }

        private void DoShaking(RectTransform rectTransform)
        {
            _inAnimation = true;
            var anim = _rectTransform.DOShakePosition(_duration, _strength, _punchVibrato);
            anim.OnComplete(() => _inAnimation = false);
        }
    }
}
