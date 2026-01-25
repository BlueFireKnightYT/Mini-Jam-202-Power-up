using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupChooser : MonoBehaviour
{
    public Movement m;
    public HpSystem hpSys;
    public LevelSystem lvlSys;
    public PowerUpActivator pUA;
    PowerupCard card1;
    PowerupCard card2;
    PowerupCard card3;

    [SerializeField] Image c1Img;
    [SerializeField] Image c2Img;
    [SerializeField] Image c3Img;
    [SerializeField] GameObject inventoryCard;
    [SerializeField] List<Transform> cardGroups;

    [SerializeField] List<PowerupCard> cards;

    PowerupCard c1;
    PowerupCard c2;
    PowerupCard c3;

    private void OnEnable()
    {
        card1 = cards[Random.Range(0, cards.Count)];
        while (card2 == card1 || card2 == null)
        {
            card2 = cards[Random.Range(0, cards.Count)];
        }
        while (card3 == card2 || card3 == card1 || card3 == null)
        {
            card3 = cards[Random.Range(0, cards.Count)];
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
        card1 = null;
        card2 = null;
        card3 = null;
    }
    public void Buy2()
    {
        ApplyUpgrade(c2);
        card1 = null;
        card2 = null;
        card3 = null;
    }

    public void Buy3()
    {
        ApplyUpgrade(c3);
        card1 = null;
        card2 = null;
        card3 = null;
    }

    void ApplyUpgrade(PowerupCard upgradeCard)
    {
        FindCardGroup(upgradeCard);
        lvlSys.inMenu = false;
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

    void FindCardGroup(PowerupCard upgradeCard)
    {
        foreach (Transform cardGroup in cardGroups)
        {
            foreach (Transform child in cardGroup)
            {
                CardIdentifier childCI = child.GetComponent<CardIdentifier>();
                if (childCI != null && childCI.cardName == upgradeCard.cardName)
                {
                    GameObject UiCard = Instantiate(inventoryCard, cardGroup);
                    CardIdentifier cI = UiCard.GetComponent<CardIdentifier>();
                    cI.cardName = upgradeCard.cardName;
                    cI.card = upgradeCard;
                    UiCard.GetComponent<Image>().sprite = upgradeCard.cardSprite;
                    return;
                }
            }
        }

        foreach (Transform cardgroup in cardGroups)
        {
            if (cardgroup.childCount == 0)
            {
                GameObject UiCard = Instantiate(inventoryCard, cardgroup);
                CardIdentifier cI = UiCard.GetComponent<CardIdentifier>();
                cI.cardName = upgradeCard.cardName;
                cI.card = upgradeCard;
                UiCard.GetComponent<Image>().sprite = upgradeCard.cardSprite;
                return;
            }
        }
    }

    void SetUpgradeImg(Image targetImage, PowerupCard upgradeSlot)
    {
        if(upgradeSlot != null)
        {
            targetImage.sprite = upgradeSlot.cardSprite;
        }
        else
        {
            targetImage.sprite = null;
        }
    }
}
