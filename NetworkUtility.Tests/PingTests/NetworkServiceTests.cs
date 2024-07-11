using FluentAssertions;
using FluentAssertions.Extensions;
using NetworkUtility.DNS;
using NetworkUtility.Ping;
using System.Net.NetworkInformation;
using FakeItEasy;

namespace NetworkUtility.Tests.PingTests;

public class NetworkServiceTests
{
    private readonly NetworkService _pingService;
    private readonly IDNS _dns;

    public NetworkServiceTests()
    {
        _dns = A.Fake<IDNS>();

        _pingService = new NetworkService(_dns);
    }

    [Fact]
    public void NetworkService_SendPing_ReturnString()
    {
        // Arrange
        A.CallTo(() => _dns.SendDNS()).Returns(true);
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
    public void NetworkService_MostRecentPings_ReturnsObject()
    {
        //Arrange
        var expected = new PingOptions()
        {
            DontFragment = true,
            Ttl = 1
        };

        //Act
        var result = _pingService.MostRecentPings();

        //Assert
        result.Should().BeAssignableTo<IEnumerable<PingOptions>>();
        result.Should().ContainEquivalentOf(expected);
        result.Should().Contain(x => x.DontFragment);
    }
}
