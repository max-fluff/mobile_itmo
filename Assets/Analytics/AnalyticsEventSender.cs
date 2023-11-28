using System;
using System.Collections.Generic;
using UnityEngine;

namespace VirtualConferences.CommonPlatform
{
    public static class AnalyticsEventSender
    {
        // Authorization
        public static void Authorization(DateTime authorizationTime)
        {
            var deviceType = SystemInfo.deviceType;
            var deviceModel = SystemInfo.deviceModel;
            var operatingSystem = SystemInfo.operatingSystem;
            var processorType = SystemInfo.processorType;
            var graphicsDeviceName = SystemInfo.graphicsDeviceName;
            var systemMemorySize = SystemInfo.systemMemorySize;

            AnalyticsHelper.SendEvent(AnalyticsCustomEvents.Authorization, new Dictionary<string, string>
            {
                { "AuthorizationTime", authorizationTime.ToString("dd.MM.yyyy HH:mm") },
                { "DeviceType", $"{deviceType}" },
                { "DeviceModel", deviceModel },
                { "OperatingSystem", operatingSystem },
                { "ProcessorType", processorType },
                { "GraphicsDeviceName", graphicsDeviceName },
                { "RAM", $"{systemMemorySize}" },
            });

            AnalyticsHelper.AuthorizationTime = authorizationTime;
        }

        // Application Events
        public static void CreateRoom(string roomId)
        {
            AnalyticsHelper.SendEvent(AnalyticsCustomEvents.CreateRoom, new Dictionary<string, string>
            {
                { "RoomId", roomId }
            });
        }

        public static void JoinRoom(DateTime joinTime, string roomId)
        {
            AnalyticsHelper.SendEvent(AnalyticsCustomEvents.JoinRoom, new Dictionary<string, string>
            {
                { "JoinTime", joinTime.ToString("dd.MM.yyyy HH:mm") },
                { "RoomId", roomId }
            });
        }

        public static void LeaveRoom(DateTime joinTime, string roomId)
        {
            AnalyticsHelper.SendEvent(AnalyticsCustomEvents.LeaveRoom, new Dictionary<string, string>
            {
                { "LeaveTime", joinTime.ToString("dd.MM.yyyy HH:mm") },
                { "RoomId", roomId }
            });
        }

        public static void QuitApplication(DateTime quitTime)
        {
            var sessionDuration = AnalyticsHelper.AuthorizationTime - quitTime;

            AnalyticsHelper.SendEvent(AnalyticsCustomEvents.ApplicationQuit, new Dictionary<string, string>
            {
                { "ApplicationSessionDuration", sessionDuration.ToString("g") }
            });
        }

        public static void FPS(int averageFPS)
        {
            AnalyticsHelper.SendEvent(AnalyticsCustomEvents.FPS, new Dictionary<string, int>
            {
                { "FPS", averageFPS }
            });
        }
    }
}