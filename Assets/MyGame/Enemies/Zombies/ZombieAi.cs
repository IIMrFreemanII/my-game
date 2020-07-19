using System;
using MyGame.Enemies.AIStateMachine;
using MyGame.Enemies.FieldOfViewComponents;
using MyGame.Enemies.Scripts;
using MyGame.Enemies.Zombies.States;
using UnityEngine;
using UnityEngine.AI;

namespace MyGame.Enemies.Zombies
{
    [RequireComponent(typeof(FieldOfView))]
    public class ZombieAi : Enemy
    {
        private StateMachine _stateMachine;
        [SerializeField] private Animator animator;

        private bool _moveToTarget;

        public bool MoveToTarget
        {
            get => _moveToTarget;
            set
            {
                if (_moveToTarget != value)
                {
                    animator.SetBool(MoveToTargetAnimId, value);
                    _moveToTarget = value;
                }
            }
        }

        public bool canAttack;

        private FieldOfView _fieldOfView;

        private Camera _camera;
        private NavMeshAgent _agent;

        private static readonly int MoveToTargetAnimId = Animator.StringToHash("MoveToTarget");

        public Transform target;
        public bool isDead;

        public float distanceToTargetCompensator = 0.2f;

        private void Awake()
        {
            _fieldOfView = GetComponent<FieldOfView>();
            animator = animator ? animator : GetComponent<Animator>();
            _camera = Camera.main;
            _agent = GetComponent<NavMeshAgent>();

            InitStateMachine();
        }

        private void InitStateMachine()
        {
            _stateMachine = new StateMachine();

            void AddTransition(IState from, IState to, Func<bool> condition) =>
                _stateMachine.AddTransition(from, to, condition);

            void AddAnyTransition(IState state, Func<bool> condition) =>
                _stateMachine.AddAnyTransition(state, condition);

            Func<bool> HasTarget() => () => target != null;
            Func<bool> HasNoTarget() => () => target == null;
            Func<bool> CanAttack() => () => canAttack;
            Func<bool> CanNotAttack() => () => !canAttack;

            Idle idle = new Idle(this, animator, _fieldOfView);
            MoveToTarget moveToTarget = new MoveToTarget(this, _agent);
            Attack attack = new Attack(this, animator, _agent);

            AddTransition(idle, moveToTarget, HasTarget());
            AddTransition(moveToTarget, idle, HasNoTarget());
            AddTransition(attack, idle, CanNotAttack());
            
            AddAnyTransition(attack, CanAttack());

            _stateMachine.SetEntryPoint(idle);
        }

        private void Update()
        {
            if (isDead) return;
            
            _stateMachine.Tick();
        }

        private void IsFighting(int boolValue)
        {
            animator.SetBool("IsFighting", boolValue == 1);
        }

        [ContextMenu("Die")]
        public void Die()
        {
            animator.SetTrigger("Die");
            _agent.enabled = false;
            isDead = true;
        }
    }
}