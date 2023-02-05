using Fusion;
using UnityEngine;

namespace Player
{
    public class PlayerSounds : NetworkBehaviour
    {
        [SerializeField] private AudioSource _soundSource;
        [SerializeField] private AudioClip[] _normalSounds;
        [SerializeField] private AudioClip[] _laughSounds;
        [SerializeField] private AudioClip[] _hungerSounds;

        [SerializeField] public bool PlayRandomSounds { get; private set; } = true;

        private void Awake()
            => _soundSource ??= GetComponent<AudioSource>();

        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        public void RPC_PlayNormalSound()
        {
            _soundSource.clip = _normalSounds[Random.Range(0, _normalSounds.Length - 1)];
            _soundSource.Play();
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        public void RPC_PlayLaughSound()
        {
            _soundSource.clip = _laughSounds[Random.Range(0, _laughSounds.Length - 1)];
            _soundSource.Play();
        }
        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        public void RPC_PlayHungerSound()
        {
            _soundSource.clip = _hungerSounds[Random.Range(0, _hungerSounds.Length - 1)];
            _soundSource.Play();
        }

    }
}