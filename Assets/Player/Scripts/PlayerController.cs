using System.Collections;
using Data;
using UnityEngine;
using UnityEngine.InputSystem;
using StatsController;
using UnityEngine.UI;
using static Player.PlayerInputs;

namespace Player
{
    public partial class PlayerController : MonoBehaviour, IPlayerActions
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
        [SerializeField] public StatsBar StatsBar { get; set; }

        private const int EARNING = 10;

        private const int DECAY = 1;

        public Canvas Hud { get; set; }

        private void Start()
        {
            StartCoroutine(nameof(GenerateMoney));
            StartCoroutine(nameof(EnableStats));

            StatsBar ??= GameObject.FindGameObjectsWithTag("HUD")[0].GetComponent<StatsBar>();
        }

        public void OnFire(InputAction.CallbackContext context)
            => Action?.Callback.Invoke(this);

        public void OnLook(InputAction.CallbackContext context)
        {

        }

        public void OnMove(InputAction.CallbackContext context)
            => (Player.MovementVector = context.ReadValue<Vector2>()).Normalize();

        private IEnumerator GenerateMoney()
        {
            while (_isGeneratingMoney)
            {
                Wallet.Money += Player.Level * EARNING;
                StatsBar.Coins.text = Wallet.Money.ToString();

                // Trigger Wallet Animation

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

                StatsBar.hunger.value = Stats.Hunger;
                StatsBar.fun.value = Stats.Fun;
                StatsBar.hygiene.value = Stats.Hygiene;
                StatsBar.sleep.value = Stats.Sleep;

                yield return new WaitForSeconds(_waitTime / 3);
            }
        }

        public void BuyItem(Interaction stat, int cost, int amount)
        {
            Wallet.Money += cost;
            switch (stat)
            {
                case Interaction.Hunger:
                    Stats.Hunger += amount;
                    break;

                case Interaction.Fun:
                    Stats.Fun += amount;
                    break;

                case Interaction.Hygiene:
                    Stats.Hygiene += amount;
                    break;

                case Interaction.Sleep:
                    Stats.Sleep += amount;
                    break;
            }
        }
    }
}