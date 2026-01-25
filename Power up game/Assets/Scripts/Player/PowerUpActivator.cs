using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PowerUpActivator : MonoBehaviour
{
    bool ableToPowerUp;
    public bool powerUpActive;
    public bool powerUpEnabled;
    public GameObject playerObj;
    public Image UIPowered;
    HpSystem hpSys;
    public ItemSlot slot1;
    public ItemSlot slot2;
    public ItemSlot slot3;
    PlayerShooting playerShooting;
    public SpinningCards uiCards;

    private void Start()
    {
        hpSys = playerObj.GetComponent<HpSystem>();
        playerShooting = playerObj.GetComponent<PlayerShooting>();
        UIPowered.color = Color.grey;
    }

    private void Update()
    {
        if(hpSys.hp > hpSys.neededHp && !powerUpActive)
        {
            ableToPowerUp = true;
        }
        if (powerUpActive)
        {
            DuringPowerUp();
        }
    }
    public void PowerUp(InputAction.CallbackContext context)
    {
        if (ableToPowerUp && context.performed && !powerUpActive && powerUpEnabled)
        {
            ableToPowerUp = false;
            powerUpActive = true;
            UIPowered.color = Color.yellow;
            if(slot1.cardInSlot != null)
            {
                Slot1Ability();
                print("slot1");
            }
            if(slot2.cardInSlot != null)
            {
                Slot2Ability();
                print("slot2");
            }
            if(slot3.cardInSlot != null)
            {

                Slot3Ability();
                print("slot3");
            }
            Invoke("StopPowerUp", 5f);
        }
    }
    public void DuringPowerUp()
    {

    }

    public void StopPowerUp()
    {
        powerUpActive = false;
        UIPowered.color = Color.grey;
        StopSlot1Ability();
        StopSlot2Ability();
        StopSlot3Ability();
    }

    public void Slot1Ability()
    {
        CardIdentifier cI = slot1.cardInSlot.GetComponent<CardIdentifier>();
        cI.card.onActivate(transform.position, playerShooting.weapons[playerShooting.currentWeapon].projectilePrefab);
        cI.card.EnableExplosiveBullets(true);
        playerShooting.cardAmount *= cI.card.projectileAmount;
        uiCards.SpinCard1();
    }
    public void StopSlot1Ability()
    {
        CardIdentifier cI = slot1.cardInSlot.GetComponent<CardIdentifier>();
        cI.card.EnableExplosiveBullets(false);
        playerShooting.cardAmount /= cI.card.projectileAmount;
    }
    public void Slot2Ability()
    {
        CardIdentifier cI = slot2.cardInSlot.GetComponent<CardIdentifier>();
        cI.card.onActivate(transform.position, playerShooting.weapons[playerShooting.currentWeapon].projectilePrefab);
        cI.card.EnableExplosiveBullets(true);
        playerShooting.cardAmount *= cI.card.projectileAmount;
        if (cI.card.retryAmount > 0)
        {
            for (int i = 0; i < cI.card.retryAmount; i++)
            {
                Invoke("Slot1Ability", 0.2f);
            }
        }
        uiCards.SpinCard2();
    }
    public void StopSlot2Ability()
    {
        CardIdentifier cI = slot2.cardInSlot.GetComponent<CardIdentifier>();
        playerShooting.cardAmount /= cI.card.projectileAmount;
        cI.card.EnableExplosiveBullets(false);
        if (cI.card.retryAmount > 0)
        {
            for (int i = 0; i < cI.card.retryAmount; i++)
            {
                Invoke("StopSlot1Ability", 0);
            }
        }
        
    }
    public void Slot3Ability()
    {
        CardIdentifier cI = slot3.cardInSlot.GetComponent<CardIdentifier>();
        cI.card.onActivate(transform.position, playerShooting.weapons[playerShooting.currentWeapon].projectilePrefab);
        cI.card.EnableExplosiveBullets(true);
        playerShooting.cardAmount *= cI.card.projectileAmount;
        if (cI.card.retryAmount > 0)
        {
            for (int i = 0; i < cI.card.retryAmount; i++)
            {
                Invoke("Slot2Ability", 0.2f);
            }
        }
        uiCards.SpinCard3();
    }
    public void StopSlot3Ability()
    {
        CardIdentifier cI = slot3.cardInSlot.GetComponent<CardIdentifier>();
        playerShooting.cardAmount /= cI.card.projectileAmount;
        cI.card.EnableExplosiveBullets(false);
        if (cI.card.retryAmount > 0)
        {
            for (int i = 0; i < cI.card.retryAmount; i++)
            {
                Invoke("StopSlot2Ability", 0);
            }
        }
    }
}
