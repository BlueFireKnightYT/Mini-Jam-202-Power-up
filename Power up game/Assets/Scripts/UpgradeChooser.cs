using Unity.Multiplayer.Center.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeChooser : MonoBehaviour
{
    public Movement m;
    public HpSystem hpSys;

    [SerializeField] Image c1Img;
    [SerializeField] Image c2Img;
    [SerializeField] Image c3Img;

    [SerializeField] Sprite speedUpCard;
    [SerializeField] Sprite dmgUpCard;
    [SerializeField] Sprite extraCard;
    [SerializeField] Sprite hpUpCard;
    [SerializeField] Sprite lifeStealCard;

    string[] upgrades = { "speedUp", "dmgUp", "extraCard", "hpUp", "lifeStealUp" };
    int c1;
    int c2;
    int c3;

    int sUPercent;

    private void OnEnable()
    {
        c1 = Random.Range(0, upgrades.Length);
        c2 = Random.Range(0, upgrades.Length);
        c3 = Random.Range(0, upgrades.Length);

        SetUpgradeImg(c1Img, c1);
        SetUpgradeImg(c2Img, c2);
        SetUpgradeImg(c3Img, c3);
    }

    public void Buy1()
    {
        ApplyUpgrade(c1);
    }
    public void Buy2()
    {
        ApplyUpgrade(c2);
    }

    public void Buy3()
    {
        ApplyUpgrade(c3);
    }
    
    void ApplyUpgrade(int upgradeSlot)
    {
        if (upgradeSlot == 0)
        {
            float speedIncrease = m.speed / 100 * sUPercent;
            m.speed += speedIncrease;
        }
        else if (upgradeSlot == 1)
        {

        }
        else if (upgradeSlot == 2)
        {

        }
        else if (upgradeSlot == 3)
        {
            hpSys.maxHp += 10;

        }
        else if (upgradeSlot == 4)
        {

        }
        Debug.Log(upgradeSlot);
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
        else
        {
            targetImage.sprite = null;
        }
    }
}
