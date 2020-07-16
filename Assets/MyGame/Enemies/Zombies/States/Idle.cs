using System.Collections;
using MyGame.Enemies.AIStateMachine;
using MyGame.Enemies.FieldOfViewComponents;
using UnityEngine;

namespace MyGame.Enemies.Zombies.States
{
    public class Idle : IState
    {
        private Animator _animator;
        private ZombieAi _zombieAi;
        private FieldOfView _fieldOfView;

        private Coroutine _coroutine;
        
        public Idle (ZombieAi zombieAi, Animator animator, FieldOfView fieldOfView)
        {
            _zombieAi = zombieAi;
            _animator = animator;
            _fieldOfView = fieldOfView;
        }
        
        private IEnumerator FindTargetsWithDelay()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f / _fieldOfView.callsPerSecond);
                Transform target = _fieldOfView.FindVisibleTarget();
                
                if (target)
                {
                    _zombieAi.target = target;
                    CleanUpCoroutine();
                }
            }
        }

        private void CleanUpCoroutine()
        {
            if (_coroutine != null)
            {
                _zombieAi.StopCoroutine(_coroutine);
            }
        }
        
        public void Tick()
        {
        }

        public void OnEnter()
        {
            _animator.SetBool("MoveToTarget", false);
            _coroutine = _zombieAi.StartCoroutine(FindTargetsWithDelay());
        }

        public void OnExit()
        {
            CleanUpCoroutine();
        }
    }
}