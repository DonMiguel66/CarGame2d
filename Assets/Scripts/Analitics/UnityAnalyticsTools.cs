using System.Collections.Generic;
using UnityEngine.Analytics;

namespace CarGame2D
{
    public class UnityAnaliticsTools : IAnalyticsTools
    {
        public void SendMessage(string eventName)
        {
            Analytics.CustomEvent(eventName);
        }

        public void SendMessage(string eventName, (string key, object value) data)
        {
            var eventData = new Dictionary<string, object> { [data.key] = data.value };
            Analytics.CustomEvent(eventName, eventData);
        }
    }
}
