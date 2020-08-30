﻿using System;
using UnityEngine;

namespace MyGame.Characters.Player.FPSController
{
    [Serializable]
    public class HeadBob
    {
        [SerializeField] private float headBobAmountX = 0.125f;
        [SerializeField] private float headBobAmountY = 0.075f;
        [SerializeField] private float eyeHeightRacio = 1.1f;
        [SerializeField] private float currentAimRacio = 1f;
        
        private Transform _parent;
        private Transform _camera;
    
        public void Init(Transform parent, Transform camera)
        {
            _parent = parent;
            _camera = camera;
        }

        public void Update(float bobStep)
        {
            Vector3 cameraLocalPosition = _camera.localPosition;
            cameraLocalPosition.x = Mathf.Sin(bobStep) * headBobAmountX * currentAimRacio;
            cameraLocalPosition.y = (Mathf.Cos(bobStep * 2) * headBobAmountY * currentAimRacio) +
                (_parent.localScale.y * eyeHeightRacio) - (_parent.localScale.y / 2);

            _camera.localPosition = cameraLocalPosition;
        }
    }
}
