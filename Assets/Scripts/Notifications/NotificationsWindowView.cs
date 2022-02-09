using UnityEngine;
using UnityEngine.UI;
using Unity.Notifications.Android;
using Unity.Notifications.iOS;
using System;

namespace CarGame2D
{
    public class NotificationsWindowView : MonoBehaviour
    {
        private const string AndroidChannelId = "android_id";

        [SerializeField]
        private Button _showNotificationButton;

        private void Awake()
        {
#if UNITY_ANDROID
            AndroidChannelRegister();
#elif UNITY_IOS

#endif
        }

        private void Start()
        {
            _showNotificationButton.onClick.AddListener(CreateNotification);
        }

        private void CreateNotification()
        {
#if UNITY_ANDROID
            NotificationAndroid();
#elif UNITY_IOS
            NotificationIOS();
#endif
        }

        private void AndroidChannelRegister()
        {
            var androidSettingsChannel = new AndroidNotificationChannel
            {
                Id = AndroidChannelId,
                Name = "Notification",
                Description = "Description",
                CanBypassDnd = true,
                EnableVibration = true
            };

            AndroidNotificationCenter.RegisterNotificationChannel(androidSettingsChannel);
        }

        private void NotificationAndroid()
        {
            var androidNotification = new AndroidNotification
            {
                Color = Color.white,
                RepeatInterval = TimeSpan.FromSeconds(100),
                FireTime = DateTime.Now + TimeSpan.FromSeconds(100),
            };

            AndroidNotificationCenter.SendNotification(androidNotification, AndroidChannelId);
        }
        private void NotificationIOS()
        {
            var currentDate = DateTime.UtcNow.ToString();
            var iosNotification = new iOSNotification
            {
                Identifier = "ios_id",
                Title = "Title",
                Body = "Body",
                ForegroundPresentationOption = PresentationOption.Sound | PresentationOption.Alert,
                Data = currentDate
            };

            iOSNotificationCenter.ScheduleNotification(iosNotification);
        }
        private void OnDestroy()
        {
            _showNotificationButton.onClick.RemoveAllListeners();
        }
    }
}
