using System;
using UnityEngine;

namespace MyGame.Characters.Player.FPSController
{
    [Serializable]
    public class MouseLook
    {
        [Tooltip("Approximately the amount of time it will take for the fps controller to reach maximum rotation speed.")]
        public float rotationSmoothness = 0.05f;
        
        public float xSensitivity = 2f;
        public float ySensitivity = 2f;
        public float minimumX = -90f;
        public float maximumX = 90f;
        public bool lockCursor = true;
        
        private float _currentYRot;
        private float _currentXRot;
        private SmoothRotation _rotationX;
        private SmoothRotation _rotationY;
        
        public void Init(Transform character, Transform camera)
        {
            _currentXRot = camera.transform.localEulerAngles.x;
            _currentYRot = character.localEulerAngles.y;
            _rotationX = new SmoothRotation(_currentXRot);
            _rotationY = new SmoothRotation(_currentYRot);
        }

        public void LookRotation(Transform character, Transform camera)
        {
            // value % 360 = make value in range from -360 to 360
            _currentYRot = (_currentYRot + FpsInput.MouseX * xSensitivity) % 360;
            _currentXRot = Mathf.Clamp((_currentXRot + FpsInput.MouseY * ySensitivity) % 360, minimumX, maximumX);

            float smoothYRot = _rotationY.Update(_currentYRot, rotationSmoothness);
            float smoothXRot = _rotationX.Update(_currentXRot, rotationSmoothness);

            character.localRotation = Quaternion.Euler(0f, smoothYRot, 0f);
            camera.localRotation = Quaternion.Euler(-smoothXRot, 0f, 0f);
            
            UpdateCursorLock();
        }

        private void UpdateCursorLock()
        {
            if (lockCursor && Cursor.lockState != CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                return;
            }

            if (!lockCursor && Cursor.lockState != CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.None;
                return;
            }
        }
        
        /// A helper for assistance with smoothing the camera rotation.
        private class SmoothRotation
        {
            private float _current;
            private float _currentVelocity;

            public SmoothRotation(float startAngle)
            {
                _current = startAngle;
            }
				
            /// Returns the smoothed rotation.
            public float Update(float target, float smoothTime)
            {
                return _current = Mathf.SmoothDampAngle(_current, target, ref _currentVelocity, smoothTime);
            }

            public float Current
            {
                set { _current = value; }
            }
        }
    }
}