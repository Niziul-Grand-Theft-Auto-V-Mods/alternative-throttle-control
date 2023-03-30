using GTA;

namespace Accurate_throttle_for_aircraft
{
    internal sealed class Main : Script
    {
        public Main()
        {
            var throttle
                = 0.25f;

            Tick += (o, e) =>
            {
                if (Game.Player.Character.IsInFlyingVehicle)
                {
                    Game
                        .SetControlValueNormalized(Control
                                                        .VehicleFlyThrottleUp, throttle);

                    if (Game.IsKeyPressed(System.Windows.Forms.Keys.Oemplus))
                    {
                        if (throttle > 1f)
                            throttle = 1f;

                        if (throttle < 1f)
                            throttle += 0.0065f;
                    }

                    if (Game.IsKeyPressed(System.Windows.Forms.Keys.OemMinus))
                    {
                        if (throttle < 0.25f)
                            throttle = 0.25f;

                        if (throttle > 0.25f)
                            throttle -= 0.0065f;
                    }

                    return;
                }

                if (throttle != 0.25f)
                    throttle = 0.25f;
            };
        }
    }
}
