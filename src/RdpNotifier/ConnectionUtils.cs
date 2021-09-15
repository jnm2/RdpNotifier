using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace RdpNotifier
{
    internal static class ConnectionUtils
    {
        public static async Task<bool> IsPortOpenAsync(DnsEndPoint endPoint, CancellationToken cancellationToken)
        {
            using var client = new TcpClient();

            try
            {
                await client.ConnectAsync(endPoint.Host, endPoint.Port, cancellationToken).ConfigureAwait(false);
                return true;
            }
            catch (SocketException)
            {
                return false;
            }
        }

        public static async Task<bool> IsRdpGatewayOnlineAsync(DnsEndPoint endPoint, CancellationToken cancellationToken)
        {
            var uriBuilder = new UriBuilder { Scheme = "https", Host = endPoint.Host, Path = "/remoteDesktopGateway/" };
            if (endPoint.Port != 443)
                uriBuilder.Port = endPoint.Port;

            using var client = new HttpClient();

            // https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-tsgu/6ea45086-c98d-4d5a-ac8c-fb0decc5d2c1
            using var request = new HttpRequestMessage(new HttpMethod("RDG_OUT_DATA"), uriBuilder.Uri);
            request.Headers.CacheControl = new() { NoCache = true };
            request.Headers.Pragma.Add(new("no-cache"));
            request.Headers.Add("RDG-Connection-Id", "{00000000-0000-0000-0000-000000000000}");

            try
            {
                using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                return response.StatusCode == HttpStatusCode.Unauthorized;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }
    }
}
