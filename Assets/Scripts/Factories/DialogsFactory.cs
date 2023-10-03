using UI.Dialogs;
using UnityEngine;

namespace Factories
{
    public class DialogsFactory : MonoBehaviour
    {
        [SerializeField] private Canvas _dialogCanvas;
        
        [Space]
        [SerializeField] private InventoryDialog _inventoryDialogPrefab;
        //[] other dialogs

        public InventoryDialog ShowInventoryDialog()
        {
            return Instantiate<InventoryDialog>(_inventoryDialogPrefab, _dialogCanvas.transform, false);
        }
    }
}