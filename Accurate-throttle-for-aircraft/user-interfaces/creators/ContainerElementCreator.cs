using GTA.UI;

using Accurate_throttle_for_aircraft.user_interfaces.creators.resources.structs;

namespace Accurate_throttle_for_aircraft.user_interfaces.creators
{
    internal abstract class ContainerElementCreator
    {
        protected ContainerElement CreateAndReturnAnContainerWithThis(StContainerElementConfiguration stContainerElementConfiguration)
        {
            return _
                   = new ContainerElement(stContainerElementConfiguration
                                                                    .Position,
                                          stContainerElementConfiguration
                                                                    .Size,
                                          stContainerElementConfiguration
                                                                    .Color);
        }
    }
}