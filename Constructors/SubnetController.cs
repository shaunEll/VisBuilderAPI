using Services;

namespace Constructors
{
    public interface SubnetController
    {
        SubnetDTO Create(SubnetSourceData ssd);
        bool IsInSubnet(SubnetDTO stdoSource, SubnetDTO sdtoToTest);
    }
}