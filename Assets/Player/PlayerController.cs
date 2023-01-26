using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;
using static Player.PlayerInputs;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : NetworkBehaviour, IPlayerActions
    {
        [SerializeField] private new Rigidbody rigidbody;

        private Vector2 move;
        public void OnFire(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnMove(InputAction.CallbackContext context)
            => move = context.ReadValue<Vector2>();

        private void Awake()
        {
            rigidbody ??= GetComponent<Rigidbody>();
        }

        public override void FixedUpdateNetwork()
        {
            this.rigidbody.position += new Vector3(move.x, 0, move.y);
        }
    }
}