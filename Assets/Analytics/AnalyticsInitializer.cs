using System;
using UnityEngine;

namespace VirtualConferences.CommonPlatform
{
    public class AnalyticsInitializer : MonoBehaviour
    {
        private static bool _isAuthorized;

        private void Awake()
        {
            var login = PlayerPrefs.GetString("UserID", Guid.NewGuid().ToString());
            PlayerPrefs.SetString("UserID", login);

            var amplitudeAnalyticsSystem = new AmplitudeAnalyticsSystem(login);
            AnalyticsHelper.Initialize(amplitudeAnalyticsSystem, login);

            if (_isAuthorized)
                return;

            _isAuthorized = true;
            AnalyticsEventSender.Authorization(DateTime.Now);
        }
    }
}