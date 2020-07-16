using System.Collections;
using MyGame.Enemies.AIStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace MyGame.Enemies.Zombies.States
{
    public class MoveToTarget : IState
    {
        private Animator _animator;
        private ZombieAi _zombieAi;
        private NavMeshAgent _agent;
        private Transform _transform;

        private Vector3 _lastTargetPos;

        private Coroutine _coroutine;
        
        public MoveToTarget(ZombieAi zombieAi, Animator animator, NavMeshAgent agent)
        {
            _zombieAi = zombieAi;
            _animator = animator;
            _agent = agent;
            _transform = zombieAi.transform;
        }

        private IEnumerator HandleDestWithDelay()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f / 2f);

                if (TargetHasMoved())
                {
                    _agent.SetDestination(_lastTargetPos);
                }
            }
        }

        private bool TargetHasMoved()
        {
            if (_zombieAi.target)
            {
                Vector3 currentTargetPos = _zombieAi.target.position;
                if (currentTargetPos != _lastTargetPos)
                {
                    _lastTargetPos = currentTargetPos;
                    return true;
                }
            }

            return false;
        }
        
        public void Tick()
        {
            float distanceToTarget = Vector3.Distance(_zombieAi.target.position, _transform.position);
            if (distanceToTarget <= _agent.stoppingDistance)
            {
                if (_agent.velocity.sqrMagnitude <= (Vector3.zero / 2).sqrMagnitude)
                {
                    _zombieAi.canAttack = true;
                }
            }
        }

        public void OnEnter()
        {
            _zombieAi.MoveToTarget = true;
            _coroutine = _zombieAi.StartCoroutine(HandleDestWithDelay());
        }

        public void OnExit()
        {
            _zombieAi.MoveToTarget = false;
            _zombieAi.StopCoroutine(_coroutine);
        }
    }
}