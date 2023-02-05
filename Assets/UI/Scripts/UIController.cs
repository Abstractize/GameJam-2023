using UnityEngine;
using UnityEngine.InputSystem;
using static Player.PlayerInputs;
using Player;
using UnityEngine.UI;
using TMPro;

namespace UIComponents
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private PlayerInput _input;
        [SerializeField] private PlayerController _controller;
        [SerializeField] private Canvas _hud;
        [SerializeField] private Canvas _store;
        [SerializeField] private Image _item;
        [SerializeField] private TMP_Text _action;
        private bool isHudActive = true;
        private bool isStoreActive = false;
        [HideInInspector] private InventoryObject[] _menu;
        private int select = 0;

        public void OnActivate(InventoryObject[] menu)
        {
            _menu = menu;
            _store.gameObject.SetActive(true);
            _input.defaultActionMap = "UI";
            _item.sprite = _menu[select].Icon;
        }

        public void OnCancel()
        {
            _store.gameObject.SetActive(false);
            _input.defaultActionMap = "Player";
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnMiddleClick(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnNavigate(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>().y;

            select += Mathf.RoundToInt(value);

            if (select >= _menu.Length)
                select = 0;

            _item.sprite = _menu[select].Icon;

        }

        public void OnPoint(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnScrollWheel(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnSubmit()
            => _menu[select].UseObject(_controller);

        public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnTrackedDevicePosition(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}

