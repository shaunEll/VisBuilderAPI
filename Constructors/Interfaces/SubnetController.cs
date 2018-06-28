namespace Constructors
{
    public interface SubnetController
    {
        Subnet Create(string IPAddress, byte CIDR, string Description = null);
        bool IsInSubnet(Subnet source, Subnet toTest);
    }
}