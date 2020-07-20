using MyGame.Enemies.FieldOfViewComponents;
using MyGame.Enemies.Scripts;
using UnityEngine;
using UnityEngine.AI;

namespace MyGame.Enemies.Zombies
{
    [RequireComponent(typeof(FieldOfView))]
    public class ZombieAi : Enemy
    {
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
        }


        private void Update()
        {
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