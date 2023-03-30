using GTA;

using GTAControl
      = GTA.Control;

using System.Windows.Forms;

using Alternative_throttle_control.settings;

namespace Alternative_throttle_control
{
    internal sealed class Main : Script
    {
        private readonly Keys _keyForThrottleIncrease;

        private readonly Keys _keyForThrottleDecrease;

        private float _throttleSensitivity;

        private float _throttleValue;
        
        public Main()
        {
            var settingsManager
                = new SettingsManager();
            
            _keyForThrottleIncrease
                = settingsManager
                        .ReturnTheKeyForTheThrottleIncrease();

            _keyForThrottleDecrease
                = settingsManager
                        .ReturnTheKeyForTheThrottleDecrease();

            _throttleSensitivity
                = settingsManager
                        .ReturnTheThrottleSensitivity();

            _throttleValue
                = 0.25f;

            Tick    += (o, e) =>
            {
                if (Game.IsLoading)
                {
                    return;
                }    

                switch (Game.Player.Character.IsInFlyingVehicle)
                {
                    case true:
                        {
                            Game
                                .SetControlValueNormalized(GTAControl
                                                                .VehicleFlyThrottleUp, _throttleValue);

                            if (Game
                                    .IsKeyPressed(_keyForThrottleIncrease))
                            {
                                IncreaseThrottle();
                            }

                            if (Game
                                    .IsKeyPressed(_keyForThrottleDecrease))
                            {
                                DecreaseThrottle();
                            }
                        }
                        return;
                    case false:
                        {
                            IdleThrottle();
                        }
                        return;
                }
            };
        }

        private void IncreaseThrottle()
        {
            if (_throttleValue > 1f)
                _throttleValue = 1f;

            if (_throttleValue < 1f)
                _throttleValue += _throttleSensitivity;
        }

        private void DecreaseThrottle()
        {
            if (_throttleValue < 0.25f)
                _throttleValue = 0.25f;

            if (_throttleValue > 0.25f)
                _throttleValue -= _throttleSensitivity;
        }
        
        private void IdleThrottle()
        {
            if (_throttleValue != 0.25f)
                _throttleValue = 0.25f;
        }
    }   
}