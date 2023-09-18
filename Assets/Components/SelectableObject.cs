using System;
using UIComponents;
using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    [Serializable]
    public struct SelectableAction
    {
        public string Name;
        public UnityEvent Callback;
    }
    public abstract class SelectableObject : MonoBehaviour
    {
        [SerializeField] protected ActionUI _actionUI;
        [SerializeField] protected string _name = "Asset Name";
        [SerializeField] protected SelectableAction[] _actions;
        public SelectableObject ApplySelection()
        {
            _actionUI.DisplayActions(_actions);
            return this;
        }
    }
}
