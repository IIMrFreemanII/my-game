using UnityEngine;

namespace MyGame.Utils
{
    public class SmoothVelocityVector
    {
        private Vector3 _current;
        private Vector3 _currentVelocity;

        /// Returns the smoothed velocity.
        public Vector3 Update(Vector3 target, float smoothTime)
        {
            return _current = Vector3.SmoothDamp(_current, target, ref _currentVelocity, smoothTime);
        }

        public Vector3 Current
        {
            get => _current;
            set => _current = value;
        }
    }
}