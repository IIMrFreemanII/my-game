using System;
using Extensions;
using UnityEngine;

namespace MyGame.Characters.Player.FPSController
{
    [Serializable]
    public class HandsBob
    {
        [SerializeField] private Transform hands = null;
        [SerializeField] private float bobWalkAmountX = 0.01f;
        [SerializeField] private float bobWalkAmountY = 0.005f;
        [SerializeField] private float bobRunAmountX = 0.05f;
        [SerializeField] private float bobRunAmountY = 0.025f;
        private float _currentHandBobX;
        private float _currentHandBobY;
        [SerializeField, Range(0f, 1f)] private float racioHipHold = 1f;

        public void Update(float bobStep)
        {
            float bobAmountX = FpsInput.Run ? bobRunAmountX : bobWalkAmountX;
            float bobAmountY = FpsInput.Run ? bobRunAmountY : bobWalkAmountY;
            
            _currentHandBobX = Mathf.Sin(bobStep) * bobAmountX * racioHipHold;
            _currentHandBobY = Mathf.Cos(bobStep * 2) * bobAmountY * -1 * racioHipHold;

            hands.localPosition = hands.localPosition.With(_currentHandBobX, _currentHandBobY);
        }
    }
}