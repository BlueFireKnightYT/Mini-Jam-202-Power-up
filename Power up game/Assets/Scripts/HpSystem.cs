using UnityEngine;
using UnityEngine.InputSystem;

public class HpSystem : MonoBehaviour
{
    int hp = 100;
    public int maxHp = 100;
    public int neededHp = 75;
    public bool inMenu = false;

    private void Update()
    {
        if (hp < maxHp)
        { 
            hp++;
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
            //menu sluiten en time door zetten
        }
        else if(context.performed && inMenu == true)
        {
            Time.timeScale = 1;
            inMenu = false;
        }
    }
}
