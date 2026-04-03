using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RouletteWheel : MonoBehaviour, IDropHandler
{
    DragDropChips dragDrop;
    public GameObject choosePanel;
    public Animator anim;
    public ChipWeapon[] allChips;
    public List<GameObject> chipGroups;
    public GameObject chipsInventory;
    public GameObject uiChipPrefab;
    public GameObject rewardMenu;
    public Image chipReward;
    public TextMeshProUGUI chipText;
    int chosenColor;
    int redOrBlack;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            dragDrop = eventData.pointerDrag.GetComponent<DragDropChips>();
            if (dragDrop != null)
            {
                Destroy(dragDrop.gameObject);
                choosePanel.SetActive(true);
            }
        }
    }

    public void ClickRed()
    {
        RotateWheel();
        chosenColor = 1;
        choosePanel.SetActive(false);
    }

    public void ClickBlack()
    {
        RotateWheel();
        chosenColor = 2;
        choosePanel.SetActive(false);
    }

    private void RotateWheel()
    {
        redOrBlack = Random.Range(0, 2);
        anim.SetTrigger("spin");
        anim.SetBool("red", false);
        anim.SetBool("black", false);
        if (redOrBlack == 0)
        {
            anim.SetBool("red", true);
            
        }
        if(redOrBlack == 1)
        {
            anim.SetBool("black", true);
        }
    }
    public void CheckWin()
    {
        if(chosenColor == 1)
        {
            if(redOrBlack == 0)
            {
                AddRandomChip();
            }
        }
        if(chosenColor == 2)
        {
            if(redOrBlack == 1)
            {
                AddRandomChip();
            }
        }
    }
    private void AddRandomChip()
    {   
        int chipType = Random.Range(0, allChips.Length);
        rewardMenu.SetActive(true);
        this.gameObject.transform.parent.parent.gameObject.SetActive(false);
        chipText.text = $"You got a {allChips[chipType].chipName}!";
        chipReward.sprite = allChips[chipType].chipSprite;
        foreach(GameObject group in chipGroups)
        {
            if(group.GetComponent<ChipIdentifier>().chipAttributes == null)
            {
                GameObject chipUI = Instantiate(uiChipPrefab, group.transform);
                chipUI.GetComponent<DragDropChips>().chipAttributes = allChips[chipType];
                group.GetComponent<ChipIdentifier>().chipAttributes = allChips[chipType];
                return;
            }
            else if(group.GetComponent<ChipIdentifier>().chipAttributes == allChips[chipType])
            {
                GameObject chipUI = Instantiate(uiChipPrefab, group.transform);
                chipUI.GetComponent<DragDropChips>().chipAttributes = allChips[chipType];
                return;
            }
        }
    }
}
