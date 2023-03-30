using System.Drawing;

using Accurate_throttle_for_aircraft.user_interfaces.creators.resources.interfaces;


namespace Accurate_throttle_for_aircraft.user_interfaces.creators.resources.structs
{
    internal struct StCustomSpriteConfiguration : IConfiguration
    {
        public SizeF Size
        { get; set; }

        public Color Color
        { get; set; }

        public PointF Position
        { get; set; }
        
        internal string Filename
        { get; set; }
    }
}
