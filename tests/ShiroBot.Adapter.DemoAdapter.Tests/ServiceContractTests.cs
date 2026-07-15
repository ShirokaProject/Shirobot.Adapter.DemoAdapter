using System.Reflection;
using ShiroBot.DemoAdapter.Services;
using ShiroBot.Model.Common;
using ShiroBot.SDK.Adapter;
using Xunit;

namespace ShiroBot.DemoAdapter.Tests;

public sealed class ServiceContractTests
{
    public static TheoryData<Type, Type> ServiceContracts => new()
    {
        { typeof(IFileService), typeof(FileService) },
        { typeof(IFriendService), typeof(FriendService) },
        { typeof(IGroupService), typeof(GroupService) },
        { typeof(IMessageService), typeof(MessageService) },
        { typeof(ISystemService), typeof(SystemService) },
        { typeof(IEventService), typeof(EventService) }
    };

    [Theory]
    [MemberData(nameof(ServiceContracts))]
    public void Service_explicitly_declares_every_current_interface_member(Type contractType, Type implementationType)
    {
        var missingMethods = contractType
            .GetMethods()
            .Where(method => !method.IsSpecialName)
            .Where(method => FindDeclaredMethod(implementationType, method) is null)
            .Select(FormatMethod)
            .ToArray();

        var missingEvents = contractType
            .GetEvents()
            .Where(@event => implementationType.GetEvent(
                @event.Name,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly) is not { } declaredEvent
                || declaredEvent.EventHandlerType != @event.EventHandlerType)
            .Select(@event => @event.Name)
            .ToArray();

        Assert.True(
            missingMethods.Length == 0 && missingEvents.Length == 0,
            $"{implementationType.Name} 未显式覆盖 {contractType.Name} 的成员。" +
            $" Methods: [{string.Join(", ", missingMethods)}];" +
            $" Events: [{string.Join(", ", missingEvents)}]");
    }

    [Fact]
    public async Task Event_service_publishes_arbitrary_models_through_unified_stream()
    {
        var service = new EventService();
        var expected = new GroupDisbandEvent(1, 2, 3, 4);
        Event? received = null;
        service.EventReceived += evt =>
        {
            received = evt;
            return Task.CompletedTask;
        };

        await service.PublishAsync(expected);

        Assert.Same(expected, received);
    }

    private static MethodInfo? FindDeclaredMethod(Type implementationType, MethodInfo contractMethod)
    {
        var parameterTypes = contractMethod
            .GetParameters()
            .Select(parameter => parameter.ParameterType)
            .ToArray();

        var method = implementationType.GetMethod(
            contractMethod.Name,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly,
            binder: null,
            types: parameterTypes,
            modifiers: null);

        return method?.ReturnType == contractMethod.ReturnType ? method : null;
    }

    private static string FormatMethod(MethodInfo method)
    {
        var parameters = string.Join(", ", method.GetParameters().Select(parameter => parameter.ParameterType.Name));
        return $"{method.Name}({parameters})";
    }
}
