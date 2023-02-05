using UnityEngine;
using UnityEngine.InputSystem;
using static Player.PlayerInputs;
using Player;
using UnityEngine.UI;

namespace UIComponents
{
    public class UIController : MonoBehaviour, IUIActions
    {
        [SerializeField] private PlayerInput _input;
        [SerializeField] private PlayerController _controller;
        [SerializeField] private Canvas _hud;
        [SerializeField] private Canvas _store;
        [SerializeField] private Image _item;
        private bool isHudActive = true;
        private bool isStoreActive = false;
        [HideInInspector] private InventoryObject[] _menu;
        private int select = 0;

        public void OnActivate(InventoryObject[] menu)
        {
            _menu = menu;

            // Cambiar ActionMap
            // _input.currentActionMap = "UI";

            // Desplegamos Interfaz
        }

        public void OnDeativate()
        {
            // _input.currentActionMap = "Player";
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            if (isStoreActive)
            {
                _item.sprite = _menu[0].Icon;
                _item.SetNativeSize();
                _store.gameObject.SetActive(false);
                isStoreActive = false;
            }
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

        public void OnSubmit(InputAction.CallbackContext context)
        {
            if (!isStoreActive)
            {
                _store.gameObject.SetActive(true);
                isStoreActive = true;
            }
            else
            {
                _menu[select].UseObject(_controller);
            }
        }



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

