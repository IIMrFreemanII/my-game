using UnityEngine;

namespace MyGame.Enemies.FieldOfViewComponents
{
    public class FieldOfView : MonoBehaviour
    {
        public float viewRadius = 3f;
        [Range(0, 360)]
        public float viewAngle = 60f;

        public LayerMask targetMask;
        public LayerMask obstacleMask;

        public Transform FindVisibleTarget()
        {
            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

            foreach (Collider targetCollider in targetsInViewRadius)
            {
                Transform target = targetCollider.transform;

                if (transform == target) continue;
                
                Vector3 dirToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                {
                    float distToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                    {
                        return target;
                    }
                }
            }
            return null;
        }

        public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal) angleInDegrees += transform.eulerAngles.y;
            
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}