using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace AmplitudeAnalytics
{
    public static class Amplitude
    {
        private static UnityWebRequestAsyncOperation NoneAsyncOperation = new UnityWebRequestAsyncOperation();
        public static readonly string Url = "https://api2.amplitude.com/2/httpapi";

        private static string _apiKey;
        private static string _userId;
        private static string _platform;
        private static string _version;

        private static bool _isFirstError = true;

        public static readonly List<AmplitudeProperty> DefaultUserProperties = new List<AmplitudeProperty>();
        private static readonly string SessionIdString = ((ulong)DateTime.UtcNow.Ticks).ToString();

        public static bool IsEnabled { get; private set; }

        public static void Disable() => IsEnabled = false;

        public static void Init(string apiKey, string userId, string platform, string version)
        {
            _apiKey = apiKey;
            _userId = userId;
            _platform = platform;
            _version = version;
            IsEnabled = true;
            _isFirstError = true;
        }

        public static void Init(string apiKey, string userId)
            => Init(apiKey, userId, Application.platform.ToString(), Application.version);

        public static UnityWebRequestAsyncOperation Send(AmplitudeEvent amplitudeEvent)
        {
            if (!IsEnabled)
            {
                if (_isFirstError)
                    Debug.LogError("Amplitude analytics is not initialized.");

                _isFirstError = false;
                return NoneAsyncOperation;
            }

            var json = CreateJson(amplitudeEvent);
            var raw = Encoding.UTF8.GetBytes(json);

            var request = new UnityWebRequest(Url, "POST")
            {
                uploadHandler = new UploadHandlerRaw(raw),
                downloadHandler = new DownloadHandlerBuffer(),
                disposeDownloadHandlerOnDispose = true,
                disposeUploadHandlerOnDispose = true,
            };

            request.SetRequestHeader("Content-Type", "application/json");

            var asyncOperation = request.SendWebRequest();
            asyncOperation.completed += _ => request.Dispose();

            return asyncOperation;
        }

        private static string CreateJson(AmplitudeEvent amplitudeEvent)
        {
            var sb = new StringBuilder(320);

            // @formatter:off
            sb.Append('{');
                sb.AppendApiKey(_apiKey).Append(',');
                sb.Append("\"events\":");
                sb.Append('{');
                    sb.AppendUserId(_userId).Append(',');
                    sb.AppendVersion(_version).Append(',');
                    sb.AppendPlatform(_platform).Append(',');
                    sb.AppendJsonProperty("session_id", SessionIdString).Append(',');
                    
                    amplitudeEvent.ToJson(sb, DefaultUserProperties);

                sb.Append('}');
            sb.Append('}');
            // @formatter:on

            return sb.ToString();
        }
    }
}