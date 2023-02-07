using Data;
using UnityEngine;

namespace CameraAction
{
    public class CameraSwitcher : MonoBehaviour
    {
        [SerializeField] private Animator _cameraAnimator;

        private void Awake()
            => _cameraAnimator ??= GetComponent<Animator>();

        public void EnterStore(Interaction interaction)
        {
            _cameraAnimator.SetTrigger(nameof(EnterStore));

            _cameraAnimator.SetInteger(nameof(Interaction), (int)interaction);
        }

        public void ExitStore()
            => _cameraAnimator.SetTrigger(nameof(ExitStore));
    }
}