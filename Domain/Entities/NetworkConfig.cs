namespace Domain.Entities
{
    public class NetworkConfig
    {
        public string ipHostAddress { get; set; }
        public string subnetMask { get; set; }
        public string subnetAddress { get; set; }
        public string subnetBroadcastAddress { get; set; }
        public int freeHostsNumberInSubnet { get; set; }
    }
}
