using ShiroBot.Model.System.Requests;
using ShiroBot.Model.System.Responses;
using ShiroBot.SDK.Adapter;

namespace ShiroBot.DemoAdapter.Services;

public sealed class SystemService : ISystemService
{
    public Task<GetLoginInfoResponse> GetLoginInfoAsync()
        => throw Unsupported(nameof(GetLoginInfoAsync));

    public Task<GetImplInfoResponse> GetImplInfoAsync()
        => throw Unsupported(nameof(GetImplInfoAsync));

    public Task<GetUserProfileResponse> GetUserProfileAsync(GetUserProfileRequest request)
        => throw Unsupported(nameof(GetUserProfileAsync));

    public Task<GetFriendListResponse> GetFriendListAsync(GetFriendListRequest request)
        => throw Unsupported(nameof(GetFriendListAsync));

    public Task<GetFriendInfoResponse> GetFriendInfoAsync(GetFriendInfoRequest request)
        => throw Unsupported(nameof(GetFriendInfoAsync));

    public Task<GetGroupListResponse> GetGroupListAsync(GetGroupListRequest request)
        => throw Unsupported(nameof(GetGroupListAsync));

    public Task<GetGroupInfoResponse> GetGroupInfoAsync(GetGroupInfoRequest request)
        => throw Unsupported(nameof(GetGroupInfoAsync));

    public Task<GetGroupMemberListResponse> GetGroupMemberListAsync(GetGroupMemberListRequest request)
        => throw Unsupported(nameof(GetGroupMemberListAsync));

    public Task<GetGroupMemberInfoResponse> GetGroupMemberInfoAsync(GetGroupMemberInfoRequest request)
        => throw Unsupported(nameof(GetGroupMemberInfoAsync));

    public Task SetAvatarAsync(SetAvatarRequest request)
        => throw Unsupported(nameof(SetAvatarAsync));

    public Task SetNicknameAsync(SetNicknameRequest request)
        => throw Unsupported(nameof(SetNicknameAsync));

    public Task SetBioAsync(SetBioRequest request)
        => throw Unsupported(nameof(SetBioAsync));

    public Task<GetCustomFaceUrlListResponse> GetCustomFaceUrlListAsync()
        => throw Unsupported(nameof(GetCustomFaceUrlListAsync));

    public Task<GetCookiesResponse> GetCookiesAsync(GetCookiesRequest request)
        => throw Unsupported(nameof(GetCookiesAsync));

    public Task<GetCsrfTokenResponse> GetCsrfTokenAsync()
        => throw Unsupported(nameof(GetCsrfTokenAsync));

    private static NotSupportedException Unsupported(string memberName)
        => new($"DemoAdapter 尚未实现 {nameof(ISystemService)}.{memberName}；请替换为目标平台的系统接口调用。");
}
