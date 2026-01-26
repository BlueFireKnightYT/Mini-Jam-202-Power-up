using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler
{
    RectTransform rectTransform;
    Vector2 pos;
    CanvasGroup canvasGroup;
    Transform originalParent;
    Transform cardsParent;

    public GameObject currentSlot = null;

    public bool isUnlocked = false;

    public bool inSlot = false;
    private void Start()
    {
        pos = rectTransform.anchoredPosition;
        originalParent = transform.parent;
        cardsParent = transform.parent.parent.parent;
    }
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isUnlocked)
        {
            rectTransform.anchoredPosition += eventData.delta;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(cardsParent);
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        inSlot = false;
        if(currentSlot != null)
        {
            currentSlot.GetComponent<ItemSlot>().cardInSlot = null;
            currentSlot = null;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        if (!inSlot)
        {
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = pos; 
        }
    }
}
