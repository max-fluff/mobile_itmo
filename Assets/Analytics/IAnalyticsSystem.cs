using System.Collections.Generic;

namespace VirtualConferences.CommonPlatform
{
    public interface IAnalyticsSystem
    {
        void SendEvent(string customEventName);
        void SendEvent(string customEventName, Dictionary<string, string> eventData);
        void SendEvent(string customEventName, Dictionary<string, int> eventData);
        void SendEvent(string customEventName, Dictionary<string, float> eventData);
        void SendEvent(string customEventName, Dictionary<string, string[]> eventData);
        void SendEvent(string customEventName, Dictionary<string, int[]> eventData);
        void SendEvent(string customEventName, Dictionary<string, float[]> eventData);
        void Disable();
    }
}