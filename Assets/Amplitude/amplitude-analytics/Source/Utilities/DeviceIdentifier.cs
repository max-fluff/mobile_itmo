using System.Linq;
using System.Net.NetworkInformation;
using JetBrains.Annotations;

namespace Analytics
{
    [PublicAPI]
    public static class DeviceIdentifier
    {
        public static string GetUniquePhysicalAddress()
        {
            var uniqueId = string.Empty;
            var nics = NetworkInterface.GetAllNetworkInterfaces();

            uniqueId = nics.Aggregate(uniqueId, (current, adapter) => 
                current + adapter.GetPhysicalAddress());
            return uniqueId;
        }
    }
}