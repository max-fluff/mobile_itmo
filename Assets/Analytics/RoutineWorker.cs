using System;
using UnityEngine;

namespace VirtualConferences.CommonPlatform
{
    public class RoutineWorker : MonoBehaviour
    {
        private void Awake()
            => DontDestroyOnLoad(gameObject);

        private void OnApplicationQuit() 
            => AnalyticsEventSender.QuitApplication(DateTime.Now);
    }
}