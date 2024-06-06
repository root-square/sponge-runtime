using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Utilities
{
    public static class NetworkHelper
    {
        /// <summary>
        /// Gets a value that indicates whether the port is available.
        /// </summary>
        /// <param name="port">A target port to scan</param>
        /// <param name="isTCP">Scans the TCP port if this value is true or the UDP port if it is false.</param>
        /// <returns>Port availability</returns>
        public static bool IsPortAvailable(int port, bool isTCP = true)
        {
            var ipProperties = IPGlobalProperties.GetIPGlobalProperties();

            if (isTCP)
            {
                // Note: Check ActiveConnection ports.
                TcpConnectionInformation[] connections = ipProperties.GetActiveTcpConnections();
                foreach (var connection in connections)
                {
                    if (connection.LocalEndPoint.Port == port)
                    {
                        return false;
                    }
                }
            }

            // Note: Check listening ports.
            IPEndPoint[] endpoints = isTCP ? ipProperties.GetActiveTcpListeners() : ipProperties.GetActiveUdpListeners();
            foreach (var endpoint in endpoints)
            {
                if (endpoint.Port == port)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Gets an available network port.
        /// </summary>
        /// <param name="range">A port range to scan</param>
        /// <param name="isTCP">Scans the TCP port if this value is true or the UDP port if it is false.</param>
        /// <returns>A port number</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static int GetAvailablePort(Range range, bool isTCP = true)
        {
            if (range.Start.Value < 0 || range.Start.Value > 65535 || range.End.Value < 0 || range.End.Value > 65535)
            {
                throw new ArgumentOutOfRangeException(nameof(range), "The valid range of the port number is 0~65535.");
            }

            if (range.Start.Value >= range.End.Value )
            {
                throw new ArgumentOutOfRangeException(nameof(range), "The start of range cannot be greater than or equal to the end of range.");
            }

            var ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            var ports = Enumerable.Range(range.Start.Value, range.End.Value - range.Start.Value + 1).ToList();
            if (isTCP)
            {
                ports.Except(ipProperties.GetActiveTcpConnections().Select(connection => connection.LocalEndPoint.Port));
                ports.Except(ipProperties.GetActiveTcpListeners().Select(listener => listener.Port));
            }
            else
            {
                ports.Except(ipProperties.GetActiveUdpListeners().Select(listener => listener.Port));
            }

            var random = new Random(DateTime.Now.Second + DateTime.Now.Millisecond);
            var randomIndex = random.Next(0, ports.Count());
            return ports[randomIndex];
        }
    }
}
