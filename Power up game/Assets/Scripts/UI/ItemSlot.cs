using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    DragDrop dragDrop;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Debug.Log("OnDrop");
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.GetComponent<RectTransform>().localPosition = GetComponent<RectTransform>().localPosition;
            dragDrop = eventData.pointerDrag.GetComponent<DragDrop>();
            dragDrop.inSlot = true;
        }
    }
}
