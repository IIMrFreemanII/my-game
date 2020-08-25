using System;
using Extensions;
using UnityEngine;

namespace MyGame.Characters.Player.FPSController
{
    [Serializable]
    public class Movement
    {
        public float walkSpeed = 2f;
        
        private CapsuleCollider _capsuleCollider = null;
        private Rigidbody _rb = null;
        private Transform _character;
        
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
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 moveDir = new Vector3(horizontal, 0, vertical).normalized;
                
            // desired move direction relative to the current rotation
            // to get direction relative to the current rotation:
            // 1. transform.rotation * moveDir;
            // or
            // 2. transform.TransformDirection(moveDir);
            Vector3 movement = (_character.rotation * moveDir) * walkSpeed;
            _rb.velocity = movement.With(y: _rb.velocity.y); 
            
            HandleJump();
        }
        
        private void HandleJump()
        {
            float jump = Input.GetAxis("Jump");

            if (IsGrounded && jump > 0)
            {
                _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}