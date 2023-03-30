using System.Drawing;

using Alternative_throttle_control.user_interfaces.creators.resources.interfaces;


namespace Alternative_throttle_control.user_interfaces.creators.resources.structs
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
