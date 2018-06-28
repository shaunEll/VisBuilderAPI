using Constructors;
using System;
using System.Net;
using Services;

namespace SubnetControllers
{
    public class StandardSubnetController : SubnetController
    {
        IPNetwork iPNetwork;

        public SubnetDTO Create(SubnetSourceData ssd)
        {
            try
            {
                iPNetwork = IPNetwork.Parse(ssd.IPAddress, ssd.Cidr);

                return new SubnetDTO()
                {
                    IPAddress = ssd.IPAddress,
                    Description = ssd.Description,
                    Network = iPNetwork.Network.ToString(),
                    Netmask = iPNetwork.Netmask.ToString(),
                    Broadcast = iPNetwork.Broadcast.ToString(),
                    Cidr = iPNetwork.Cidr,
                    Usable = (int)iPNetwork.Usable,
                    FirstUsableIP = iPNetwork.FirstUsable.ToString(),
                    LastUsableIP = iPNetwork.LastUsable.ToString()
                };
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool IsInSubnet(SubnetDTO sdtoSource, SubnetDTO sdtoToTest)
        {
            IPNetwork sourceSubnet = IPNetwork.Parse(sdtoSource.IPAddress, sdtoSource.Cidr);
            IPNetwork testSubnet = IPNetwork.Parse(sdtoToTest.IPAddress, sdtoToTest.Cidr);

            return sourceSubnet.Contains(testSubnet);
        }
    }
}
