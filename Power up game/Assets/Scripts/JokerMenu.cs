using UnityEngine;
using UnityEngine.UI;

public class JokerMenu : MonoBehaviour
{
    public Movement m;
    public HpSystem hpSys;
    public PlayerShooting pShoot;
    int card1;
    int card2;
    int card3;
    bool hasChosen;

    [SerializeField] Image c1Img;
    [SerializeField] Image c2Img;
    [SerializeField] Image c3Img;

    [SerializeField] Sprite baseCardImg;
    [SerializeField] Sprite speedUpCard;
    [SerializeField] Sprite dmgUpCard;
    [SerializeField] Sprite extraCard;
    [SerializeField] Sprite hpUpCard;
    [SerializeField] Sprite lifeStealCard;
    [SerializeField] Sprite attackSpeedUpCard;
    [SerializeField] Sprite pierceUpCard;


    string[] upgrades = { "speedUp", "dmgUp", "extraCard", "hpUp", "lifeStealUp", "attackSpeedUp", "pierceUp" };
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
    }

    public void Buy1()
    {
        if (!hasChosen)
        {
            ApplyUpgrade(c1);
            SetUpgradeImg(c1Img, c1);
            card2 = card1;
            card3 = card1;
            hasChosen = true;

        }
    }
    public void Buy2()
    {
        if (!hasChosen)
        {
            ApplyUpgrade(c2);
            SetUpgradeImg(c2Img, c2);
            card2 = card1;
            card3 = card1;
            hasChosen = true;
        } 
    }

    public void Buy3()
    {
        if (!hasChosen)
        {
            SetUpgradeImg(c3Img, c3);
            ApplyUpgrade(c3);
            card2 = card1;
            card3 = card1;
            hasChosen = true;
        } 
    }
    
    void ApplyUpgrade(int upgradeSlot)
    {
        if (upgradeSlot == 0)
        {
            m.speed++;
            m.baseSpeed++;
        }
        else if (upgradeSlot == 1)
        {
            pShoot.damage++;
            pShoot.baseDamage++;
        }
        else if (upgradeSlot == 2)
        {
            pShoot.cardAmount++;
            pShoot.baseCardAmount++;
        }
        else if (upgradeSlot == 3)
        {
            hpSys.hp += 10;
            hpSys.maxHp += 10;
        }
        else if (upgradeSlot == 4)
        {
            pShoot.lifestealAmount += 5;
            pShoot.baseLifestealAmount += 5;
        }
        else if (upgradeSlot == 5)
        {
            pShoot.cooldown -= 0.1f;
            pShoot.baseCooldown -= 0.1f;
        }
        else if (upgradeSlot == 6)
        {
            pShoot.pierceAmount += 1;
            pShoot.basePierceAmount += 1;
        }
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
            targetImage.sprite = attackSpeedUpCard;
        }
        else if (upgradeSlot == 6)
        {
            targetImage.sprite = pierceUpCard;
        }
        else
        {
            targetImage.sprite = null;
        }
    }

    public void ContinueGame()
    {
        hpSys.inMenu = false;
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
        hasChosen = false;
        c1Img.sprite = baseCardImg;
        c2Img.sprite = baseCardImg;
        c3Img.sprite = baseCardImg;
    }
}
