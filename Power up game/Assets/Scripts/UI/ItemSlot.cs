using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    DragDrop dragDrop;
    public GameObject cardInSlot;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            dragDrop = eventData.pointerDrag.GetComponent<DragDrop>();
            if (!dragDrop.isChip)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                eventData.pointerDrag.GetComponent<RectTransform>().localPosition = GetComponent<RectTransform>().localPosition;
                dragDrop.currentSlot = this.gameObject;
                dragDrop.inSlot = true;
                cardInSlot = eventData.pointerDrag.gameObject;
            }  
        }
    }
}
