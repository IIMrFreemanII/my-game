using System;
using UnityEngine;

namespace MyGame.Characters.Player.FPSController
{
    [Serializable]
    public class BobStepCounter
    {
        [SerializeField] private float bobSpeed = 1f;
        private float _count;
        public float Count => _count;
        
        private Vector3 _parentLastPos;
        private Transform _parent;

        public void Init(Transform parent)
        {
            _parentLastPos = parent.position;
            _parent = parent;
        }

        public void Update(bool grounded)
        {
            if (grounded)
            {
                _count += Vector3.Distance(_parentLastPos, _parent.position) * bobSpeed;
            }
            
            _parentLastPos = _parent.position;
        }
    }
}