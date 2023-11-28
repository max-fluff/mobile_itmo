using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace VirtualConferences.CommonPlatform
{
    public static class AnalyticsHelper
    {
        private static IAnalyticsSystem _analyticsSystem;

        public static DateTime AuthorizationTime;

        public static void SetAnalyticsSystem(IAnalyticsSystem analyticsSystem)
            => _analyticsSystem = analyticsSystem;
        

        public static void SendEvent(string customEventName) => _analyticsSystem.SendEvent(customEventName);

        public static void SendEvent(string customEventName, Dictionary<string, int> eventData)
            => _analyticsSystem.SendEvent(customEventName, eventData);

        public static void SendEvent(string customEventName, Dictionary<string, string> eventData)
            => _analyticsSystem.SendEvent(customEventName, eventData);

        public static void SendEvent(string customEventName, Dictionary<string, float> eventData)
            => _analyticsSystem.SendEvent(customEventName, eventData);

        public static void SendEvent(string customEventName, Dictionary<string, int[]> eventData)
            => _analyticsSystem.SendEvent(customEventName, eventData);

        public static void SendEvent(string customEventName, Dictionary<string, string[]> eventData)
            => _analyticsSystem.SendEvent(customEventName, eventData);

        public static void SendEvent(string customEventName, Dictionary<string, float[]> eventData)
            => _analyticsSystem.SendEvent(customEventName, eventData);

        public static void Initialize(IAnalyticsSystem analyticsSystem, string userLogin)
        {
            if (_analyticsSystem is null)
                SetAnalyticsSystem(analyticsSystem);
        }
    }
}