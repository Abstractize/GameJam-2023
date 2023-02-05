using System.Collections;
using Data;
using UnityEngine;
using UnityEngine.InputSystem;
using static Player.PlayerInputs;
using StatsController;
using UnityEngine.UI;

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
        [SerializeField] public StatsBar statsBar { get; set; }

        private const int EARNING = 10;
        public int Level { get; private set; } = 1;

        private const int DECAY = 1;

        public Canvas Hud { get; set; }

        public enum Interaction
        {
            Default,
            Hunger,
            Fun,
            Sleep,
            Hygiene
        }
        public Interaction interaction = Interaction.Default;

        private void Start()
        {
            StartCoroutine(nameof(GenerateMoney));
            StartCoroutine(nameof(EnableStats));

            statsBar = GameObject.FindGameObjectsWithTag("HUD")[0].GetComponent<StatsBar>();
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
                statsBar.Coins.text = Wallet.Money.ToString();

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
                        Stats.Hunger -= DECAY;
                        break;
                    case 2:
                        Stats.Fun -= DECAY;
                        break;
                    case 3:
                        Stats.Sleep -= DECAY;
                        break;
                    case 4:
                        Stats.Hygiene -= DECAY;
                        break;
                }

                statsBar.hunger.value = Stats.Hunger;
                statsBar.fun.value = Stats.Fun;
                statsBar.hygiene.value = Stats.Hygiene;
                statsBar.sleep.value = Stats.Sleep;

                Debug.Log($"Stats lower");
                yield return new WaitForSeconds(_waitTime / 3);
            }
        }

        private void PlayerInteract()
        {
            switch (interaction)
            {
                case Interaction.Default:
                    break;
                case Interaction.Hunger:
                    // Hunger Store
                    break;
                case Interaction.Fun:
                    // Fun Store
                    break;
                case Interaction.Hygiene:
                    // Hygiene Store
                    break;
                case Interaction.Sleep:
                    // Sleep Store
                    break;
            }
        }
    }
}