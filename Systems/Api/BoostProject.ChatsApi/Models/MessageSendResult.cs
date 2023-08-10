namespace BoostProject.ChatsApi.Models;

/// <summary>
/// Default is true.
/// <para></para>
/// You can directly specify that message is not sent by calling constructor with 1 false parameter without error.
/// <para></para>
/// You can call constructor with error, by that <see cref="MessageSent"/> will be set false automatically
/// </summary>
public struct MessageSendResult
{
    #region Constructors
    public MessageSendResult() => MessageSent = true;

    public MessageSendResult(bool IsMessageSent) 
    { 
        MessageSent = IsMessageSent; 
    }

    public MessageSendResult(string error)
    {
        MessageSent = false;
        Error = error;
    }
    #endregion
    public bool MessageSent { get; set; }
    public string Error { get; set; }
}
