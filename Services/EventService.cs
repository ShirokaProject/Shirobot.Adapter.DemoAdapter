using ShiroBot.Model.Common;
using ShiroBot.SDK.Adapter;

namespace ShiroBot.DemoAdapter.Services;

public sealed class EventService : IEventService
{
    public event Func<Event, Task>? EventReceived;

    internal async Task PublishDemoFriendNudgeAsync(long selfId, long userId)
    {
        var demoEvent = new FriendNudgeEvent(
            DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            selfId,
            userId,
            IsSelfSend: false,
            IsSelfReceive: true,
            DisplayAction: "戳了戳",
            DisplaySuffix: "（DemoAdapter 演示事件）",
            DisplayActionImgUrl: string.Empty);

        await PublishAsync(demoEvent);
    }

    public async Task PublishAsync(Event evt)
    {
        var handlers = EventReceived;
        if (handlers is null) return;

        foreach (Func<Event, Task> handler in handlers.GetInvocationList())
        {
            await handler(evt);
        }
    }
}
