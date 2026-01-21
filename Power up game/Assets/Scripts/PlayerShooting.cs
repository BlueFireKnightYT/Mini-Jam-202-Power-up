using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public HpSystem hpSys;

    [SerializeField] GameObject bulletPrefab;
    public float cooldown;
    public float baseCooldown;
    public Transform cardPos;

    float neededTime;
    bool canShoot = true;
    bool shooting = false;
    public float damage;
    public float baseDamage;
    public int lifestealAmount;
    public int cardAmount;
    private int shotCardAmount;
    public int baseCardAmount;
    public int pierceAmount;
    public int basePierceAmount;

    private void Start()
    {
        baseDamage = damage;
        baseCooldown = cooldown;
        baseCardAmount = cardAmount;
        basePierceAmount = pierceAmount;
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed && (hpSys.inMenu == false || hpSys.inPauseMenu == false))
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
            shotCardAmount = 0;
            canShoot = false;
            neededTime = cooldown;
            InvokeRepeating("ShootCard", 0, 0.1f);
        }
        if(shotCardAmount >= cardAmount)
        {
            CancelInvoke("ShootCard");
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

    void ShootCard()
    {
        Instantiate(bulletPrefab, cardPos.position, cardPos.rotation);
        shotCardAmount++;
    }

    public void DoLifesteal()
    {
        hpSys.hp += lifestealAmount;
    }
}
