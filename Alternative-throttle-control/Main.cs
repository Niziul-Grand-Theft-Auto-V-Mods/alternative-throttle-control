using Alternative_throttle_control.script;
using Alternative_throttle_control.user_interfaces;
using GTA;
using GTA.UI;

namespace Alternative_throttle_control
{
    internal sealed class Main : Script
    {
        private AlternativeThrottleControl _alternativeThrottleControl;

        private UserInterfaces _userInterfaces;

        public Main()
        {
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
                            if (_alternativeThrottleControl is null
                                ||
                                _userInterfaces is null)
                            {
                                _alternativeThrottleControl
                                    = InstantiateScript<AlternativeThrottleControl>();

                                _userInterfaces
                                    = InstantiateScript<UserInterfaces>();

                                _alternativeThrottleControl
                                    .Resume();

                                _userInterfaces
                                    .Resume();
                            }

                            if (_alternativeThrottleControl.IsPaused
                                ||
                                _userInterfaces.IsPaused)
                            {
                                _alternativeThrottleControl
                                    .Resume();

                                _userInterfaces
                                    .Resume();
                            }
                        }
                        return;
                    case false:
                        {
                            if (_alternativeThrottleControl is null
                                ||
                                _userInterfaces is null)
                            {
                                return;
                            }

                            if (_alternativeThrottleControl.IsPaused
                                ||
                                _userInterfaces.IsPaused)
                            {
                                return;
                            }

                            _alternativeThrottleControl
                                .Pause();

                            _userInterfaces
                                .Pause();
                        }
                        return;
                }

                Interval = 2000;
            };

            Aborted += (o, e) =>
            {
                if (_alternativeThrottleControl is null
                    ||
                    _userInterfaces is null)
                {
                    return;
                }

                _alternativeThrottleControl
                    .Abort();

                _userInterfaces
                    .Abort();

                _alternativeThrottleControl
                    = null;

                _userInterfaces
                    = null;
            };
        }
    }
}
