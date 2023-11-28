using System.Collections.Generic;

namespace AmplitudeAnalytics
{
    public class AmplitudeEventBuilder
    {
        private string _eventType;
        private List<AmplitudeProperty> _userProperties = new List<AmplitudeProperty>();
        private List<AmplitudeProperty> _eventProperties = new List<AmplitudeProperty>();

        public AmplitudeEventBuilder EventType(string eventType)
        {
            _eventType = eventType;
            return this;
        }

        public AmplitudeEvent Build() => new AmplitudeEvent(_eventType, _userProperties, _eventProperties);

        #region Event properties methods
        public AmplitudeEventBuilder EventProperty(string propertyName, string propertyValue)
        {
            var value = new AmplitudeValue(propertyValue);
            return EventProperty(propertyName, value);
        }
        
        public AmplitudeEventBuilder EventProperty(string propertyName, int propertyValue)
        {
            var value = new AmplitudeValue(propertyValue);
            return EventProperty(propertyName, value);
        }

        public AmplitudeEventBuilder EventProperty(string propertyName, float propertyValue)
        {
            var value = new AmplitudeValue(propertyValue);
            return EventProperty(propertyName, value);
        }
        
        public AmplitudeEventBuilder EventProperty(string propertyName, string[] propertyValue)
        {
            var value = new AmplitudeValue(propertyValue);
            return EventProperty(propertyName, value);
        }
        
        public AmplitudeEventBuilder EventProperty(string propertyName, float[] propertyValue)
        {
            var value = new AmplitudeValue(propertyValue);
            return EventProperty(propertyName, value);
        }
        
        public AmplitudeEventBuilder EventProperty(string propertyName, int[] propertyValue)
        {
            var value = new AmplitudeValue(propertyValue);
            return EventProperty(propertyName, value);
        }

        private AmplitudeEventBuilder EventProperty(string propertyName, AmplitudeValue propertyValue)
        {
            _eventProperties.Add(new AmplitudeProperty(propertyName, propertyValue));
            return this;
        }
        #endregion

        #region User properties methods
        public AmplitudeEventBuilder UserProperty(string propertyName, string propertyValue)
        {
            var value = new AmplitudeValue(propertyValue);
            return UserProperty(propertyName, value);
        }
        
        public AmplitudeEventBuilder UserProperty(string propertyName, int propertyValue)
        {
            var value = new AmplitudeValue(propertyValue);
            return UserProperty(propertyName, value);
        }

        public AmplitudeEventBuilder UserProperty(string propertyName, float propertyValue)
        {
            var value = new AmplitudeValue(propertyValue);
            return UserProperty(propertyName, value);
        }
        
        public AmplitudeEventBuilder UserProperty(string propertyName, string[] propertyValue)
        {
            var value = new AmplitudeValue(propertyValue);
            return UserProperty(propertyName, value);
        }
        
        public AmplitudeEventBuilder UserProperty(string propertyName, int[] propertyValue)
        {
            var value = new AmplitudeValue(propertyValue);
            return UserProperty(propertyName, value);
        }
        
        public AmplitudeEventBuilder UserProperty(string propertyName, float[] propertyValue)
        {
            var value = new AmplitudeValue(propertyValue);
            return UserProperty(propertyName, value);
        }
        
        private AmplitudeEventBuilder UserProperty(string propertyName, AmplitudeValue propertyValue)
        {
            _userProperties.Add(new AmplitudeProperty(propertyName, propertyValue));
            return this;
        }
        #endregion
        
        public static implicit operator AmplitudeEvent(AmplitudeEventBuilder builder) => builder.Build();
    }
}