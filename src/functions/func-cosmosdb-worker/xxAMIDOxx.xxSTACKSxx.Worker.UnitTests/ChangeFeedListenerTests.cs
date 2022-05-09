using System;
using System.Collections.Generic;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Azure.Documents;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace xxAMIDOxx.xxSTACKSxx.Worker.UnitTests;

[Trait("TestType", "UnitTests")]
public class ChangeFeedListenerTests
{
    private readonly IApplicationEventPublisher appEventPublisher;
    private readonly ILogger<ChangeFeedListener> logger;

    public ChangeFeedListenerTests()
    {
        appEventPublisher = Substitute.For<IApplicationEventPublisher>();
        logger = Substitute.For<ILogger<ChangeFeedListener>>();
    }

    [Fact]
    public void TestExecution()
    {
        var changeFeedListener = new ChangeFeedListener(appEventPublisher, logger);

        var trigger = GetDocuments(3);

        changeFeedListener.Run(trigger);

        appEventPublisher.Received(3).PublishAsync(Arg.Any<IApplicationEvent>());
    }

    [Fact]
    public void TestExecution_EmptyList()
    {
        var changeFeedListener = new ChangeFeedListener(appEventPublisher, logger);

        var trigger = GetDocuments(0);

        changeFeedListener.Run(trigger);

        appEventPublisher.Received(0).PublishAsync(Arg.Any<IApplicationEvent>());
    }

    private IReadOnlyList<Document> GetDocuments(int quantity)
    {
        var result = new List<Document>();

        for (int i = 0; i < quantity; i++)
        {
            var id = Guid.NewGuid();
            result.Add(new Document()
            {
                Id = $"{id}",
                ResourceId = $"{id}",
                TimeToLive = 500
            });
        }

        return result;
    }
}
