using System.Drawing;

using Accurate_throttle_for_aircraft.user_interfaces.creators.resources.interfaces;


namespace Accurate_throttle_for_aircraft.user_interfaces.creators.resources.structs
{
    internal struct StTextElementConfiguration : IConfiguration
    {
        public Color Color
        { get; set; }

        public PointF Position
        { get; set; }

        internal float Scale
        { get; set; }

        internal string Caption
        { get; set; }
    }
}
