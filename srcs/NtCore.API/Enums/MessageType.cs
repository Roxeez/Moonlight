namespace NtCore.API.Enums
{
    /// <summary>
    ///     Represent all kind of Message (not Chat related)
    /// </summary>
    public enum MessageType : byte
    {
        MIDDLE_SCREEN = 0,
        BOTTOM_CARD = 1,
        MIDDLE_SCREEN_YELLOW = 2,
        MIDDLE_SCREEN_SMALL = 3,
        BOTTOM_RED = 5,
        MIDDLE_SCREEN_AND_BOTTOM_CARD = 6
    }
}