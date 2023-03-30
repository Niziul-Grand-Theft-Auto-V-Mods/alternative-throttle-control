using GTA.UI;

using Accurate_throttle_for_aircraft.user_interfaces.creators.resources.structs;


namespace Accurate_throttle_for_aircraft.user_interfaces.creators
{
    internal abstract class CustomSpriteCreator
    {
        protected CustomSprite CreateAndReturnAnCustomSpriteWithThis(StCustomSpriteConfiguration stCustomSpriteConfiguration)
        {
            return _
                   = new CustomSprite(stCustomSpriteConfiguration
                                                            .Filename,
                                      stCustomSpriteConfiguration
                                                            .Size,
                                      stCustomSpriteConfiguration
                                                            .Position,
                                      stCustomSpriteConfiguration
                                                            .Color);
        }
    }
}
