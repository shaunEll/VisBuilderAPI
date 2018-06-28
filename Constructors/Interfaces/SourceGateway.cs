namespace Constructors
{
    public class SubnetSourceData
    {
        public string IPAddress { get; set; }
        public byte Cidr { get; set; }
        public string Description { get; set; }
    }

    public interface SourceGateway
    {
        bool EndOfSource { get; }
        SubnetSourceData ReadNext();
        void RestartReader();
    }
}