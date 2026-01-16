using UnityEngine;
using UnityEngine.InputSystem;

public class HpSystem : MonoBehaviour
{
    int hp = 100;
    public int maxHp = 100;
    public int neededHp = 75;
    public bool inMenu = false;

    public GameObject upgradeMenu;

    private void Start()
    {
        upgradeMenu.SetActive(false);
    }

    private void Update()
    {
        if (hp < maxHp)
        { 
            hp++;
            Debug.Log(hp);
        }
    }

    public void Upgrade(InputAction.CallbackContext context)
    {
        if (context.performed && hp > neededHp && inMenu == false)
        {
            hp -= neededHp;
            Debug.Log("Upgrade");

            //time stop
            Time.timeScale = 0;
            inMenu = true;
            // menu met 3 choices
            upgradeMenu.SetActive(true);
            
        }
        else if(context.performed && inMenu == true)
        {
            //menu sluiten en time door zetten
            Time.timeScale = 1;
            inMenu = false;
            upgradeMenu.SetActive(false);
        }
    }
}
