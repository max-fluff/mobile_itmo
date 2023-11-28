namespace VirtualConferences.CommonPlatform
{
    public class AnalyticsCustomEvents
    {
        // Authorization
        public const string Authorization = nameof(Authorization);

        // Menu
        public const string UserChangedNickname = nameof(UserChangedNickname);

        // Application Events
        public const string ApplicationQuit = nameof(ApplicationQuit);
        public const string CreateRoom = nameof(CreateRoom);
        public const string JoinRoom = nameof(JoinRoom);
        public const string LeaveRoom = nameof(LeaveRoom);
        public const string FPS = nameof(FPS);
    }
}