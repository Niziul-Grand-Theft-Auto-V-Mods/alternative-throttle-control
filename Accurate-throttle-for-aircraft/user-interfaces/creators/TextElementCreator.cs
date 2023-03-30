using GTA.UI;

using Accurate_throttle_for_aircraft.user_interfaces.creators.resources.structs;


namespace Accurate_throttle_for_aircraft.user_interfaces.creators
{
    internal abstract class TextElementCreator
    {
        protected TextElement CreateAndReturnAnTextElementWithThis(StTextElementConfiguration stTextElementConfiguration)
        {
            return _
                   = new TextElement(stTextElementConfiguration
                                                        .Caption,
                                     stTextElementConfiguration
                                                        .Position,
                                     stTextElementConfiguration
                                                        .Scale,
                                     stTextElementConfiguration
                                                        .Color);
        }
    }
}
