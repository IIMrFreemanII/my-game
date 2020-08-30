using System;
using Extensions;
using UnityEngine;

namespace MyGame.Weapons.Scripts
{
    [Serializable]
    public class HandsBob
    {
        [SerializeField] private Transform hands = null;
        [SerializeField] private float bobAmountX = 0.01f;
        [SerializeField] private float bobAmountY = 0.005f;
        private float _currentHandBobX;
        private float _currentHandBobY;
        [SerializeField, Range(0f, 1f)] private float racioHipHold = 1f;

        public void Update(float bobStep)
        {
            _currentHandBobX = Mathf.Sin(bobStep) * bobAmountX * racioHipHold;
            _currentHandBobY = Mathf.Cos(bobStep * 2) * bobAmountY * -1 * racioHipHold;

            hands.localPosition = hands.localPosition.With(_currentHandBobX, _currentHandBobY);
        }
    }
}