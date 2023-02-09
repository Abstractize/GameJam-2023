using System.Collections;
using Components;
using Data;
using UnityEngine;
using UnityEngine.InputSystem;
using static Player.PlayerInputs;

namespace Player
{
    public partial class PlayerController : MonoBehaviour, IPlayerActions
    {
        [SerializeField] public NetworkPlayer Player { get; set; }
        [HideInInspector] public Wallet Wallet { get; set; } = new();
        [HideInInspector] public PlayerStats Stats { get; set; } = new();
        [HideInInspector] private Vector2 _mousePosition;
        [SerializeField]
        [Range(10, 100)]
        private float _waitTime = 15;
        [SerializeField] private ObjectSelector _selector;
        [SerializeField] private Camera _camera;
        [SerializeField] private bool _isGeneratingMoney = true;
        [SerializeField] private bool _isEnabledStats = true;
        [SerializeField] private MessageLogger _logger;

        private const int EARNING = 10;
        private const int DECAY = 1;

        public Canvas Hud { get; set; }

        private void Start()
        {
            StartCoroutine(nameof(GenerateMoney));
            StartCoroutine(nameof(EnableStats));
        }

        public void OnMove(InputAction.CallbackContext context)
            => (Player.MovementVector = context.ReadValue<Vector2>()).Normalize();

        public void OnSelect(InputAction.CallbackContext context)
            => _selector.Select(_camera.ScreenPointToRay(_mousePosition));

        public void OnMouse(InputAction.CallbackContext context)
            => _mousePosition = context.ReadValue<Vector2>();

        private IEnumerator GenerateMoney()
        {
            while (_isGeneratingMoney)
            {
                Wallet.Money += Player.Level * EARNING;

                // Trigger Wallet Animation

                yield return new WaitForSeconds(_waitTime);
            }
        }

        private IEnumerator EnableStats()
        {
            while (_isEnabledStats)
            {
                yield return new WaitForSeconds(_waitTime / 3);

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


            }
        }

        public void BuyItem(Interaction stat, int cost, int amount)
        {
            try
            {
                if (Wallet.Money < cost)
                    throw new System.Exception("You don't have enough money to buy this item");

                if (IsStatMaxed(stat))
                    throw new System.Exception("The stat is at its maximum value");

                Wallet.Money -= cost;
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

                    case Interaction.Evolution:
                        Player.Level += amount;
                        break;
                }
            }
            catch (System.Exception e)
            {
                _logger.LogMessage(e.Message);
            }

        }

        private bool IsStatMaxed(Interaction stat) => stat switch
        {
            Interaction.Hunger => Stats.Hunger.IsMax,
            Interaction.Fun => Stats.Fun.IsMax,
            Interaction.Hygiene => Stats.Hygiene.IsMax,
            Interaction.Sleep => Stats.Sleep.IsMax,
            _ => false,
        };
    }
}