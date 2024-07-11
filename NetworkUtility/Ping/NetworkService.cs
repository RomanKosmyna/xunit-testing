using NetworkUtility.DNS;
using System.Net.NetworkInformation;

namespace NetworkUtility.Ping;

public class NetworkService
{
    private readonly IDNS _dns;

    public NetworkService(IDNS dns)
    {
        _dns = dns;
    }

    public string SendPing()
    {
        var dnsSucess = _dns.SendDNS();

        if (dnsSucess) return "Success: Ping Sent!";

        return "Failed: Ping not Sent!";
    }

    public int PingTimeout(int a, int b)
    {
        return a + b;
    }

    public DateTime LastPingDate()
    {
        return DateTime.Now;
    }

    public PingOptions GetPingOptions()
    {
        return new PingOptions()
        {
            DontFragment = true,
            Ttl = 1
        };
    }

    public IEnumerable<PingOptions> MostRecentPings()
    {
        IEnumerable<PingOptions> pingOptions =
        [
            new PingOptions()
            {
                DontFragment= true,
                Ttl = 1
            },
            new PingOptions()
            {
                DontFragment= true,
                Ttl = 1
            },
            new PingOptions()
            {
                DontFragment= true,
                Ttl = 1
            }
        ];

        return pingOptions;
    }
}