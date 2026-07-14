using ShiroBot.Model.File.Requests;
using ShiroBot.Model.File.Responses;
using ShiroBot.SDK.Adapter;

namespace ShiroBot.DemoAdapter.Services;

public sealed class FileService : IFileService
{
    public Task<UploadPrivateFileResponse> UploadPrivateFileAsync(UploadPrivateFileRequest request)
        => throw Unsupported(nameof(UploadPrivateFileAsync));

    public Task<UploadGroupFileResponse> UploadGroupFileAsync(UploadGroupFileRequest request)
        => throw Unsupported(nameof(UploadGroupFileAsync));

    public Task<GetPrivateFileDownloadUrlResponse> GetPrivateFileDownloadUrlAsync(GetPrivateFileDownloadUrlRequest request)
        => throw Unsupported(nameof(GetPrivateFileDownloadUrlAsync));

    public Task<GetGroupFileDownloadUrlResponse> GetGroupFileDownloadUrlAsync(GetGroupFileDownloadUrlRequest request)
        => throw Unsupported(nameof(GetGroupFileDownloadUrlAsync));

    public Task<GetGroupFilesResponse> GetGroupFilesAsync(GetGroupFilesRequest request)
        => throw Unsupported(nameof(GetGroupFilesAsync));

    public Task MoveGroupFileAsync(MoveGroupFileRequest request)
        => throw Unsupported(nameof(MoveGroupFileAsync));

    public Task RenameGroupFileAsync(RenameGroupFileRequest request)
        => throw Unsupported(nameof(RenameGroupFileAsync));

    public Task DeleteGroupFileAsync(DeleteGroupFileRequest request)
        => throw Unsupported(nameof(DeleteGroupFileAsync));

    public Task<CreateGroupFolderResponse> CreateGroupFolderAsync(CreateGroupFolderRequest request)
        => throw Unsupported(nameof(CreateGroupFolderAsync));

    public Task RenameGroupFolderAsync(RenameGroupFolderRequest request)
        => throw Unsupported(nameof(RenameGroupFolderAsync));

    public Task DeleteGroupFolderAsync(DeleteGroupFolderRequest request)
        => throw Unsupported(nameof(DeleteGroupFolderAsync));

    public Task PersistGroupFileAsync(PersistGroupFileRequest request)
        => throw Unsupported(nameof(PersistGroupFileAsync));

    private static NotSupportedException Unsupported(string memberName)
        => new($"DemoAdapter 尚未实现 {nameof(IFileService)}.{memberName}；请替换为目标平台的文件接口调用。");
}
