using Constructors;
using System;
using System.Net;

namespace SubnetControllers
{
    public class StandardSubnetController : SubnetController
    {
        public Subnet Create(string IPAddress, byte CIDR, string Description = null)
        {
            try
            {
                return createSubnet(IPAddress, IPNetwork.Parse(IPAddress, CIDR), Description);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        Subnet createSubnet(string IPAddress, IPNetwork iPNetwork, string Description)
        {
            return new Subnet()
            {
                IPAddress = IPAddress,
                Description = Description,
                Network = iPNetwork.Network.ToString(),
                Netmask = iPNetwork.Netmask.ToString(),
                Broadcast = iPNetwork.Broadcast.ToString(),
                Cidr = iPNetwork.Cidr,
                Usable = (int)iPNetwork.Usable,
                FirstUsableIP = iPNetwork.FirstUsable.ToString(),
                LastUsableIP = iPNetwork.LastUsable.ToString()
            };
        }

        public bool IsInSubnet(Subnet source, Subnet toTest)
        {
            IPNetwork sourceSubnet = IPNetwork.Parse(source.IPAddress, source.Cidr);
            IPNetwork testSubnet = IPNetwork.Parse(toTest.IPAddress, toTest.Cidr);

            return sourceSubnet.Contains(testSubnet);
        }
    }
}
