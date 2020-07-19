using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEditor;
using UnityEngine;

namespace MyGame.Enemies.BehaviorTreeTasks.Conditions
{
    [TaskCategory("ZombieAi")]
    public class CanSeeTarget : Conditional
    {
        [BehaviorDesigner.Runtime.Tasks.Tooltip("The field of view angle of the agent (in degrees)")]
        public SharedFloat viewAngle = 90f;
        [BehaviorDesigner.Runtime.Tasks.Tooltip("The distance that the agent can see")]
        public SharedFloat viewRadius = 15f;
        [BehaviorDesigner.Runtime.Tasks.Tooltip("The target transform that is within sight")]
        public SharedTransform returnedTargetTransform;
        
        public SharedLayerMask targetMask;
        public SharedLayerMask obstacleMask;
        
        public SharedInt callsPerSecond = 3;

        private Coroutine _seekCoroutine;

        public override void OnStart()
        {
            _seekCoroutine = Owner.StartCoroutine(SeekTargetWithDelay());
        }
        
        public override TaskStatus OnUpdate()
        {
            if (returnedTargetTransform.Value != null) {
                return TaskStatus.Success;
            }
            return TaskStatus.Running;
        }
        
        private IEnumerator SeekTargetWithDelay()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f / callsPerSecond.Value);
                returnedTargetTransform.Value = SeekTarget();
                
                if (returnedTargetTransform.Value)
                {
                    CleanUpSeekTargetCoroutine();
                }
            }
        }
        
        private void CleanUpSeekTargetCoroutine()
        {
            if (_seekCoroutine != null)
            {
                Owner.StopCoroutine(_seekCoroutine);
            }
        }
        
        private Transform SeekTarget()
        {
            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius.Value, targetMask.Value);

            foreach (Collider targetCollider in targetsInViewRadius)
            {
                Transform target = targetCollider.transform;

                if (transform == target) continue;
                
                Vector3 dirToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle.Value / 2)
                {
                    float distToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask.Value))
                    {
                        return target;
                    }
                }
            }
            return null;
        }

        public override void OnDrawGizmos()
        {
#if UNITY_EDITOR
            Handles.color = Color.white;
            Handles.DrawWireArc(Owner.transform.position, Vector3.up, Vector3.forward, 360, viewRadius.Value);
            
            Vector3 viewAngleA = DirFromAngle(-viewAngle.Value / 2, false);
            Vector3 viewAngleB = DirFromAngle(viewAngle.Value / 2, false);
            
            Handles.DrawLine(Owner.transform.position, Owner.transform.position + viewAngleA * viewRadius.Value);
            Handles.DrawLine(Owner.transform.position, Owner.transform.position + viewAngleB * viewRadius.Value);
#endif
        }

        private Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal) angleInDegrees += Owner.transform.eulerAngles.y;
            
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}