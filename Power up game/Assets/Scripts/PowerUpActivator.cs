using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PowerUpActivator : MonoBehaviour
{
    bool ableToPowerUp;
    bool powerUpActive;
    public  float powerUpDuration;
    public GameObject playerObj;
    public Image UIPowered;
    HpSystem hpSys;
    public float poweredSpeed;
    public float poweredDamage;
    public int poweredLifesteal;
    public float poweredCooldown;
    public float realCooldown;
    public int poweredCardAmount;
    public int poweredPierceAmount;
    Movement moveScript;
    PlayerShooting shootScript;

    private void Start()
    {
        hpSys = playerObj.GetComponent<HpSystem>();
        moveScript = GetComponent<Movement>();
        shootScript = GetComponent<PlayerShooting>();

        UIPowered.color = Color.grey;
    }

    private void Update()
    {
        if(hpSys.hp > hpSys.neededHp)
        {
            ableToPowerUp = true;
        }
        if (poweredCooldown > 0) 
        {
            realCooldown = 0.2f + poweredCooldown / 4 ;
        }
        else
        {
            realCooldown = poweredCooldown;
        }
    }
    public void PowerUp(InputAction.CallbackContext context)
    {
        if (ableToPowerUp && context.performed && !powerUpActive)
        {
            ableToPowerUp = false;
            powerUpActive = true;
            hpSys.shield = hpSys.maxShield;
            hpSys.hp -= (hpSys.neededHp);
            moveScript.speed += poweredSpeed;
            shootScript.damage += poweredDamage;
            shootScript.lifestealAmount += poweredLifesteal;
            shootScript.cooldown -= realCooldown;
            shootScript.cardAmount += poweredCardAmount;
            shootScript.pierceAmount += poweredPierceAmount;
            Invoke("StopPowerUp", powerUpDuration);
            UIPowered.color = Color.yellow;
        }
    }

    void StopPowerUp()
    {
        powerUpActive = false;
        moveScript.speed = moveScript.baseSpeed;
        shootScript.damage = shootScript.baseDamage;
        shootScript.lifestealAmount = shootScript.baseLifestealAmount;
        shootScript.cooldown = shootScript.baseCooldown;
        shootScript.cardAmount = shootScript.baseCardAmount;
        shootScript.pierceAmount = shootScript.basePierceAmount;
        hpSys.shield = 0;
        UIPowered.color = Color.grey;
    }

}
