using UnityEngine;
using UnityEngine.UI;

public class UpgradeChooser : MonoBehaviour
{
    public Movement m;
    public HpSystem hpSys;
    public PowerUpActivator pUA;
    int card1;
    int card2;
    int card3;

    [SerializeField] Image c1Img;
    [SerializeField] Image c2Img;
    [SerializeField] Image c3Img;

    [SerializeField] Sprite speedUpCard;
    [SerializeField] Sprite dmgUpCard;
    [SerializeField] Sprite extraCard;
    [SerializeField] Sprite hpUpCard;
    [SerializeField] Sprite lifeStealCard;
    [SerializeField] Sprite timeUpCard;
    [SerializeField] Sprite attackSpeedUpCard;
    [SerializeField] Sprite pierceUpCard;


    string[] upgrades = { "speedUp", "dmgUp", "extraCard", "hpUp", "lifeStealUp", "timeUp", "attackSpeedUp", "pierceUp" };
    int c1;
    int c2;
    int c3;

    private void OnEnable()
    {
        card1 = Random.Range(0, upgrades.Length);
        while (card2 == card1)
        {
            card2 = Random.Range(0, upgrades.Length);
        } 
        while (card3 == card2 || card3 == card1)
        {
            card3 = Random.Range(0, upgrades.Length);
        }
        c1 = card1;
        c2 = card2;
        c3 = card3;

        SetUpgradeImg(c1Img, c1);
        SetUpgradeImg(c2Img, c2);
        SetUpgradeImg(c3Img, c3);
    }

    public void Buy1()
    {
        ApplyUpgrade(c1);
        card2 = card1;
        card3 = card1;
    }
    public void Buy2()
    {
        ApplyUpgrade(c2);
        card2 = card1;
        card3 = card1;
    }

    public void Buy3()
    {
        ApplyUpgrade(c3);
        card2 = card1;
        card3 = card1;
    }
    
    void ApplyUpgrade(int upgradeSlot)
    {
        if (upgradeSlot == 0)
        {
            pUA.poweredSpeed++;
        }
        else if (upgradeSlot == 1)
        {
            pUA.poweredDamage += 10;
        }
        else if (upgradeSlot == 2)
        {
            pUA.poweredCardAmount++;
        }
        else if (upgradeSlot == 3)
        {
            hpSys.maxShield += 10;
        }
        else if (upgradeSlot == 4)
        {
            pUA.poweredLifesteal++;
        }
        else if (upgradeSlot == 5)
        {
            pUA.powerUpDuration++;
        }
        else if (upgradeSlot == 6)
        {
            pUA.poweredCooldown += 0.2f;
        }
        else if (upgradeSlot == 7)
        {
            pUA.poweredPierceAmount ++;
        }
        hpSys.inMenu = false;
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

    void SetUpgradeImg(Image targetImage, int upgradeSlot)
    {

        if (upgradeSlot == 0)
        {
            targetImage.sprite = speedUpCard;
        }
        else if (upgradeSlot == 1)
        {
            targetImage.sprite = dmgUpCard;
        }
        else if (upgradeSlot == 2)
        {
            targetImage.sprite = extraCard;
        }
        else if (upgradeSlot == 3)
        {
            targetImage.sprite = hpUpCard;
        }
        else if (upgradeSlot == 4)
        {
            targetImage.sprite = lifeStealCard;
        }
        else if (upgradeSlot == 5)
        {
            targetImage.sprite = timeUpCard;
        }
        else if (upgradeSlot == 6)
        {
            targetImage.sprite = attackSpeedUpCard;
        }
        else if (upgradeSlot == 7)
        {
            targetImage.sprite = pierceUpCard;
        }
        else
        {
            targetImage.sprite = null;
        }
    }
}
