using Fusion;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using static Player.PlayerInputs;

namespace Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerController : NetworkBehaviour, IPlayerActions
    {
        [SerializeField] NavMeshAgent _agent;
        [SerializeField] Animator _animator;

        [SerializeField][Range(0, 0.99f)] float _smoothing;

        private Vector3 _movementVector;
        private Vector3 _lastDirection;
        private Vector3 _targetDirection;
        private float _lerpTime;

        public void OnFire(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 value = context.ReadValue<Vector2>();
            _movementVector = new Vector3(value.x, 0, value.y);
            _movementVector.Normalize();
        }

        private void Awake()
        {
            _agent ??= GetComponent<NavMeshAgent>();
            if (_animator == null)
                throw new System.Exception($"{nameof(_animator)} is not Implemented");
        }

        public override void FixedUpdateNetwork()
        {
            if (_movementVector != _lastDirection)
                _lerpTime = 0;

            _lastDirection = _movementVector;
            _targetDirection = Vector3.Lerp(_targetDirection, _movementVector, Mathf.Clamp01(_lerpTime * (1 - _smoothing)));

            _agent.Move(_targetDirection * _agent.speed);

            _lerpTime += Time.deltaTime;
        }

        public void LateUpdate()
        {
            _animator.SetFloat("X", _movementVector.x);
            _animator.SetFloat("Y", _movementVector.z);
        }
    }
}