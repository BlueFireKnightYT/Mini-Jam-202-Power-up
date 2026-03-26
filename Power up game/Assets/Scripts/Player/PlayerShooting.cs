using System.Collections.Generic;
using UnityEditorInternal;
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
    public float damageMultiplier;
    public int cardAmount;
    private int shotCardAmount;
    public int baseCardAmount;
    public int pierceAmount;
    public int basePierceAmount;
    GameObject bulletPrefab;
    public SpriteRenderer weaponSprite;
    public int currentWeapon;

    public GameObject projectile;

    [Header("Projectile Attributes")]
    public float projectileSizeMultiplier = 1;
    public float projectileSpeedMultiplier = 1;

    [Header("Explosion Attributes")]
    public bool explosiveBullets;
    public GameObject explosionPrefab;
    public float explosionRadius;

    [Header("Homing Attributes")]
    public bool isHoming;
    public float homingSpeed;

    [Header("Direction Attributes")]
    public bool isBoomerang;
    public bool isChaotic;
    public bool isOrbit;

    [Header("Chip Stuff")]
    public int chipCount;
    public ChipWeapon[] chipWeapons = new ChipWeapon[3];

    public GameObject lastProjectile;
    private void Start()
    {
        m = GetComponent<Movement>();
        lvlSys = GetComponent<LevelSystem>();
        rb = GetComponent<Rigidbody2D>();
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
            if (weapons[currentWeapon].weaponName == "Card")
            {
                shotCardAmount = 0;
                InvokeRepeating("ShootCardWrapper", 0, 0.1f);
            }
            if (weapons[currentWeapon].weaponName == "Coin")
            {
                Invoke("ShootChipWrapper", 0f);
                Invoke("ShootChipWrapper", 0.4f);
                Invoke("ShootChipWrapper", 0.8f);
            }
            canShoot = false;
            neededTime = weapons[currentWeapon].cooldownTime;
        }
        if(shotCardAmount >= cardAmount)
        {
            CancelInvoke("ShootCardWrapper");
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

    void ShootCardWrapper()
    {
        ShootCard(cardPos.position, cardPos.rotation);
    }
    public void ShootCard(Vector2 position, Quaternion rotation)
    {
        GameObject projectile = Instantiate(bulletPrefab, position, rotation);
        shotCardAmount++;
        lastProjectile = projectile;
        projectile.GetComponent<Rigidbody2D>().AddForce(rb.linearVelocity, ForceMode2D.Impulse);
        Bullet bullet = projectile.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.damage = damage;
            bullet.damageMultiplier = damageMultiplier;
            bullet.speedMultiplier = projectileSpeedMultiplier;
            bullet.sizeMultiplier = projectileSizeMultiplier;
            if (explosiveBullets)
            {
                Bullet bulletScript = projectile.GetComponent<Bullet>();
                bulletScript.canExplode = true;
                bulletScript.explosionPrefab = explosionPrefab;
                bulletScript.explosionRadius = explosionRadius;

            }
            if (isHoming)
            {
                projectile.GetComponent<Bullet>().isHoming = true;
                projectile.GetComponent<Bullet>().homingSpeed = homingSpeed;
            }
            if (weapons[currentWeapon].gravityEnabled)
            {
                projectile.GetComponent<Rigidbody2D>().AddForce(projectile.transform.up * weapons[currentWeapon].projectileForce, ForceMode2D.Impulse);
            }
            if (isBoomerang)
            {
                projectile.GetComponent<Bullet>().isBoomerang = true;
            }
            if (isChaotic)
            {
                projectile.GetComponent<Bullet>().isChaotic = true;
            }
            if (isOrbit)
            {
                projectile.GetComponent<Bullet>().isOrbit = true;
            }
        }
    }

    void ShootChipWrapper()
    {
        ShootChip(cardPos.position, cardPos.rotation);
    }

    public void ShootChip(Vector2 position, Quaternion rotation)
    {
        if(chipCount >= 2)
        {
            chipCount = 0;
        }
        else
        {
            chipCount++;
        }
        if (chipWeapons[chipCount] != null)
        {
            GameObject chip = Instantiate(chipWeapons[chipCount].chipPrefab, position, rotation);
            lastProjectile = chip;
            chip.GetComponent<Rigidbody2D>().AddForce(rb.linearVelocity, ForceMode2D.Impulse);
        }
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
