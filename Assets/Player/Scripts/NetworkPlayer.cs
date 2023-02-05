using System.Collections;
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

        [SerializeField] private PlayerSounds _soundEmitter;
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

            StartCoroutine(nameof(PlayNormalSoundRandomly));
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
            _animator.SetFloat("Devolution", (float)GetDevolution());
        }

        public void PlayerLeft(PlayerRef player)
        {
            if (player == Object.InputAuthority)
                Runner.Despawn(Object);
        }

        private Devolution GetDevolution()
        {
            int level = Level % 60;

            return level switch
            {
                int n when 0 <= n && n < 20 => Devolution.BASE_LEVEL,
                int n when 20 <= n && n < 40 => Devolution.MEDIUM_LEVEL,
                int n when 40 <= n && n < 60 => Devolution.ROOT_LEVEL,
                _ => throw new System.Exception($"{nameof(level)} is out of range"),
            };
        }

        public IEnumerator PlayNormalSoundRandomly()
        {

            while (_soundEmitter.PlayRandomSounds)
            {
                _soundEmitter.RPC_PlayNormalSound();
                float randomSeconds = Random.Range(3f, 15f);
                yield return new WaitForSeconds(randomSeconds);
            }
        }
    }
    public enum Devolution
    {
        BASE_LEVEL = 0,
        MEDIUM_LEVEL = 1,
        ROOT_LEVEL = 2
    }
}

