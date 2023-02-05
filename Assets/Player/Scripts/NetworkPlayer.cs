using Fusion;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(NetworkPosition))]
    public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
    {
        public static NetworkPlayer Local { get; set; }
        [Header("Components")]
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _agent;
        [HideInInspector] public Vector2 MovementVector { get; set; }
        public int Level { get; set; } = 1;

        private void Awake()
        {
            _agent ??= GetComponent<NavMeshAgent>();
            _animator ??= GetComponentInChildren<Animator>();
        }

        public override void Spawned()
        {
            if (Object.HasInputAuthority)
                Local = this;

        }

        public override void FixedUpdateNetwork()
        {
            Vector3 direction = new(MovementVector.x, 0, MovementVector.y);

            if (MovementVector.magnitude > 0.5f)
                _agent.Move(transform.TransformDirection(direction) * _agent.speed);

            transform.LookAt(Vector3.zero);
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

