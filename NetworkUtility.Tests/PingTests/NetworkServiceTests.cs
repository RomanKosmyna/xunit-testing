using FluentAssertions;
using FluentAssertions.Extensions;
using NetworkUtility.Ping;
using System.Net.NetworkInformation;

namespace NetworkUtility.Tests.PingTests;

public class NetworkServiceTests
{
    private readonly NetworkService _pingService;

    public NetworkServiceTests()
    {
        _pingService = new NetworkService();
    }

    [Fact]
    public void NetworkService_SendPing_ReturnString()
    {
        // Arrange

        // Act
        var result = _pingService.SendPing();

        // Assert
        result.Should().NotBeNullOrWhiteSpace();
        result.Should().Be("Success: Ping Sent!");
        //result.Should().Contain("Ping", Exactly.Once());
    }

    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(1, 2, 3)]
    public void NetworkService_PingTimeout_ReturnInt(int a, int b, int expected)
    {
        var result = _pingService.PingTimeout(a, b);

        result.Should().Be(expected);
        result.Should().BeGreaterThanOrEqualTo(2);
    }

    [Fact]
    public void NetworkService_LastPingDate_ReturnDate()
    {
        var result = _pingService.LastPingDate();

        result.Should().BeAfter(1.January(2010));
        result.Should().BeBefore(1.January(2030));
    }

    [Fact]
    public void NetworkService_GetPingOptions_ReturnsObject()
    {
        //Arrange
        var expected = new PingOptions()
        {
            DontFragment = true,
            Ttl = 1
        };

        //Act
        var result = _pingService.GetPingOptions();

        //Assert
        result.Should().BeOfType<PingOptions>();
        result.Should().BeEquivalentTo(expected);
        result.Ttl.Should().Be(expected.Ttl);
    }
}
