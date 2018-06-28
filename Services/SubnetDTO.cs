namespace Services
{
    public class SubnetDTO
    {
        public string IPAddress { get; set; }
        public string Network { get; set; }
        public string Netmask { get; set; }
        public string Broadcast { get; set; }
        public string FirstUsableIP { get; set; }
        public string LastUsableIP { get; set; }
        public int Usable { get; set; }
        public byte Cidr { get; set; }

        public string Description { get; set; }
    }
}