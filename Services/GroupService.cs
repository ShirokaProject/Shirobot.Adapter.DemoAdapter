using ShiroBot.Model.Group.Requests;
using ShiroBot.Model.Group.Responses;
using ShiroBot.SDK.Adapter;

namespace ShiroBot.DemoAdapter.Services;

public sealed class GroupService : IGroupService
{
    public Task SetGroupNameAsync(SetGroupNameRequest request)
        => throw Unsupported(nameof(SetGroupNameAsync));

    public Task SetGroupAvatarAsync(SetGroupAvatarRequest request)
        => throw Unsupported(nameof(SetGroupAvatarAsync));

    public Task SetGroupMemberCardAsync(SetGroupMemberCardRequest request)
        => throw Unsupported(nameof(SetGroupMemberCardAsync));

    public Task SetGroupMemberSpecialTitleAsync(SetGroupMemberSpecialTitleRequest request)
        => throw Unsupported(nameof(SetGroupMemberSpecialTitleAsync));

    public Task SetGroupMemberAdminAsync(SetGroupMemberAdminRequest request)
        => throw Unsupported(nameof(SetGroupMemberAdminAsync));

    public Task SetGroupMemberMuteAsync(SetGroupMemberMuteRequest request)
        => throw Unsupported(nameof(SetGroupMemberMuteAsync));

    public Task SetGroupWholeMuteAsync(SetGroupWholeMuteRequest request)
        => throw Unsupported(nameof(SetGroupWholeMuteAsync));

    public Task KickGroupMemberAsync(KickGroupMemberRequest request)
        => throw Unsupported(nameof(KickGroupMemberAsync));

    public Task<GetGroupAnnouncementsResponse> GetGroupAnnouncementsAsync(GetGroupAnnouncementsRequest request)
        => throw Unsupported(nameof(GetGroupAnnouncementsAsync));

    public Task SendGroupAnnouncementAsync(SendGroupAnnouncementRequest request)
        => throw Unsupported(nameof(SendGroupAnnouncementAsync));

    public Task DeleteGroupAnnouncementAsync(DeleteGroupAnnouncementRequest request)
        => throw Unsupported(nameof(DeleteGroupAnnouncementAsync));

    public Task<GetGroupEssenceMessagesResponse> GetGroupEssenceMessagesAsync(GetGroupEssenceMessagesRequest request)
        => throw Unsupported(nameof(GetGroupEssenceMessagesAsync));

    public Task SetGroupEssenceMessageAsync(SetGroupEssenceMessageRequest request)
        => throw Unsupported(nameof(SetGroupEssenceMessageAsync));

    public Task QuitGroupAsync(QuitGroupRequest request)
        => throw Unsupported(nameof(QuitGroupAsync));

    public Task SendGroupMessageReactionAsync(SendGroupMessageReactionRequest request)
        => throw Unsupported(nameof(SendGroupMessageReactionAsync));

    public Task SendGroupNudgeAsync(SendGroupNudgeRequest request)
        => throw Unsupported(nameof(SendGroupNudgeAsync));

    public Task<GetGroupNotificationsResponse> GetGroupNotificationsAsync(GetGroupNotificationsRequest request)
        => throw Unsupported(nameof(GetGroupNotificationsAsync));

    public Task AcceptGroupRequestAsync(AcceptGroupRequestRequest request)
        => throw Unsupported(nameof(AcceptGroupRequestAsync));

    public Task RejectGroupRequestAsync(RejectGroupRequestRequest request)
        => throw Unsupported(nameof(RejectGroupRequestAsync));

    public Task AcceptGroupInvitationAsync(AcceptGroupInvitationRequest request)
        => throw Unsupported(nameof(AcceptGroupInvitationAsync));

    public Task RejectGroupInvitationAsync(RejectGroupInvitationRequest request)
        => throw Unsupported(nameof(RejectGroupInvitationAsync));

    private static NotSupportedException Unsupported(string memberName)
        => new($"DemoAdapter 尚未实现 {nameof(IGroupService)}.{memberName}；请替换为目标平台的群接口调用。");
}
