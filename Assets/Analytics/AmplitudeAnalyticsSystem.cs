using System.Collections;
using System.Collections.Generic;
using AmplitudeAnalytics;
using UnityEngine;
using UnityEngine.Networking;

namespace VirtualConferences.CommonPlatform
{
    public class AmplitudeAnalyticsSystem : IAnalyticsSystem
    {
        public static readonly AnalyticsCustomEvents CustomEvents = new AnalyticsCustomEvents();
        private const string ApiKey = "9acc463a08edba6c99835c451b05a8f8";

        private RoutineWorker _routineWorker;

        private RoutineWorker RoutineWorker
        {
            get
            {
                if (_routineWorker is null)
                {
                    var go = new GameObject("RoutineWorker");
                    _routineWorker = go.AddComponent<RoutineWorker>();
                }

                return _routineWorker;
            }
        }

        public AmplitudeAnalyticsSystem(string userLogin)
        {
            Amplitude.Init(ApiKey, userLogin);
        }

        public void SendEvent(string customEventName)
        {
            var amplitudeEvent = AmplitudeEvent.Builder.EventType(customEventName);
            var operation = Amplitude.Send(amplitudeEvent);
            RoutineWorker.StartCoroutine(SendEventEnumerator(operation));
        }

        public void SendEvent(string customEventName, Dictionary<string, string> eventData)
        {
            var amplitudeEvent = AmplitudeEvent.Builder.EventType(customEventName);
            foreach (var data in eventData)
            {
                var propertyName = data.Key;
                var propertyValue = data.Value;
                amplitudeEvent.EventProperty(propertyName, propertyValue);
            }

            var operation = Amplitude.Send(amplitudeEvent);

            RoutineWorker.StartCoroutine(SendEventEnumerator(operation));
        }

        public void SendEvent(string customEventName, Dictionary<string, int> eventData)
        {
            var amplitudeEvent = AmplitudeEvent.Builder.EventType(customEventName);
            foreach (var data in eventData)
            {
                var propertyName = data.Key;
                var propertyValue = data.Value;
                amplitudeEvent.EventProperty(propertyName, propertyValue);
            }

            var operation = Amplitude.Send(amplitudeEvent);

            RoutineWorker.StartCoroutine(SendEventEnumerator(operation));
        }

        public void SendEvent(string customEventName, Dictionary<string, float> eventData)
        {
            var amplitudeEvent = AmplitudeEvent.Builder.EventType(customEventName);
            foreach (var data in eventData)
            {
                var propertyName = data.Key;
                var propertyValue = data.Value;
                amplitudeEvent.EventProperty(propertyName, propertyValue);
            }

            var operation = Amplitude.Send(amplitudeEvent);

            RoutineWorker.StartCoroutine(SendEventEnumerator(operation));
        }

        public void SendEvent(string customEventName, Dictionary<string, string[]> eventData)
        {
            var amplitudeEvent = AmplitudeEvent.Builder.EventType(customEventName);
            foreach (var data in eventData)
            {
                var propertyName = data.Key;
                var propertyValue = data.Value;
                amplitudeEvent.EventProperty(propertyName, propertyValue);
            }

            var operation = Amplitude.Send(amplitudeEvent);

            RoutineWorker.StartCoroutine(SendEventEnumerator(operation));
        }


        public void SendEvent(string customEventName, Dictionary<string, float[]> eventData)
        {
            var amplitudeEvent = AmplitudeEvent.Builder.EventType(customEventName);
            foreach (var data in eventData)
            {
                var propertyName = data.Key;
                var propertyValue = data.Value;
                amplitudeEvent.EventProperty(propertyName, propertyValue);
            }

            var operation = Amplitude.Send(amplitudeEvent);

            RoutineWorker.StartCoroutine(SendEventEnumerator(operation));
        }

        public void SendEvent(string customEventName, Dictionary<string, int[]> eventData)
        {
            var amplitudeEvent = AmplitudeEvent.Builder.EventType(customEventName);
            foreach (var data in eventData)
            {
                var propertyName = data.Key;
                var propertyValue = data.Value;
                amplitudeEvent.EventProperty(propertyName, propertyValue);
            }

            var operation = Amplitude.Send(amplitudeEvent);

            RoutineWorker.StartCoroutine(SendEventEnumerator(operation));
        }

        public void Disable() => Amplitude.Disable();

        private static IEnumerator SendEventEnumerator(UnityWebRequestAsyncOperation operation)
        {
            yield return operation;
            if (operation.webRequest.error is not null)
                Debug.LogError(operation.webRequest.error);
        }
    }
}