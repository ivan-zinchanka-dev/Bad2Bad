using UnityEngine;
using UnityEngine.UI;

namespace UI.Dialogs
{
    public class ClosableDialog : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _hideArea;

        protected virtual void OnEnable()
        {
            _closeButton.onClick.AddListener(Close);
            if (_hideArea != null) _hideArea.onClick.AddListener(Close);
        }

        private void Close()
        {
            Destroy(gameObject);
        }

        protected virtual void OnDisable()
        {
            _closeButton.onClick.RemoveListener(Close);
            if (_hideArea != null) _hideArea.onClick.RemoveListener(Close);
        }
    }
}