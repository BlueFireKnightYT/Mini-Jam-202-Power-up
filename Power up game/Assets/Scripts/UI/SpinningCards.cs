using UnityEngine;
using UnityEngine.UI;

public class SpinningCards : MonoBehaviour
{
    public GameObject card1;
    public GameObject card2;
    public GameObject card3;
    Animator card1Anim;
    Animator card2Anim;
    Animator card3Anim;
    public GameObject cardSlot1;
    public GameObject cardSlot2;
    public GameObject cardSlot3;

    private void Start()
    {
        card1Anim = card1.GetComponent<Animator>();
        card2Anim = card2.GetComponent<Animator>();
        card3Anim = card3.GetComponent<Animator>();
    }
    private void Update()
    {
        if(cardSlot1.GetComponent<ItemSlot>().cardInSlot == null)
        {
            card1.SetActive(false);
        }
        else
        {
            card1.SetActive(true);
            card1.GetComponent<Image>().sprite = cardSlot1.GetComponent<ItemSlot>().cardInSlot.GetComponent<CardIdentifier>().card.cardSprite;
        }
        if (cardSlot2.GetComponent<ItemSlot>().cardInSlot == null)
        {
            card2.SetActive(false);
        }
        else
        {
            card2.SetActive(true);
            card2.GetComponent<Image>().sprite = cardSlot2.GetComponent<ItemSlot>().cardInSlot.GetComponent<CardIdentifier>().card.cardSprite;
        }
        if (cardSlot3.GetComponent<ItemSlot>().cardInSlot == null)
        {
            card3.SetActive(false);
        }
        else
        {
            card3.SetActive(true);
            card3.GetComponent<Image>().sprite = cardSlot3.GetComponent<ItemSlot>().cardInSlot.GetComponent<CardIdentifier>().card.cardSprite;
        }
    }

    public void SpinCard1()
    {
        card1Anim.SetTrigger("SpinCard");
    }
    public void SpinCard2()
    {
        card2Anim.SetTrigger("SpinCard");
    }
    public void SpinCard3()
    {
        card3Anim.SetTrigger("SpinCard");
    }
}
