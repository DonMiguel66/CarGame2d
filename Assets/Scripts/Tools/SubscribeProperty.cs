using System;

namespace CarGame2D
{
    public class SubscribeProperty<T> : IReadonlySubscribeProperty<T>
    {
        private T _value;

        private Action<T> _onChangeValue;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                _onChangeValue?.Invoke(_value);
            }
        }

        public void SubscribeOnChange(Action<T> subscriptionAction)
        {
            _onChangeValue += subscriptionAction;
        }

        public void UnsubscribeOnChange(Action<T> unsubscriptionAction)
        {
            _onChangeValue -= unsubscriptionAction;
        }
    }
}
