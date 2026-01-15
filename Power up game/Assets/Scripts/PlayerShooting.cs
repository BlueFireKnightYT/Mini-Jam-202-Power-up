using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float cooldown;

    float neededTime;
    bool canShoot = true;

    public void Shoot(InputAction.CallbackContext context)
    {
        if(context.performed && canShoot == true)
        {
            canShoot = false;
            neededTime = Time.time + cooldown;
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }

    private void Update()
    {
        if (neededTime > Time.time)
        {
            canShoot = true;
        }
    }
}
