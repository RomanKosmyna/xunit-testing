using FluentAssertions;
using NetworkUtility.Ping;

namespace NetworkUtility.Tests.PingTests;

public class NetworkServiceTests
{
    [Fact]
    public void NetworkService_SendPing_ReturnString()
    {
        // Arrange
        var pingService = new NetworkService();

        // Act
        var result = pingService.SendPing();

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
        var pingService = new NetworkService();

        var result = pingService.PingTimeout(a, b);

        result.Should().Be(expected);
        result.Should().BeGreaterThanOrEqualTo(2);
    }
}
