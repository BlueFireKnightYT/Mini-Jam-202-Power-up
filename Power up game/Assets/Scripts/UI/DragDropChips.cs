using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDropChips : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    RectTransform rectTransform;
    Vector2 pos;
    CanvasGroup canvasGroup;
    Transform originalParent;
    Transform cardsParent;

    GameObject player;

    public ChipWeapon chipAttributes;

    Image image;

    public GameObject currentSlot = null;

    public bool isUnlocked = false;

    public bool inSlot = false;
    private void Start()
    {
        pos = rectTransform.anchoredPosition;
        originalParent = transform.parent;
        cardsParent = transform.parent.parent.parent.parent.parent;
        player = GameObject.FindGameObjectWithTag("player");
        image = GetComponent<Image>();
        image.sprite = chipAttributes.chipSprite;
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(cardsParent);
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        inSlot = false;
        if (currentSlot != null)
        {
            currentSlot.GetComponent<ItemSlotChips>().cardInSlot = null;
            currentSlot.GetComponent<ItemSlotChips>().currentChip = null;
            player.GetComponent<PlayerShooting>().chipWeapons[currentSlot.GetComponent<ItemSlotChips>().chipIndex] = null;
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
