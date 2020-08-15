using System;
using Extensions;
using UnityEngine;

namespace MyGame.Weapons.Guns.Scripts
{
    [Serializable]
    public class WeaponAimHandler
    {
        [SerializeField] private float maxAimDistance = 30f;
        [SerializeField] private LayerMask aimLayer = default;
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private float stopRotationDist = 1f;
        [SerializeField] private bool aim = true;
        private Camera _camera = null;
        private Transform _transform;

        public void Init(Transform transform, Camera camera)
        {
            _transform = transform;
            _camera = camera;
            aimLayer = LayerMask.GetMask("Default", "Enemy");
        }

        public void Update()
        {
            if (aim)
            {
                HandleWeaponAim();
            }
        }

        public void SetAim(bool aim)
        {
            this.aim = aim;
        }

        private void HandleWeaponAim()
        {
            Vector3 origin = _camera.transform.position;
            Vector3 direction = _camera.transform.forward;
        
            if (Physics.Raycast(origin, direction, out RaycastHit hit, maxAimDistance, aimLayer))
            {
                float hitDist = hit.distance;

                if (hitDist >= stopRotationDist)
                {
                    Vector3 directionToTarget = _transform.DirectionTo(hit.point);
                    RotateToDirection(directionToTarget);
                }
            }
            else
            {
                RotateToDirection(direction);
            }
        }

        private void RotateToDirection(Vector3 dirToTarget)
        {
            float t = rotationSpeed * Time.deltaTime;
            Vector3 smoothDir = Vector3.Lerp(_transform.forward, dirToTarget, t);
            _transform.forward = smoothDir;
        }
    }
}
