using Alternative_throttle_control.alternative_throttle_control;
using Alternative_throttle_control.user_interfaces;
using GTA;

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
                            if (_alternativeThrottleControl == null
                                ||
                                _userInterfaces == null)
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
                            if (_alternativeThrottleControl == null
                                ||
                                _userInterfaces == null)
                            {
                                return;
                            }

                            if (!_alternativeThrottleControl.IsPaused
                                ||
                                !_userInterfaces.IsPaused)
                            {
                                _alternativeThrottleControl
                                    .Pause();

                                _userInterfaces
                                    .Pause();
                            }
                        }
                        return;
                }

                Interval = 2000;
            };

            Aborted += (o, e) =>
            {
                if (_alternativeThrottleControl == null
                    ||
                    _userInterfaces == null)
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
