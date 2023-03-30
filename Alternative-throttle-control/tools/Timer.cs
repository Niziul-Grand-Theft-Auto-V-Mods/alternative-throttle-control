using GTA;

namespace Alternative_throttle_control.tools
{
    internal sealed class Timer
    {
        private float _currentGameTime;

        private float _oldGameTime;

        public Timer()
        {
            _currentGameTime
                = Game
                    .GameTime;

            _oldGameTime
                = Game
                    .GameTime;
        }

        internal bool ReturnsTrueForEach(int milliseconds)
        {
            _currentGameTime
                = Game
                    .GameTime;

            if (!(_currentGameTime > _oldGameTime + milliseconds))
            {
                return false;
            }
            {
                _oldGameTime
                    = _currentGameTime;

                return true;
            }
        }
    }
}
