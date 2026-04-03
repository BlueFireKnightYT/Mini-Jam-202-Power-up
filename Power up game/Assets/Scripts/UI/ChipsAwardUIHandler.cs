using UnityEngine;

public class ChipsAwardUIHandler : MonoBehaviour
{
    public GameObject chipsInventory;

    public void Continue()
    {
        chipsInventory.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
