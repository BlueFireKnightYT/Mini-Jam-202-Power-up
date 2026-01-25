using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public HpSystem hpSys;
    public LevelSystem lvlSys;
    Movement m;
    Rigidbody2D rb;

    public List<PlayerWeapon> weapons;
    public float cooldown;
    public float baseCooldown;
    public Transform cardPos;

    float neededTime;
    bool canShoot = true;
    bool shooting = false;
    public float damage;
    public float baseDamage;
    public int cardAmount;
    private int shotCardAmount;
    public int baseCardAmount;
    public int pierceAmount;
    public int basePierceAmount;
    GameObject bulletPrefab;
    public SpriteRenderer weaponSprite;
    int currentWeapon;

    public GameObject lastProjectile;
    private void Start()
    {
        m = GetComponent<Movement>();
        lvlSys = GetComponent<LevelSystem>();
        rb = GetComponent<Rigidbody2D>();
        baseDamage = damage;
        baseCooldown = cooldown;
        baseCardAmount = cardAmount;
        basePierceAmount = pierceAmount;
        bulletPrefab = weapons[0].projectilePrefab;
        weaponSprite.sprite = weapons[0].weaponSprite;
        currentWeapon = 0;
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed && (lvlSys.inMenu == false || hpSys.inPauseMenu == false))
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
            if(bulletPrefab.GetComponent<Bullet>() != null)
            {
                shotCardAmount = 0;
                canShoot = false;
                neededTime = cooldown;
                InvokeRepeating("ShootCard", 0, 0.1f);
            }
            else if (bulletPrefab.GetComponent<CoinProjectile>() != null)
            {
                neededTime = cooldown -0.2f;
                canShoot = false;
                Invoke("ShootCoin", 0f);
            }
            else  if (bulletPrefab.GetComponent<SlotBullet>() != null)
            {
                int slotGunCooldown = 2;
                neededTime = slotGunCooldown;
                canShoot = false;
                Invoke("ShootSlot", 0);
            }
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
            GameObject projectile = Instantiate(bulletPrefab, cardPos.position, cardPos.rotation);
            shotCardAmount++;
            projectile.GetComponent<Rigidbody2D>().AddForce(rb.linearVelocity, ForceMode2D.Impulse);
    }

    void ShootCoin()
    {
        GameObject projectile = Instantiate(bulletPrefab, cardPos.position, cardPos.rotation);
        lastProjectile = projectile;
        projectile.GetComponent<Rigidbody2D>().AddForce(rb.linearVelocity, ForceMode2D.Impulse);
    }

    void ShootSlot()
    {
        int shots = Random.Range(4, 13);

        for (int i = 0; i <= shots; i++)
        { 
            GameObject projectile = Instantiate(bulletPrefab, cardPos.position, cardPos.rotation * Quaternion.Euler(0f, 0f, Random.Range(-15, 15)));
            projectile.GetComponent<Rigidbody2D>().AddForce(rb.linearVelocity, ForceMode2D.Impulse);
        }
    }

    public void DoLifesteal(int lifestealAmount)
    {
        hpSys.hp += lifestealAmount;
    }

    public void WeaponSelector(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentWeapon++;
        }

        currentWeapon = (currentWeapon + weapons.Count) % weapons.Count;

        weaponSprite.sprite = weapons[currentWeapon].weaponSprite;
        bulletPrefab = weapons[currentWeapon].projectilePrefab;
    }

    
}
