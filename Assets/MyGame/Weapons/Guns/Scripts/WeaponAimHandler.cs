using Extensions;
using UnityEngine;

public class WeaponAimHandler : MonoBehaviour
{
    [SerializeField] private float maxAimDistance = 30f;
    [SerializeField] private LayerMask aimLayer = default;
    [SerializeField] private new Camera camera = null;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float stopRotationDist = 1f;

    private void Awake()
    {
        camera = camera ? camera : Camera.main;
    }

    private void Update()
    {
        HandleWeaponAim();
    }

    private void HandleWeaponAim()
    {
        Vector3 origin = camera.transform.position;
        Vector3 direction = camera.transform.forward;
        
        if (Physics.Raycast(origin, direction, out RaycastHit hit, maxAimDistance, aimLayer))
        {
            float hitDist = hit.distance;

            if (hitDist >= stopRotationDist)
            {
                Vector3 directionToTarget = transform.DirectionTo(hit.point);
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
        Vector3 smoothDir = Vector3.Lerp(transform.forward, dirToTarget, t);
        transform.forward = smoothDir;
    }
}
