using UnityEngine;

namespace Potato
{
    public class PotatoSpawner : MonoBehaviour
    {
        [Range(0.01f, 100f)]
        [SerializeField] private float _range;
        [SerializeField] Transform _spawnPoint;
        [HideInInspector] Transform _hitObject;
        private void Awake()
            => _spawnPoint = GetComponent<Transform>();

        public void ThrowPotato()
            => _hitObject = Physics.Raycast(_spawnPoint.position, _spawnPoint.forward, out RaycastHit hit, _range) ?
                hit.transform : null;
    }
}


