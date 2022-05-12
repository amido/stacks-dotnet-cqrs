using System;
using System.Text;
using Amido.Stacks.Messaging.Azure.ServiceBus.Serializers;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using xxAMIDOxx.xxSTACKSxx.Application.CQRS.Events;

namespace xxAMIDOxx.xxSTACKSxx.Listener;

public class StacksListener
{
    private readonly IMessageReader msgReader;
    private readonly ILogger<StacksListener> logger;

    public StacksListener(IMessageReader msgReader, ILogger<StacksListener> logger)
    {
        this.msgReader = msgReader;
        this.logger = logger;
    }

    [Obsolete("This Method is Deprecated. Please use StacksListener.ReceiveMessage()")]
    [FunctionName("StacksListenerMessage")]
    public void Run([ServiceBusTrigger(
        "%TOPIC_NAME%",
        "%SUBSCRIPTION_NAME%",
        Connection = "SERVICEBUS_CONNECTIONSTRING")] Message mySbMsg)
    {
        var appEvent = msgReader.Read<StacksCloudEvent<MenuCreatedEvent>>(mySbMsg);

        // TODO: work with appEvent
        logger.LogInformation($"Message read. Menu Id: {appEvent?.Data?.MenuId}");

        logger.LogInformation($"C# ServiceBus topic trigger function processed message: {appEvent}");
    }

    [FunctionName("StacksListenerServiceBusReceivedMessage")]
    public void ReceiveMessage([ServiceBusTrigger(
        "%TOPIC_NAME%",
        "%SUBSCRIPTION_NAME%",
        Connection = "SERVICEBUS_CONNECTIONSTRING")] ServiceBusReceivedMessage mySbMsg)
    {
        var appEvent = JsonConvert.DeserializeObject<StacksCloudEvent<MenuCreatedEvent>>(Encoding.UTF8.GetString(mySbMsg.Body));

        // TODO: work with appEvent
        logger.LogInformation($"Message read. Menu Id: {appEvent?.Data?.MenuId}");

        logger.LogInformation($"C# ServiceBus topic trigger function processed message: {appEvent}");
    }
}

