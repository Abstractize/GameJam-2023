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
        [HideInInspector] public GameAction Action { get; set; } = new();
        [HideInInspector] public Wallet Wallet { get; set; } = new();
        [HideInInspector] public PlayerStats Stats { get; set; } = new();
        [SerializeField]
        [Range(10, 100)]
        private float _waitTime = 15;
        [SerializeField] private bool _isGeneratingMoney = true;
        [SerializeField] private bool _isEnabledStats = true;

        private const int EARNING = 10;
        public int Level { get; private set; } = 1;

        private const int DECAY = 1;

        private void Start()
        {
            StartCoroutine(nameof(GenerateMoney));
            StartCoroutine(nameof(EnableStats));
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
            while (_isGeneratingMoney)
            {
                Wallet.Money += Level * EARNING;

                // Trigger Wallet Animation

                Debug.Log($"You have {Wallet.Money} money");
                yield return new WaitForSeconds(_waitTime);
            }
        }

        private IEnumerator EnableStats()
        {
            while (_isEnabledStats)
            {
                int i = Random.Range(1, 5);

                switch (i)
                {
                    case 1:
                        Stats.hunger -= DECAY;
                        break;
                    case 2:
                        Stats.fun -= DECAY;
                        break;
                    case 3:
                        Stats.sleep -= DECAY;
                        break;
                    case 4:
                        Stats.hygiene -= DECAY;
                        break;
                }

                Debug.Log($"Stats lower");
                yield return new WaitForSeconds(_waitTime / 3);
            }
        }
    }
}