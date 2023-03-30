using Alternative_throttle_control.user_interfaces.interfaces;
using GTA;

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
                if (Game
                        .IsControlJustPressed(Control
                                                .VehicleExit))
                {
                    Wait(5000);

                    return;
                }

                throttleInterface
                    .BuildTheInterface();

                throttleInterface
                    .ShowTheInterface();
            };

            Aborted += (o, e) =>
            {
                throttleInterface
                    .RemoveTheInterface();
            };
        }
    }
}
