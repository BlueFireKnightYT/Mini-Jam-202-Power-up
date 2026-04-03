using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class RouletteWheel : MonoBehaviour, IDropHandler
{
    DragDropChips dragDrop;
    public GameObject choosePanel;
    public Animator anim;
    public ChipWeapon[] allChips;
    public List<GameObject> chipGroups;
    public GameObject chipsInventory;
    public GameObject uiChipPrefab;

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
        RotateWheel(1);
        choosePanel.SetActive(false);
    }

    public void ClickBlack()
    {
        RotateWheel(2);
        choosePanel.SetActive(false);
    }

    private void RotateWheel(int color)
    {
        int redOrBlack = Random.Range(0, 2);
        anim.SetTrigger("spin");
        anim.SetBool("red", false);
        anim.SetBool("black", false);
        if (redOrBlack == 0)
        {
            anim.SetBool("red", true);
            if(color == 1)
            {
                AddRandomChip();
            }
            else
            {
                print("lose");
            }
        }
        if(redOrBlack == 1)
        {
            anim.SetBool("black", true);
            if (color == 2)
            {
                AddRandomChip();
            }
            else
            {
                print("lose");
            }
        }
    }

    private void AddRandomChip()
    {
        int chipType = Random.Range(0, allChips.Length);
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
