using System;
using UnityEngine;

namespace MyGame.Characters.Player.FPSController
{
    [Serializable]
    public class MouseLook
    {
        public float xSensitivity = 2f;
        public float ySensitivity = 2f;
        public float minimumX = -90f;
        public float maximumX = 90f;
        public bool lockCursor = true;
        
        private float _currentYRot;
        private float _currentXRot;
        
        public void Init(Transform character, Transform camera)
        {
            _currentXRot = camera.transform.localEulerAngles.x;
            _currentYRot = character.localEulerAngles.y;
        }

        public void LookRotation(Transform character, Transform camera)
        {
            // value % 360 = make value in range from -360 to 360
            _currentYRot = (_currentYRot + Input.GetAxis("Mouse X") * xSensitivity) % 360;
            _currentXRot = Mathf.Clamp((_currentXRot + Input.GetAxis("Mouse Y") * ySensitivity) % 360, minimumX, maximumX);

            character.localRotation = Quaternion.Euler(0f, _currentYRot, 0f);
            camera.localRotation = Quaternion.Euler(-_currentXRot, 0f, 0f);
            
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
    }
}