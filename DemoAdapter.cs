using ShiroBot.SDK.Adapter;
using ShiroBot.SDK.Config;
using ShiroBot.SDK.Core;
using ShiroBot.SDK.Plugin;
using ShiroBot.DemoAdapter.Services;

namespace ShiroBot.DemoAdapter;

[BotAdapter(
    "DemoAdapter",
    Name = "ShiroBot 示例适配器",
    Version = "0.7.1",
    Description = "展示 ShiroBot SDK 适配器接口、配置加载与事件发布的完整模板。",
    Author = "ShiroBot",
    IsSingleFile = true)]
public sealed class DemoAdapter : IBotAdapter
{
    private readonly EventService _eventService = new();
    private CancellationTokenSource? _demoEventCancellation;
    private Task? _demoEventTask;

    public IFileService File { get; } = new FileService();
    public IFriendService Friend { get; } = new FriendService();
    public IGroupService Group { get; } = new GroupService();
    public IMessageService Message { get; } = new MessageService();
    public ISystemService System { get; } = new SystemService();
    public IEventService Event => _eventService;

    public IConfigContext Config { get; set; } = null!;
    public IConsoleLogger Logger { get; set; } = null!;

    public async Task StartAsync()
    {
        await StopAsync().ConfigureAwait(false);
        var config = Config.Load<DemoAdapterConfig>();
        Config.Save(config);

        if (!config.EnableDemoEvent)
        {
            Logger.Info("DemoAdapter 已启动；周期性演示事件处于关闭状态。");
            return;
        }

        var cancellation = new CancellationTokenSource();
        _demoEventCancellation = cancellation;
        _demoEventTask = PublishDemoEventsAsync(config, cancellation.Token);

        Logger.Success($"DemoAdapter 已启动；每 {Math.Max(1, config.DemoEventIntervalSeconds)} 秒发布一次演示事件。");
    }

    public async Task StopAsync()
    {
        var cancellation = _demoEventCancellation;
        var task = _demoEventTask;
        _demoEventCancellation = null;
        _demoEventTask = null;
        if (cancellation is null) return;

        cancellation.Cancel();
        try
        {
            if (task is not null) await task.ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            cancellation.Dispose();
        }
    }

    private async Task PublishDemoEventsAsync(DemoAdapterConfig config, CancellationToken cancellationToken)
    {
        var interval = TimeSpan.FromSeconds(Math.Max(1, config.DemoEventIntervalSeconds));
        using var timer = new PeriodicTimer(interval);

        try
        {
            while (await timer.WaitForNextTickAsync(cancellationToken))
            {
                try
                {
                    await _eventService.PublishDemoFriendNudgeAsync(
                        config.DemoEventSelfId,
                        config.DemoEventUserId);
                    Logger.Info("已发布一个演示 FriendNudgeEvent。");
                }
                catch (Exception ex)
                {
                    Logger.Error($"演示事件处理失败: {ex.GetType().Name}: {ex.Message}");
                }
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
        }
    }
}
