using Data;
using Fusion;
using Kiosk;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using static Player.PlayerInputs;

namespace Player
{
    public class PlayerController : MonoBehaviour, IPlayerActions
    {
        [SerializeField] public NetworkPlayer Player { get; set; }
        [HideInInspector] public GameAction Action { get; set; }

        public void OnFire(InputAction.CallbackContext context)
            => Action?.Callback.Invoke(Player);

        public void OnLook(InputAction.CallbackContext context)
        {

        }

        public void OnMove(InputAction.CallbackContext context)
            => (Player.MovementVector = context.ReadValue<Vector2>()).Normalize();

    }
}