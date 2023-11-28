namespace AmplitudeAnalytics
{
    public readonly struct AmplitudeProperty
    {
        public readonly string Name;
        public readonly AmplitudeValue Value;

        public AmplitudeProperty(string name, AmplitudeValue value)
        {
            Name = name;
            Value = value;
        }

        public AmplitudeProperty(string name, int intValue)
            : this(name, new AmplitudeValue(intValue))
        {
        }

        public AmplitudeProperty(string name, float floatValue)
            : this(name, new AmplitudeValue(floatValue))
        {
        }


        public AmplitudeProperty(string name, string stringValue)
            : this(name, new AmplitudeValue(stringValue))
        {
        }


        public AmplitudeProperty(string name, int[] arrayIntValue)
            : this(name, new AmplitudeValue(arrayIntValue))
        {
        }


        public AmplitudeProperty(string name, float[] arrayFloatValue)
            : this(name, new AmplitudeValue(arrayFloatValue))
        {
        }


        public AmplitudeProperty(string name, string[] arrayStringValue)
            : this(name, new AmplitudeValue(arrayStringValue))
        {
        }

    }
}