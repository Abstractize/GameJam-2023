using UnityEngine;

namespace Components
{
    public class ObjectSelector : MonoBehaviour
    {
        [Range(0.01f, 100f)]
        [SerializeField] private float _range;
        [HideInInspector] public SelectableObject SelectedObject { get; set; } = null;
        public void Select(Ray ray)
            => SelectedObject = Physics.Raycast(ray, out RaycastHit hit, _range) ?
                hit.transform?.GetComponent<SelectableObject>().ApplySelection() : null;

        public void Deselect()
            => SelectedObject = null;
    }
}
