using System.Collections;
using Data;
using UnityEngine;
using UnityEngine.InputSystem;
using static Player.PlayerInputs;

namespace Player
{
    public class PlayerController : MonoBehaviour, IPlayerActions
    {
        [SerializeField] public NetworkPlayer Player { get; set; }
        [HideInInspector] public GameAction Action { get; set; }
        [HideInInspector] public Wallet Wallet { get; set; }

        private const int EARNING = 10;
        public int Level { get; private set; } = 1;

        private void Start()
        {
            StartCoroutine(nameof(GenerateMoney));
        }

        public void OnFire(InputAction.CallbackContext context)
            => Action?.Callback.Invoke(Player);

        public void OnLook(InputAction.CallbackContext context)
        {

        }

        public void OnMove(InputAction.CallbackContext context)
            => (Player.MovementVector = context.ReadValue<Vector2>()).Normalize();

        private IEnumerator GenerateMoney()
        {
            Wallet.Money += Level * EARNING;
            // Wallet Animation
            yield return new WaitForSeconds(1.0f);
        }
    }
}