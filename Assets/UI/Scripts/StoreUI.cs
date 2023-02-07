using CameraAction;
using Cinemachine;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UIComponents
{
    public class StoreUI : MonoBehaviour
    {
        [Header("Player Properties")]
        [SerializeField] private PlayerController _controller;
        [SerializeField] private CameraSwitcher _cameraSwitcher;
        [SerializeField] private PlayerInput _inputActions;
        [Header("UI Properties")]
        [SerializeField] private TMP_Text _storeName;
        [SerializeField] private TMP_Text _itemName;
        [SerializeField] private Image _itemSprite;
        [SerializeField] private TMP_Text _price;

        [HideInInspector] private InventoryObject[] _menu;


        private int select = 0;

        public void OnActivate(InventoryObject[] menu)
        {
            _menu = menu;
            gameObject.SetActive(true);
            select = 0;

            _inputActions.actions.Disable();

            if (menu[0].Stat != Data.Interaction.Evolution)
                _cameraSwitcher.EnterStore(_menu[select].Stat);
        }

        public void OnCancel()
        {
            gameObject.SetActive(false);

            if (_menu[0].Stat != Data.Interaction.Evolution)
                _cameraSwitcher.ExitStore();

            _inputActions.actions.Enable();
        }

        public void OnBuy()
            => _menu[select].UseObject(_controller);

        public void PreviusItem()
        {
            if (--select < 0)
                select = _menu.Length - 1;
        }

        public void NextItem()
        {
            if (++select >= _menu.Length)
                select = 0;
        }

        private void LateUpdate()
        {
            var item = _menu[select];
            _itemName.text = item.Name;
            _itemSprite.sprite = item.Icon;
            _price.text = item.Cost.ToString();
            _storeName.text = _controller.Action?.StoreName;
        }
    }
}

