using ShiroBot.Model.Common;
using ShiroBot.Model.Message.Requests;
using ShiroBot.Model.Message.Responses;
using ShiroBot.SDK.Adapter;

namespace ShiroBot.DemoAdapter.Services;

public sealed class MessageService : IMessageService
{
    public Task<SendPrivateMessageResponse> SendPrivateMessageAsync(SendPrivateMessageRequest request)
        => throw Unsupported(nameof(SendPrivateMessageAsync));

    public Task<SendGroupMessageResponse> SendGroupMessageAsync(SendGroupMessageRequest request)
        => throw Unsupported(nameof(SendGroupMessageAsync));

    public Task RecallPrivateMessageAsync(RecallPrivateMessageRequest request)
        => throw Unsupported(nameof(RecallPrivateMessageAsync));

    public Task RecallGroupMessageAsync(RecallGroupMessageRequest request)
        => throw Unsupported(nameof(RecallGroupMessageAsync));

    public Task<GetMessageResponse> GetMessageAsync(GetMessageRequest request)
        => throw Unsupported(nameof(GetMessageAsync));

    public Task<GetHistoryMessagesResponse> GetHistoryMessagesAsync(GetHistoryMessagesRequest request)
        => throw Unsupported(nameof(GetHistoryMessagesAsync));

    public Task<GetResourceTempUrlResponse> GetResourceTempUrlAsync(GetResourceTempUrlRequest request)
        => throw Unsupported(nameof(GetResourceTempUrlAsync));

    public Task<GetForwardedMessagesResponse> GetForwardedMessagesAsync(GetForwardedMessagesRequest request)
        => throw Unsupported(nameof(GetForwardedMessagesAsync));

    public Task MarkMessageAsReadAsync(MarkMessageAsReadRequest request)
        => throw Unsupported(nameof(MarkMessageAsReadAsync));

    private static NotSupportedException Unsupported(string memberName)
        => new($"DemoAdapter 尚未实现 {nameof(IMessageService)}.{memberName}；请替换为目标平台的消息接口调用。");
}
