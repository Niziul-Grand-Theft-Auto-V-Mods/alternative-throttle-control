using Alternative_throttle_control.settings;
using GTA;
using System.Windows.Forms;
using CTimer
      = Alternative_throttle_control.tools.Timer;
using GTAControl
      = GTA.Control;


namespace Alternative_throttle_control.alternative_throttle_control
{
    [ScriptAttributes(NoDefaultInstance = true)]
    internal sealed class AlternativeThrottleControl : Script
    {
        public AlternativeThrottleControl()
        {
            var keyThatDeterminesTheIncreaseOrDecreaseOfTheValues
                = Keys.NumPad3;

            var timer
                = new CTimer();

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

            Tick += (o, e) =>
            {
                switch (Game.Player.Character.IsInFlyingVehicle)
                {
                    case false:
                        {
                            IdleThrottle();
                        }
                        return;
                    case true:
                        {
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
                        }
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
                    ThrottleUp();
                }

                if (keyThatDeterminesTheIncreaseOrDecreaseOfTheValues == keyForTheThrottleDown)
                {
                    ThrottleDown();
                }

                return;

                void ThrottleUp()
                {
                    if (throttleUpValue > 1f)
                        throttleUpValue = 1f;

                    if (throttleUpValue < 1f)
                        throttleUpValue += throttleSensitivity;
                }

                void ThrottleDown()
                {
                    if (throttleDownValue < 0.25f)
                        throttleDownValue = 0.25f;

                    if (timer
                            .ReturnsTrueForEach(1))
                    {
                        if (throttleDownValue == 0.25f)
                            keyThatDeterminesTheIncreaseOrDecreaseOfTheValues = Keys.NumPad3;
                    }

                    if (throttleDownValue > 0.25f)
                        throttleDownValue -= throttleSensitivity;
                }
            }

            void DecreaseThrottle()
            {
                if (keyThatDeterminesTheIncreaseOrDecreaseOfTheValues == keyForTheThrottleUp)
                {
                    ThrottleDown();
                }

                if (keyThatDeterminesTheIncreaseOrDecreaseOfTheValues == keyForTheThrottleDown)
                {
                    ThrottleUp();
                }

                return;

                void ThrottleDown()
                {
                    if (throttleUpValue < 0.25f)
                        throttleUpValue = 0.25f;

                    if (timer
                            .ReturnsTrueForEach(1))
                    {
                        if (throttleUpValue == 0.25f)
                            keyThatDeterminesTheIncreaseOrDecreaseOfTheValues = Keys.NumPad3;
                    }

                    if (throttleUpValue > 0.25f)
                        throttleUpValue -= throttleSensitivity;
                }

                void ThrottleUp()
                {
                    if (throttleDownValue > 1f)
                        throttleDownValue = 1f;

                    if (throttleDownValue < 1f)
                        throttleDownValue += throttleSensitivity;
                }
            }
        }
    }
}