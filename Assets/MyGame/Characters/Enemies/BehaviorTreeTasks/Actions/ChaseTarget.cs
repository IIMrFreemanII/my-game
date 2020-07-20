using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace MyGame.Enemies.BehaviorTreeTasks.Actions
{
    [TaskCategory("ZombieAi")]
    public class ChaseTarget : Action
    {
        public SharedTransform target;
        public SharedFloat distanceToStop;
        
        private NavMeshAgent _agent;
        private Vector3 _lastTargetPos;

        public SharedInt callsPerSecond = 3;
        
        private Coroutine _chaseCoroutine;

        public override void OnAwake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.stoppingDistance = distanceToStop.Value;
        }

        public override void OnStart()
        {
            _chaseCoroutine = Owner.StartCoroutine(ChaseTargetWithDelay());
        }

        public override TaskStatus OnUpdate()
        {
            if (target.Value == null)
            {
                return TaskStatus.Failure;
            }
            
            float distanceToTarget = Vector3.Distance(target.Value.position, transform.position);
            if (distanceToTarget <= _agent.stoppingDistance) {
                if (_agent.velocity.sqrMagnitude <= Vector3.zero.sqrMagnitude)
                {
                    CleanUpChaseTargetCoroutine();
                    return TaskStatus.Success;
                }
            }
            return TaskStatus.Running;
        }
        
        private IEnumerator ChaseTargetWithDelay()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f / callsPerSecond.Value);

                if (TargetHasMoved())
                {
                    _agent.SetDestination(_lastTargetPos);
                }
            }
        }

        private void CleanUpChaseTargetCoroutine()
        {
            if (_chaseCoroutine != null)
            {
                Owner.StopCoroutine(_chaseCoroutine);
            }
        }
        
        private bool TargetHasMoved()
        {
            if (target.Value)
            {
                Vector3 currentTargetPos = target.Value.position;
                if (currentTargetPos != _lastTargetPos)
                {
                    _lastTargetPos = currentTargetPos;
                    return true;
                }
            }

            return false;
        }
    }
}