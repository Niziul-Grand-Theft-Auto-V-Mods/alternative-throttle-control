using System.Drawing;

using Alternative_throttle_control.user_interfaces.creators.resources.interfaces;


namespace Alternative_throttle_control.user_interfaces.creators.resources.structs
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
