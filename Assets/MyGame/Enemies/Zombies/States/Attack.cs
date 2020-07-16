using System.Collections;
using MyGame.Enemies.AIStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace MyGame.Enemies.Zombies.States
{
    public class Attack : IState
    {
        private Animator _animator;
        private ZombieAi _zombieAi;
        private NavMeshAgent _agent;
        private Transform _transform;

        private Coroutine _coroutine;
        
        public Attack(ZombieAi zombieAi, Animator animator, NavMeshAgent agent)
        {
            _zombieAi = zombieAi;
            _animator = animator;
            _agent = agent;
            _transform = zombieAi.transform;
        }

        public void Tick()
        {
            HandleRotation();
        }

        private void HandleAttack()
        {
            if (_zombieAi.target)
            {
                float distanceToTarget = Vector3.Distance(_zombieAi.target.position, _transform.position);
                if (distanceToTarget <= _agent.stoppingDistance)
                {
                    if (!_animator.GetBool("IsFighting")) _animator.SetTrigger("Attack");
                }
                else
                {
                    SetCanNotAttack();
                }
            }
            else
            {
                SetCanNotAttack();
            }
        }

        private IEnumerator HandleAttackWithDelay()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f / 3f);
                HandleAttack();
            }
        }

        private void SetCanNotAttack()
        {
            if (!_animator.GetBool("IsFighting"))
            {
                _zombieAi.canAttack = false;
            }
        }
        
        private void HandleRotation()
        {
            Vector3 dir = _zombieAi.target.transform.position - _zombieAi.transform.position;
            _zombieAi.transform.forward = new Vector3(dir.x, 0, dir.z);
        }

        public void OnEnter()
        {
            _coroutine = _zombieAi.StartCoroutine(HandleAttackWithDelay());
        }

        public void OnExit()
        {
            _zombieAi.StopCoroutine(_coroutine);
        }
    }
}