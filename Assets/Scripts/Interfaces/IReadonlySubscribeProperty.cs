using System;

namespace CarGame2D
{
    public interface IReadonlySubscribeProperty<T>
    {
        T Value { get; }

        void SubscribeOnChange(Action<T> subscriptionAction);
        void UnsubscribeOnChange(Action<T> unsubscriptionAction);
    }
}
