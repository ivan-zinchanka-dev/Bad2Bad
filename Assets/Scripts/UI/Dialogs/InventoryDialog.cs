using UnityEngine;

namespace UI.Dialogs
{
    public class InventoryDialog : ClosableDialog
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            Time.timeScale = 0.0f;
        }

        public void SetModel()
        {
            
        }
        
        protected override void OnDisable()
        {
            Time.timeScale = 1.0f;
            base.OnDisable();
        }
    }
}