using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public HpSystem hpSys;
    Movement m;
    Rigidbody2D rb;

    public Sprite[] weaponSprites;
    public GameObject[] projectilePrefabs;
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
    int currentBullet;
    int currentWeapon;

    public GameObject lastProjectile;
    private void Start()
    {
        m = GetComponent<Movement>();
        rb = GetComponent<Rigidbody2D>();
        baseDamage = damage;
        baseCooldown = cooldown;
        baseCardAmount = cardAmount;
        basePierceAmount = pierceAmount;
        bulletPrefab = projectilePrefabs[0];
        weaponSprite.sprite = weaponSprites[0];
        currentBullet = 0;
        currentWeapon = 0;
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

    public void DoLifesteal(int lifestealAmount)
    {
        hpSys.hp += lifestealAmount;
    }

    public void WeaponSelector(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentBullet++;
            currentWeapon++;
        }

        currentWeapon = (currentWeapon + weaponSprites.Length) % weaponSprites.Length;
        currentBullet = (currentBullet + projectilePrefabs.Length) % projectilePrefabs.Length;

        weaponSprite.sprite = weaponSprites[currentWeapon];
        bulletPrefab = projectilePrefabs[currentBullet];
    }

    
}
