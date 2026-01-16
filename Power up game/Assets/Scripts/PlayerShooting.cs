using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float cooldown;
    public Transform cardPos;

    float neededTime;
    bool canShoot = true;
    bool shooting = false;

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            shooting = true;
        }
        else if (context.canceled)
        {
            shooting = false;
        }
    }

    private void Update()
    {
        if (canShoot && shooting)
        {
            canShoot = false;
            neededTime = cooldown;
            Instantiate(bulletPrefab, cardPos.position, cardPos.rotation);
        }


        if (neededTime > 0)
        {
            neededTime -= Time.deltaTime;
        }
        else if(neededTime <= 0 && canShoot == false)
        {
            canShoot = true;
        }
    }
}
