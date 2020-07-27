using Extensions;
using UnityEngine;

public class WeaponAimHandler : MonoBehaviour
{
    [SerializeField] private float maxAimDistance;
    [SerializeField] private LayerMask aimLayer;
    [SerializeField] private Camera camera;

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
            Vector3 directionToTarget = transform.DirectionTo(hit.point);
            
            RotateToDirection(directionToTarget);
        }
        else
        {
            RotateToDirection(direction);
        }
    }

    private void RotateToDirection(Vector3 dirToTarget)
    {
        transform.forward = dirToTarget;
    }
}
