namespace GreenThumb.Managers
{
    public static class SessionManager
    {
        public static int? UserSessionId { get; set; }

        public static void Close()
        {
            UserSessionId = null;
        }

    }
}
