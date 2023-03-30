using Alternative_throttle_control.settings;
using GTA;
using System.Windows.Forms;
using GTAControl
      = GTA.Control;


namespace Alternative_throttle_control.script
{
    [ScriptAttributes(NoDefaultInstance = true)]
    internal sealed class AlternativeThrottleControl : Script
    {
        public AlternativeThrottleControl()
        {
            var keyThatDeterminesTheIncreaseOrDecreaseOfTheValues
                = Keys.NumPad3;

            var settingsManager
                = new SettingsManager();

            var keyForTheThrottleUp
                = settingsManager
                    .ReturnTheKeyForTheThrottleIncrease();

            var keyForTheThrottleDown
                = settingsManager
                    .ReturnTheKeyForTheThrottleDecrease();

            var throttleSensitivity
                = settingsManager
                    .ReturnTheThrottleSensitivity();

            var throttleUpValue
                = 0.25f;

            var throttleDownValue
                = 0.25f;

            Tick    += (o, e) =>
            {
                if (Game
                        .IsControlJustPressed(GTAControl
                                                    .VehicleExit))
                {
                    IdleThrottle();
                }

                Game
                    .SetControlValueNormalized(GTAControl
                                                    .VehicleFlyThrottleUp, throttleUpValue);
                Game
                    .SetControlValueNormalized(GTAControl
                                                    .VehicleFlyThrottleDown, throttleDownValue);

                if (Game
                        .IsKeyPressed(keyForTheThrottleUp))
                {
                    IncreaseThrottle();

                    return;
                }

                if (Game
                        .IsKeyPressed(keyForTheThrottleDown))
                {
                    DecreaseThrottle();

                    return;
                }
            };

            KeyDown += (o, e) =>
            {
                if (e.KeyCode == keyForTheThrottleUp
                    ||
                    e.KeyCode == keyForTheThrottleDown)
                {
                    if (keyThatDeterminesTheIncreaseOrDecreaseOfTheValues == Keys.NumPad3)
                    {
                        keyThatDeterminesTheIncreaseOrDecreaseOfTheValues = e.KeyCode;
                    }
                }
            };

            void IdleThrottle()
            {
                if (throttleUpValue != 0.25f)
                    throttleUpValue = 0.25f;

                if (throttleDownValue != 0.25f)
                    throttleDownValue = 0.25f;
            }

            void IncreaseThrottle()
            {
                if (keyThatDeterminesTheIncreaseOrDecreaseOfTheValues == keyForTheThrottleUp)
                {
                    IncreaseThrottleUp();
                }

                if (keyThatDeterminesTheIncreaseOrDecreaseOfTheValues == keyForTheThrottleDown)
                {
                    DecreaseThrottleUp();
                }

                return;

                void IncreaseThrottleUp()
                {
                    if (throttleUpValue > 1f)
                        throttleUpValue = 1f;

                    if (throttleUpValue < 1f)
                        throttleUpValue += throttleSensitivity + (Game.LastFrameTime / 5f);
                }

                void DecreaseThrottleUp()
                {
                    if (throttleDownValue < 0.25f)
                        throttleDownValue = 0.25f;

                    if (throttleDownValue == 0.25f)
                    {
                        Yield();

                        keyThatDeterminesTheIncreaseOrDecreaseOfTheValues = Keys.NumPad3;
                    }

                    if (throttleDownValue > 0.25f)
                        throttleDownValue -= throttleSensitivity + (Game.LastFrameTime / 5f);
                }
            }

            void DecreaseThrottle()
            {
                if (keyThatDeterminesTheIncreaseOrDecreaseOfTheValues == keyForTheThrottleDown)
                {
                    IncreaseThrottleDown();
                }

                if (keyThatDeterminesTheIncreaseOrDecreaseOfTheValues == keyForTheThrottleUp)
                {
                    DecreaseThrottleDown();
                }

                return;

                void IncreaseThrottleDown()
                {
                    if (throttleDownValue > 1f)
                        throttleDownValue = 1f;

                    if (throttleDownValue < 1f)
                        throttleDownValue += throttleSensitivity + (Game.LastFrameTime / 5f);
                }

                void DecreaseThrottleDown()
                {
                    if (throttleUpValue < 0.25f)
                        throttleUpValue = 0.25f;

                    if (throttleUpValue == 0.25f)
                    {
                        Yield();

                        keyThatDeterminesTheIncreaseOrDecreaseOfTheValues = Keys.NumPad3;
                    }

                    if (throttleUpValue > 0.25f)
                        throttleUpValue -= throttleSensitivity + (Game.LastFrameTime / 5f);
                }
            }
        }
    }
}