using System.Drawing;

namespace Accurate_throttle_for_aircraft.user_interfaces.creators.resources.interfaces
{
    internal interface IConfiguration
    {
        Color Color
        { get; set; }

        PointF Position
        { get; set; }
    }
}