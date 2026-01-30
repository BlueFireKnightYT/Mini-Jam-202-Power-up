using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryEnabler : MonoBehaviour
{
    public PowerUpActivator pUA;
    public bool inInv = false;
    public void ToggleInventory(InputAction.CallbackContext context)
    {
        if(context.performed && !pUA.powerUpActive)
        {
            if (!this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(true);
                Time.timeScale = 0;
                pUA.powerUpEnabled = false;
                inInv = true;
            }
            else
            {
                this.gameObject.SetActive(false);
                Time.timeScale = 1;
                pUA.powerUpEnabled = true;
                inInv = false;
            }
        }
    }
}
