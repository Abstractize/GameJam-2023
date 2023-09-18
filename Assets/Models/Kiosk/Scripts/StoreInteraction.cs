using Cinemachine;
using Components;
using Data;
using Player;
using UIComponents;
using UnityEngine;

namespace Kiosk
{
    public class StoreInteraction : SelectableObject
    {
        [SerializeField] private InventoryObject[] _inventory;
        [SerializeField] private StoreUI _ui;

        public void ActivateStore()
            => _ui.OnActivate(_name, _inventory);
    }
}

