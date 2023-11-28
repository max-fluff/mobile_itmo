using System.Text;

namespace AmplitudeAnalytics
{
    internal static class StringBuilderExtension
    {
        public static StringBuilder AppendJsonProperty(this StringBuilder sb, string key, string value)
        {
            sb.AppendStringToJson(key);
            sb.Append(':');
            sb.AppendStringToJson(value);
            return sb;
        }
        
        public static StringBuilder AppendPair(this StringBuilder sb, AmplitudeProperty property)
        {
            sb.Append('"');
            sb.Append(property.Name);
            sb.Append("\":");
            sb.Append(property.Value.ToJson());
            return sb;
        }
        
        public static StringBuilder AppendPair(this StringBuilder sb, (string, AmplitudeValue) pair)
        {
            var (key, value) = pair;
            sb.Append('"');
            sb.Append(key);
            sb.Append("\":");
            sb.Append(value.ToJson());
            return sb;
        }

        public static StringBuilder AppendStringToJson(this StringBuilder sb, string str)
        {
            sb.Append('"');
            sb.Append(str);
            sb.Append('"');
            return sb;
        }
        
        public static StringBuilder AppendApiKey(this StringBuilder sb, string apiKey) 
            => sb.AppendJsonProperty("api_key", apiKey);

        public static StringBuilder AppendUserId(this StringBuilder sb, string userId)
            => sb.AppendJsonProperty("user_id", userId);

        public static StringBuilder AppendVersion(this StringBuilder sb, string version) 
            => sb.AppendJsonProperty("app_version", version);

        public static StringBuilder AppendPlatform(this StringBuilder sb, string platform) 
            => sb.AppendJsonProperty("platform", platform);
        
        public static StringBuilder AppendEventType(this StringBuilder sb, string eventType) 
            => sb.AppendJsonProperty("event_type", eventType);
    }
}