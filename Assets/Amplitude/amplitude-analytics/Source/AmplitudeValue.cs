using System;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AmplitudeAnalytics
{
    public struct AmplitudeValue
    {
        private int _intValue;
        private float _floatValue;
        private string _stringValue;
        private int[] _arrayIntValue;
        private float[] _arrayFloatValue;
        private string[] _arrayStringValue;
        
        private AmplitudeValueType valueType;
        
        public AmplitudeValue(int intValue) : this()
        {
            _intValue = intValue;
            valueType = AmplitudeValueType.Int;
        }

        public AmplitudeValue(float floatValue) : this()
        {
            _floatValue = floatValue;
            valueType = AmplitudeValueType.Float;
        }
        
        public AmplitudeValue(string stringValue) : this()
        {
            _stringValue = stringValue;
            valueType = AmplitudeValueType.String;
        }
        
        public AmplitudeValue(int[] arrayIntValue) : this()
        {
            _arrayIntValue = arrayIntValue;
            valueType = AmplitudeValueType.IntArray;
        }

        public AmplitudeValue(float[] arrayFloatValue) : this()
        {
            _arrayFloatValue = arrayFloatValue;
            valueType = AmplitudeValueType.FloatArray;
        }

        public AmplitudeValue(string[] arrayStringValue) : this()
        {
            _arrayStringValue = arrayStringValue;
            valueType = AmplitudeValueType.StringArray;
        }

        public string ToJson()
        {
            var sb = new StringBuilder();
            
            switch (valueType)
            {
                case AmplitudeValueType.Int:
                    sb.Append(_intValue);
                    break;
                case AmplitudeValueType.Float:
                    sb.Append(_floatValue.ToString(CultureInfo.InvariantCulture));
                    break;
                case AmplitudeValueType.String:
                    sb.AppendStringToJson(_stringValue);
                    break;
                case AmplitudeValueType.IntArray:
                    sb.Append('[');
                    if (_arrayIntValue.Length > 0)
                        sb.Append(_arrayIntValue.First());
                    for (var i = 1; i < _arrayIntValue.Length; i++)
                    {
                        sb.Append(',');
                        sb.Append(_arrayIntValue[i]);
                    }
                    sb.Append(']');
                    break;
                case AmplitudeValueType.FloatArray:
                    sb.Append('[');
                    if (_arrayFloatValue.Length > 0)
                        sb.Append(_arrayFloatValue.First().ToString(CultureInfo.InvariantCulture));
                    for (var i = 1; i < _arrayFloatValue.Length; i++)
                    {
                        sb.Append(',');
                        sb.Append(_arrayFloatValue[i].ToString(CultureInfo.InvariantCulture));
                    }
                    sb.Append(']');
                    break;
                case AmplitudeValueType.StringArray:
                    sb.Append('[');
                    if (_arrayStringValue.Length > 0)
                        sb.AppendStringToJson(_arrayStringValue.First());
                    for (var i = 1; i < _arrayStringValue.Length; i++)
                    {
                        sb.Append(',');
                        sb.AppendStringToJson(_arrayStringValue[i]);
                    }
                    sb.Append(']');
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            return sb.ToString();
        }
    }
}