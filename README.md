# ShiroBot DemoAdapter

这是一个面向 `ShiroBot.SDK 0.7.0-rc3` 的完整示例适配器。它显式列出当前 SDK 的全部 service 方法和事件，便于开发者直接逐项替换为目标平台实现，也便于 SDK 升级时发现接口差异。

## 项目结构

- `DemoAdapter.cs`：适配器入口、配置加载和可选演示事件循环。
- `DemoAdapterConfig.cs`：带完整 `ConfigField` 元数据的配置模型。
- `Services/FileService.cs`：当前 `IFileService` 的全部方法。
- `Services/FriendService.cs`：当前 `IFriendService` 的全部方法。
- `Services/GroupService.cs`：当前 `IGroupService` 的全部方法。
- `Services/MessageService.cs`：当前 `IMessageService` 的全部方法。
- `Services/SystemService.cs`：当前 `ISystemService` 的全部方法。
- `Services/EventService.cs`：当前 `IEventService` 的全部事件及演示事件发布入口。
- `tests/`：通过反射检查每个模板 service 是否显式声明当前接口的全部成员。

除演示事件发布外，所有尚未接入目标平台的方法都会明确抛出 `NotSupportedException`。实现真实适配器时，可以保留方法签名并直接替换方法体。

## 适配器元数据

SDK 0.7.0 使用 `BotAdapterAttribute` 声明适配器元数据：

```csharp
[BotAdapter(
    "DemoAdapter",
    Name = "ShiroBot 示例适配器",
    Version = "0.7.0",
    Description = "展示 ShiroBot SDK 适配器接口、配置加载与事件发布的完整模板。",
    Author = "ShiroBot")]
```

示例没有声明 `IBotAdapter.Metadata`。从 SDK 0.7.0 开始，该成员应由 SDK 提供默认兼容实现，并从适配器特性读取元数据。

## 配置和启动行为

`StartAsync` 使用宿主注入的 `IConfigContext` 加载并保存 `DemoAdapterConfig`。保存操作会把新增的默认配置项写入配置文件。

| 配置项 | 默认值 | 说明 |
| --- | --- | --- |
| `EnableDemoEvent` | `false` | 是否启动周期性演示事件。默认关闭。 |
| `DemoEventIntervalSeconds` | `30` | 演示事件间隔，运行时最小按 1 秒处理。 |
| `DemoEventSelfId` | `10000` | 演示事件中的机器人 ID。 |
| `DemoEventUserId` | `10001` | 演示事件中的好友 ID。 |

启用后，适配器会周期性通过 `EventService.FriendNudge` 发布 `FriendNudgeEvent`。真实适配器应删除或替换该循环，从 WebSocket、SSE、Webhook 或平台 SDK 接收事件，再触发对应事件。

## SDK 接口兼容规则

ShiroBot SDK 对已发布 adapter 的接口演进必须遵循以下规则：

- SDK 向 `IFileService`、`IFriendService`、`IGroupService`、`IMessageService` 或 `ISystemService` 新增方法时，必须提供 default interface implementation，通常默认抛出 `NotSupportedException`。
- SDK 向 `IEventService` 新增事件时，也必须提供 default interface implementation；事件的默认 `add`/`remove` 访问器可以为空实现。
- SDK 向 `IBotAdapter` 新增成员时，同样必须提供兼容默认实现，或者通过不破坏既有实现类的其他机制演进。
- 如果新增接口成员没有默认实现，使用旧版 SDK 编译的 adapter 将不再满足新接口契约，宿主可能在加载或调用时失败；仅提高包版本不能保证二进制兼容。
- 默认实现用于保证旧 adapter 继续加载，不代表新 adapter 应忽略新能力。此模板故意显式实现当前全部成员，让代码审查可以直接看到支持范围。

`ServiceContractTests` 不依赖接口成员是否有默认实现。升级 SDK 后，只要接口新增了方法或事件，而模板 service 尚未显式声明，反射测试就会失败并列出遗漏成员。这补足了编译器在 default interface implementation 场景下不会报错的问题。

## SDK 引用策略

正式依赖始终是：

```xml
<PackageReference Include="ShiroBot.SDK" Version="0.7.0-rc3" />
```

在此仓库与 ShiroBot 主仓库并排开发，且 `../ShiroBot/ShiroBot.SDK/ShiroBot.SDK.csproj` 存在时，项目默认临时切换到相对路径 `ProjectReference`，以便在预发布包尚未发布时进行本地构建。

CI、tag release 和正式发布必须传入：

```bash
-p:UseLocalShiroBotSdk=false
```

这样构建会使用 `ShiroBot.SDK 0.7.0-rc3` 的 `PackageReference`，不会把主仓库项目引用带入发布产物。

## 本地构建和测试

在与 ShiroBot 主仓库并排的当前目录中运行：

```bash
dotnet build ShiroBot.Adapter.DemoAdapter.slnx
dotnet test ShiroBot.Adapter.DemoAdapter.slnx
```

0.7.0 发布后，也可以显式验证 NuGet 包构建：

```bash
dotnet restore ShiroBot.Adapter.DemoAdapter.slnx -p:UseLocalShiroBotSdk=false
dotnet build ShiroBot.Adapter.DemoAdapter.slnx -c Release --no-restore -p:UseLocalShiroBotSdk=false
dotnet test ShiroBot.Adapter.DemoAdapter.slnx -c Release --no-build --no-restore -p:UseLocalShiroBotSdk=false
```

## GitHub Actions

- `.github/workflows/ci.yml`：对 pull request、`main` 分支 push 和手动触发执行 restore、build、test。
- `.github/workflows/release.yml`：推送 `v*` tag 或手动指定 tag 时，使用正式 `PackageReference` 构建、测试、发布，并把 adapter DLL/PDB 上传到 GitHub Release。
