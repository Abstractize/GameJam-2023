using Fusion;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
    {
        public static NetworkPlayer Local { get; set; }
        [Header("Movement Values")]
        [SerializeField][Range(0, 0.99f)] private float _smoothing;
        [Header("Components")]
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _agent;
        [HideInInspector] public Vector2 MovementVector { get; set; }
        private Vector3 _lastDirection;
        private Vector3 _targetDirection;
        private float _lerpTime;

        private void Awake()
        {
            _agent ??= GetComponent<NavMeshAgent>();
            if (_animator == null)
                throw new System.Exception($"{nameof(_animator)} is not Implemented");
        }

        public override void Spawned()
        {
            if (Object.HasInputAuthority)
                Local = this;

        }

        public override void FixedUpdateNetwork()
        {
            Vector3 direction = new(MovementVector.x, 0, MovementVector.y);
            if (direction != _lastDirection)
                _lerpTime = 0;

            _lastDirection = direction;
            _targetDirection = Vector3.Lerp(_targetDirection, direction, Mathf.Clamp01(_lerpTime * (1 - _smoothing)));

            _agent.Move(_targetDirection * _agent.speed);

            _lerpTime += Time.deltaTime;
        }

        public override void Render()
        {
            _animator.SetFloat("X", MovementVector.x);
            _animator.SetFloat("Y", MovementVector.y);
        }

        public void PlayerLeft(PlayerRef player)
        {
            if (player == Object.InputAuthority)
                Runner.Despawn(Object);
        }
    }
}

