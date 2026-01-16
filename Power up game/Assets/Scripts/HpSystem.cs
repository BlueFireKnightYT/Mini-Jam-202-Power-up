using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.InputSystem;

public class HpSystem : MonoBehaviour
{
    int hp = 100;
    public int maxHp = 100;
    public int neededHp = 75;

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
        if (context.performed && hp > neededHp)
        {
            hp -= neededHp;
            Debug.Log("Upgrade");
        }
    }
}
