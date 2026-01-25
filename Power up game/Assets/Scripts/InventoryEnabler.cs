using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryEnabler : MonoBehaviour
{
    public void ToggleInventory(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if (!this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(true);
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
        
    }
}
