using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmplitudeAnalytics
{
    public struct AmplitudeEvent
    {
        public static AmplitudeEventBuilder Builder => new AmplitudeEventBuilder();

        private string _eventType;
        private List<AmplitudeProperty> _userProperties;
        private List<AmplitudeProperty> _eventProperties;

        public AmplitudeEvent(string eventType, List<AmplitudeProperty> userProperties,
            List<AmplitudeProperty> eventProperties)
        {
            _eventType = eventType;
            _userProperties = userProperties;
            _eventProperties = eventProperties;
        }

        internal void ToJson(StringBuilder sb, List<AmplitudeProperty> extraUserProperties)
        {
            sb.AppendEventType(_eventType);
            sb.Append(',');

            if (_eventProperties != null && _eventProperties.Count > 0)
            {
                sb.AppendStringToJson("event_properties");
                sb.Append(':');
                sb.Append('{');
                sb.AppendPair(_eventProperties.First());

                for (var i = 1; i < _eventProperties.Count; i++)
                {
                    sb.Append(',');
                    sb.AppendPair(_eventProperties[i]);
                }

                sb.Append('}');
            }

            var hasUserProperties = _userProperties != null && _userProperties.Count > 0;
            var hasExtraUserProperties = extraUserProperties != null && extraUserProperties.Count > 0;
            var hasAnyUserProperties = hasUserProperties || hasExtraUserProperties;
            
            if (hasAnyUserProperties)
            {
                sb.Append(',');
                sb.AppendStringToJson("user_properties");
                sb.Append(':');
                sb.Append('{');
            }

            if (hasUserProperties)
            {
                sb.AppendPair(_userProperties[0]);

                for (var i = 1; i < _userProperties.Count; i++)
                {
                    sb.Append(',');
                    sb.AppendPair(_userProperties[i]);
                }
            }
            
            if (hasExtraUserProperties)
            {
                sb.AppendPair(extraUserProperties[0]);

                for (var i = 1; i < extraUserProperties.Count; i++)
                {
                    sb.Append(',');
                    sb.AppendPair(extraUserProperties[i]);
                }
            }

            if (hasAnyUserProperties)
            {
                sb.Append('}');
            }
        }
    }
}