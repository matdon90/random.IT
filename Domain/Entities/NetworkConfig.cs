namespace Domain.Entities
{
    public class NetworkConfig
    {
        public string ipV4Address { get; set; }
        public string subnetMask { get; set; }
        public string defaultGateway { get; set; }
    }
}
