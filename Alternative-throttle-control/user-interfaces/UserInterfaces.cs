using Alternative_throttle_control.settings;
using Alternative_throttle_control.user_interfaces.creators.resources.structs;
using Alternative_throttle_control.user_interfaces.interfaces;
using Alternative_throttle_control.user_interfaces.managers;
using GTA;
using GTA.UI;
using System.Drawing;

namespace Alternative_throttle_control.user_interfaces
{
    [ScriptAttributes(NoDefaultInstance = true)]
    internal sealed class UserInterfaces : Script
    {
        public UserInterfaces()
        {
            var throttleInterface
                = new ThrottleInterface();

            Tick    += (o, e) =>
            {
                if (Game.Player.Character.IsInFlyingVehicle)
                {
                    throttleInterface
                        .BuildTheInterface();

                    throttleInterface
                        .ShowTheInterface();

                    return;
                }

                throttleInterface
                    .RemoveTheInterface();
            };

            Aborted += (o, e) =>
            {
                throttleInterface
                    .RemoveTheInterface();
            };
        }
    }
}
