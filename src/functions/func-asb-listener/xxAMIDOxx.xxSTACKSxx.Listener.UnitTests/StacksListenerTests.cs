using System;
using System.Text;
using Amido.Stacks.Core.Operations;
using Amido.Stacks.Messaging.Azure.ServiceBus.Extensions;
using Amido.Stacks.Messaging.Azure.ServiceBus.Serializers;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NSubstitute;
using Xunit;
using xxAMIDOxx.xxSTACKSxx.Application.CQRS.Events;

namespace xxAMIDOxx.xxSTACKSxx.Listener.UnitTests;

[Trait("TestType", "UnitTests")]
public class StacksListenerTests
{
    private readonly IMessageReader msgReader;
    private readonly ILogger<StacksListener> logger;

    public StacksListenerTests()
    {
        msgReader = Substitute.For<IMessageReader>();
        logger = Substitute.For<ILogger<StacksListener>>();
    }

    [Fact]
    public void TestExecution()
    {
        var msgBody = BuildMessageBody();
        var message = BuildMessage(msgBody);

        var stacksListener = new StacksListener(msgReader, logger);

        stacksListener.Run(message);

        msgReader.Received(1).Read<StacksCloudEvent<MenuCreatedEvent>>(message);
    }

    public MenuCreatedEvent BuildMessageBody()
    {
        var id = Guid.NewGuid();
        return new MenuCreatedEvent(new TestOperationContext(), id);
    }

    public Message BuildMessage(MenuCreatedEvent body)
    {
        Guid correlationId = GetCorrelationId(body);

        var convertedMessage = new Message
        {
            CorrelationId = $"{correlationId}",
            ContentType = "application/json;charset=utf-8",
            Body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body))
        };

        return convertedMessage
            .SetEnclosedMessageType(body.GetType())
            .SetSerializerType(GetType());
    }

    private static Guid GetCorrelationId(object body)
    {
        var ctx = body as IOperationContext;
        return ctx?.CorrelationId ?? Guid.NewGuid();
    }
}
