using UnityEngine;
using UnityEngine.InputSystem;
using static Player.PlayerInputs;
using Player;

namespace UIComponents
{
    public class UIController : MonoBehaviour, IUIActions
    {
        [SerializeField] private PlayerInput _input;
        [SerializeField] private PlayerController _controller;
        [HideInInspector] private InventoryObject[] _menu;
        private int select = 0;

        public void OnActivate(InventoryObject[] menu)
        {
            _menu = menu;

            // Cambiar ActionMap
            //_input.currentActionMap;

            // Desplegamos Interfaz
        }

        //public void OnDeativate() => _input.currentActionMap = "Player";

        public void OnCancel(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
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

