using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;
using xxAMIDOxx.xxSTACKSxx.API.Models.Requests;

namespace xxAMIDOxx.xxSTACKSxx.API.UnitTests.Models;

public class RequestTests
{
    [Theory, AutoData]
    public void Create_CreateCategoryRequest(CreateCategoryRequest request)
        => request.Should().NotBeNull();

    [Theory, AutoData]
    public void Create_CreateItemRequest(CreateItemRequest request)
        => request.Should().NotBeNull();
}
