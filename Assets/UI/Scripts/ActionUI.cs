using Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIComponents
{
    public class ActionUI : MonoBehaviour
    {
        [SerializeField] private ObjectSelector _selector;
        [Header("Action Button Properties")]
        [SerializeField] private Button _actionButton;
        [SerializeField] private TMP_Text _actionText;
        [HideInInspector] private SelectableAction[] _actions;
        public void DisplayActions(SelectableAction[] actions)
            => _actions = actions;

        public void Click(int actionNumber)
            => _actions[actionNumber].Callback.Invoke();

        private void LateUpdate()
        {
            _actionButton.enabled = _selector.SelectedObject != null;
            _actionButton.gameObject.SetActive(_selector.SelectedObject != null);
            _actionText.text = _actions?[0].Name;
        }
    }
}