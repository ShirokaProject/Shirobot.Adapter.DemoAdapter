using ShiroBot.SDK.Config;

namespace ShiroBot.DemoAdapter;

public sealed class DemoAdapterConfig
{
    [ConfigField(
        "是否启动周期性演示事件。默认关闭，生产适配器应改为连接真实事件源。",
        Label = "启用演示事件",
        Type = "boolean",
        Options = new string[] { "false", "true" },
        Min = 0,
        Max = 1,
        Placeholder = "false")]
    public bool EnableDemoEvent { get; set; }

    [ConfigField(
        "两次演示事件之间的间隔秒数，最小值为 1 秒。",
        Label = "演示事件间隔（秒）",
        Type = "number",
        Options = new string[] { },
        Min = 1,
        Max = 86400,
        Placeholder = "30")]
    public int DemoEventIntervalSeconds { get; set; } = 30;

    [ConfigField(
        "演示 FriendNudgeEvent 中使用的机器人账号 ID。",
        Label = "演示机器人 ID",
        Type = "number",
        Options = new string[] { },
        Min = 1,
        Max = 9999999999999,
        Placeholder = "10000")]
    public long DemoEventSelfId { get; set; } = 10000;

    [ConfigField(
        "演示 FriendNudgeEvent 中使用的好友账号 ID。",
        Label = "演示好友 ID",
        Type = "number",
        Options = new string[] { },
        Min = 1,
        Max = 9999999999999,
        Placeholder = "10001")]
    public long DemoEventUserId { get; set; } = 10001;
}
