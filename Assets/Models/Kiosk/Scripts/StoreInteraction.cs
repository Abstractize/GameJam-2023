using Cinemachine;
using Data;
using Player;
using UIComponents;
using UnityEngine;

namespace Kiosk
{
    public interface IStoreInteraction
    {
        void ActionCallback(PlayerController player);
    }

    public class StoreInteraction : MonoBehaviour, IStoreInteraction
    {
        [SerializeField] private string _name = "Store";
        [SerializeField] private string _actionName = "Buy Something";
        [SerializeField] private GameAction _action;
        [SerializeField] private PlayerController _controller;
        [SerializeField] private InventoryObject[] _inventory;
        [SerializeField] private StoreUI _ui;

        private void Awake()
            => _action = new GameAction
            {
                StoreName = _name,
                Name = _actionName,
                Callback = ActionCallback
            };

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player.NetworkPlayer>() != _controller.Player)
                return;
            _controller.Action = _action;
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Player.NetworkPlayer>() != _controller.Player)
                return;
            _controller.Action = null;
        }

        public void ActionCallback(PlayerController player)
            => _ui.OnActivate(_inventory);

    }
}

