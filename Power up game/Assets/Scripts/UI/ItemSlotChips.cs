using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotChips : MonoBehaviour, IDropHandler
{
    DragDropChips dragDrop;
    public GameObject cardInSlot;
    public ChipWeapon currentChip;
    public PlayerShooting psScript;
    public int chipIndex;
    public void OnDrop(PointerEventData eventData)
    { 
        if (eventData.pointerDrag != null)
        {
            dragDrop = eventData.pointerDrag.GetComponent<DragDropChips>();
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.GetComponent<RectTransform>().localPosition = GetComponent<RectTransform>().localPosition;
            dragDrop.currentSlot = this.gameObject;
            dragDrop.inSlot = true;
            cardInSlot = eventData.pointerDrag.gameObject;
            currentChip = eventData.pointerDrag.GetComponent<DragDropChips>().chipAttributes;
            psScript.chipWeapons[chipIndex] = currentChip;
        }
    }
}
