using ShiroBot.Model.Friend.Requests;
using ShiroBot.Model.Friend.Responses;
using ShiroBot.SDK.Adapter;

namespace ShiroBot.DemoAdapter.Services;

public sealed class FriendService : IFriendService
{
    public Task SendFriendNudgeAsync(SendFriendNudgeRequest request)
        => throw Unsupported(nameof(SendFriendNudgeAsync));

    public Task SendProfileLikeAsync(SendProfileLikeRequest request)
        => throw Unsupported(nameof(SendProfileLikeAsync));

    public Task DeleteFriendAsync(DeleteFriendRequest request)
        => throw Unsupported(nameof(DeleteFriendAsync));

    public Task<GetFriendRequestsResponse> GetFriendRequestsAsync(GetFriendRequestsRequest request)
        => throw Unsupported(nameof(GetFriendRequestsAsync));

    public Task AcceptFriendRequestAsync(AcceptFriendRequestRequest request)
        => throw Unsupported(nameof(AcceptFriendRequestAsync));

    public Task RejectFriendRequestAsync(RejectFriendRequestRequest request)
        => throw Unsupported(nameof(RejectFriendRequestAsync));

    private static NotSupportedException Unsupported(string memberName)
        => new($"DemoAdapter 尚未实现 {nameof(IFriendService)}.{memberName}；请替换为目标平台的好友接口调用。");
}
