using ShiroBot.Model.Common;
using ShiroBot.SDK.Adapter;

namespace ShiroBot.DemoAdapter.Services;

public sealed class EventService : IEventService
{
#pragma warning disable CS0067 // Template events are raised only after a platform event source is connected.
    public event Func<GroupIncomingMessage, Task>? GroupMessageReceived;
    public event Func<FriendIncomingMessage, Task>? FriendMessageReceived;
    public event Func<MessageRecallEvent, Task>? MessageRecall;
    public event Func<FriendRequestEvent, Task>? FriendRequest;
    public event Func<GroupJoinRequestEvent, Task>? GroupJoinRequest;
    public event Func<GroupInvitedJoinRequestEvent, Task>? GroupInvitedJoinRequest;
    public event Func<GroupInvitationEvent, Task>? GroupInvitation;
    public event Func<FriendNudgeEvent, Task>? FriendNudge;
    public event Func<FriendFileUploadEvent, Task>? FriendFileUpload;
    public event Func<GroupAdminChangeEvent, Task>? GroupAdminChange;
    public event Func<GroupEssenceMessageChangeEvent, Task>? GroupEssenceMessageChange;
    public event Func<GroupMemberIncreaseEvent, Task>? GroupMemberIncrease;
    public event Func<GroupMemberDecreaseEvent, Task>? GroupMemberDecrease;
    public event Func<GroupNameChangeEvent, Task>? GroupNameChange;
    public event Func<GroupMessageReactionEvent, Task>? GroupMessageReaction;
    public event Func<GroupMuteEvent, Task>? GroupMute;
    public event Func<GroupWholeMuteEvent, Task>? GroupWholeMute;
    public event Func<GroupNudgeEvent, Task>? GroupNudge;
    public event Func<GroupFileUploadEvent, Task>? GroupFileUpload;
    public event Func<PeerPinChangeEvent, Task>? PeerPinChange;
    public event Func<GroupDisbandEvent, Task>? GroupDisband;
    public event Func<BotOfflineEvent, Task>? BotOffline;
#pragma warning restore CS0067

    internal async Task PublishDemoFriendNudgeAsync(long selfId, long userId)
    {
        var handlers = FriendNudge;
        if (handlers is null)
        {
            return;
        }

        var demoEvent = new FriendNudgeEvent(
            DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            selfId,
            userId,
            IsSelfSend: false,
            IsSelfReceive: true,
            DisplayAction: "戳了戳",
            DisplaySuffix: "（DemoAdapter 演示事件）",
            DisplayActionImgUrl: string.Empty);

        foreach (Func<FriendNudgeEvent, Task> handler in handlers.GetInvocationList())
        {
            await handler(demoEvent);
        }
    }
}
