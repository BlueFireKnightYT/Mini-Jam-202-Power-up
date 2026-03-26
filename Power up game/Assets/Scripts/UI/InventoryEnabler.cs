using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryEnabler : MonoBehaviour
{
    public PowerUpActivator pUA;
    public bool inInv = false;
    public GameObject chipsInventory;
    public GameObject cardInventory;
    public void ToggleInventory(InputAction.CallbackContext context)
    {
        if(context.performed && !pUA.powerUpActive)
        {
            if (!chipsInventory.gameObject.activeSelf && !cardInventory.gameObject.activeSelf)
            {
                cardInventory.gameObject.SetActive(true);
                Time.timeScale = 0;
                pUA.powerUpEnabled = false;
                inInv = true;
            }
            else
            {
                chipsInventory.gameObject.SetActive(false);
                cardInventory.gameObject.SetActive(false);
                Time.timeScale = 1;
                pUA.powerUpEnabled = true;
                inInv = false;
            }
        }
    }

    public void SwitchToChips()
    {
        chipsInventory.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void SwitchToCards()
    {
        cardInventory.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
