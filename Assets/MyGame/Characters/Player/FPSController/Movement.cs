using System;
using Extensions;
using UnityEngine;

namespace MyGame.Characters.Player.FPSController
{
    [Serializable]
    public class Movement
    {
        [Tooltip("Approximately the amount of time it will take for the player to reach maximum running or walking speed.")]
        public float movementSmoothness = 0.125f;
        
        public float walkSpeed = 2f;
        
        private CapsuleCollider _capsuleCollider = null;
        private Rigidbody _rb = null;
        private Transform _character;

        private SmoothVelocityVector _smoothVelocityVector = new SmoothVelocityVector();

        [SerializeField] private LayerMask jumpGroundLayer = default;
        
        [SerializeField] private float jumpForce = 12f;
        [SerializeField] [Range(0f, 1f)] private float groundCheckRadius = 0.05f;
        
        private bool IsGrounded
        {
            get
            {
                Vector3 bottomCenterPoint = new Vector3(_capsuleCollider.bounds.center.x, _capsuleCollider.bounds.min.y,
                    _capsuleCollider.bounds.center.z);

                float decreasedRadius = _capsuleCollider.bounds.size.x / 2 * groundCheckRadius;
                return Physics.CheckCapsule(_capsuleCollider.bounds.center, bottomCenterPoint, decreasedRadius,
                    jumpGroundLayer);
            }
        }
        
        public void Init(Transform character)
        {
            _character = character;
            _rb = character.GetComponent<Rigidbody>();
            _capsuleCollider = character.GetComponent<CapsuleCollider>();
            
            _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ |
                             RigidbodyConstraints.FreezeRotationY;
            
            if (jumpGroundLayer == character.gameObject.layer)
            {
                Debug.LogError("Player SortingLayer must be different from Ground Sorting Layer!");
            }
        }

        public void UpdateMovement()
        {
            float horizontal = FpsInput.Horizontal;
            float vertical = FpsInput.Vertical;

            Vector3 moveDir = new Vector3(horizontal, 0, vertical).normalized;
                
            // desired move direction relative to the current rotation
            // to get direction relative to the current rotation:
            // 1. transform.rotation * moveDir;
            // or
            // 2. transform.TransformDirection(moveDir);
            Vector3 movement = (_character.rotation * moveDir) * walkSpeed;
            Vector3 smoothMovement = _smoothVelocityVector.Update(movement, movementSmoothness);

            _rb.velocity = smoothMovement.With(y: _rb.velocity.y);
            
            HandleJump();
        }
        
        private void HandleJump()
        {
            if (IsGrounded && FpsInput.Jump)
            {
                _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
        
        /// A helper for assistance with smoothing the movement.
        private class SmoothVelocityVector
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
                set { _current = value; }
            }
        }
    }
}